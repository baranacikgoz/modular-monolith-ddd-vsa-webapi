import http from 'k6/http';
import { sleep } from 'k6';
import { runBuyer } from './scenarios/buyer.js';
import { runSeller } from './scenarios/seller.js';
import { runNewSeller } from './scenarios/new_seller.js';
import { runAdmin } from './scenarios/admin.js';

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5001';
const ADMIN_PHONE = '901111111111'; // seeded SystemAdmin — Baran Açıkgöz
const JSON_HEADERS = { 'Content-Type': 'application/json' };

// ─── Scenarios ────────────────────────────────────────────────────────────────
//
//  buyer      50%  Read-heavy browsing: search stores → products → details
//  seller     30%  Mixed: manage own store, update listings, browse catalog
//  new_seller 15%  Write-heavy onboarding: register → create store → list product → logout
//  admin       5%  Admin catalog management and marketplace monitoring
//
// Total peak: 50 VUs. Ramp: 1 min up → 5 min steady → 30 s down.

export const options = {
  scenarios: {
    buyer: {
      executor: 'ramping-vus',
      exec: 'runBuyer',
      startVUs: 0,
      stages: [
        { duration: '1m', target: 25 },
        { duration: '5m', target: 25 },
        { duration: '30s', target: 0 },
      ],
    },
    seller: {
      executor: 'ramping-vus',
      exec: 'runSeller',
      startVUs: 0,
      stages: [
        { duration: '1m', target: 15 },
        { duration: '5m', target: 15 },
        { duration: '30s', target: 0 },
      ],
    },
    new_seller: {
      executor: 'ramping-vus',
      exec: 'runNewSeller',
      startVUs: 0,
      stages: [
        { duration: '1m', target: 7 },
        { duration: '5m', target: 7 },
        { duration: '30s', target: 0 },
      ],
    },
    admin: {
      executor: 'constant-vus',
      exec: 'runAdmin',
      vus: 3,
      duration: '6m30s',
    },
  },

  thresholds: {
    'http_req_duration{scenario:buyer}': ['p(95)<500'],
    'http_req_duration{scenario:seller}': ['p(95)<800'],
    'http_req_duration{scenario:new_seller}': ['p(95)<1500'],
    'http_req_duration{scenario:admin}': ['p(95)<1000'],
    'http_req_failed': ['rate<0.01'],
  },
};

// ─── Setup ────────────────────────────────────────────────────────────────────
// Runs once before any VU starts. Returns shared data passed to every VU function.
// Responsibilities:
//   1. Login as SystemAdmin (single token reused by admin VUs to avoid OTP race)
//   2. Activate all seeded ProductTemplates so seller/new_seller VUs can list products
//   3. Collect seeded store IDs for buyer/admin scenarios

export function setup() {
  // Wait for the DB seeder to finish — seeder runs async after healthcheck passes
  sleep(10);

  // Admin login
  http.post(`${BASE_URL}/otp/login`,
    JSON.stringify({ phoneNumber: ADMIN_PHONE, captchaToken: 'dummy' }),
    { headers: JSON_HEADERS });

  const loginRes = http.post(`${BASE_URL}/tokens`,
    JSON.stringify({ phoneNumber: ADMIN_PHONE, otp: '123456' }),
    { headers: JSON_HEADERS });

  if (loginRes.status !== 200) {
    throw new Error(`Admin login failed: ${loginRes.status} — ${loginRes.body}`);
  }

  const adminToken = loginRes.json('accessToken');
  const authHeaders = {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${adminToken}`,
  };

  // Fetch seeded product templates and activate each one
  const templateIds = [];
  let retries = 0;
  while (templateIds.length === 0 && retries < 6) {
    if (retries > 0) sleep(5);
    const res = http.get(
      `${BASE_URL}/v1/product-templates/search?PageNumber=1&PageSize=50`,
      { headers: authHeaders },
    );
    if (res.status === 200) {
      const templates = res.json('data') || [];
      for (const t of templates) {
        http.get(`${BASE_URL}/v1/product-templates/${t.id}/activate`, { headers: authHeaders });
        templateIds.push(t.id);
      }
    }
    retries++;
  }

  // Fetch seeded store IDs
  const storeIds = [];
  const storesRes = http.get(
    `${BASE_URL}/v1/stores/search?PageNumber=1&PageSize=50`,
    { headers: authHeaders },
  );
  if (storesRes.status === 200) {
    const stores = storesRes.json('data') || [];
    for (const s of stores) storeIds.push(s.id);
  }

  return { adminToken, templateIds, storeIds };
}

// Re-export scenario functions so k6 can resolve `exec` names
export { runBuyer, runSeller, runNewSeller, runAdmin };

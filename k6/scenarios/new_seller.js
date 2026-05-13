import { check, sleep } from 'k6';
import { post } from '../lib/http.js';
import { sendOtp } from '../lib/auth.js';

// Simulates the full new-user onboarding journey: arrive → register → create store → list first product → leave.
// Each VU+iteration is a completely fresh user, testing the write-heavy registration path under load.
// Phone pattern: 9053 + 4-digit VU + 4-digit ITER  (supports 9999 VUs × 9999 iters = ~100M users)

function phone() {
  return `9053${String(__VU).padStart(4, '0')}${String(__ITER).padStart(4, '0')}`;
}

export function runNewSeller(data) {
  const p = phone();

  // Step 1 — Register
  sendOtp(p);
  const regRes = post('/users/register/self', {
    phoneNumber: p,
    otp: '123456',
    fullName: `New Seller ${p}`,
    birthDate: '20-06-2001',
    captchaToken: 'dummy',
  });
  if (!check(regRes, { 'new_seller — register: 200': r => r.status === 200 })) {
    sleep(2);
    return;
  }

  // Step 2 — Login (OTP must be re-sent; register consumed the previous one)
  sendOtp(p);
  const loginRes = post('/tokens', { phoneNumber: p, otp: '123456' });
  if (!check(loginRes, { 'new_seller — login: 200': r => r.status === 200 })) {
    sleep(2);
    return;
  }
  const token = loginRes.json('accessToken');

  // Step 3 — Create store
  const storeRes = post('/v1/stores/my', {
    name: `Store ${p}`,
    description: 'Freshly opened',
    address: 'Onboarding Avenue 1',
  }, token);
  if (!check(storeRes, { 'new_seller — create store: 200': r => r.status === 200 })) {
    sleep(2);
    return;
  }

  // Step 4 — List first product (requires an active template from setup())
  if (data.templateIds && data.templateIds.length > 0) {
    const templateId = data.templateIds[__ITER % data.templateIds.length];
    const productRes = post('/v1/stores/my/products', {
      productTemplateId: templateId,
      name: 'My First Product',
      description: 'Just listed',
      quantity: 20,
      price: 149.99,
    }, token);
    check(productRes, { 'new_seller — list product: 200': r => r.status === 200 });
  }

  // Step 5 — Logout
  const revokeRes = post('/tokens/revoke', null, token);
  check(revokeRes, { 'new_seller — revoke: 204': r => r.status === 204 });

  sleep(2);
}

import { check, sleep } from 'k6';
import { get } from '../lib/http.js';
import { login, register } from '../lib/auth.js';

// Each buyer VU gets a unique phone: 9051 + 8-digit VU number.
// On first iteration: register (idempotent on re-runs) then login.
// Subsequent iterations: read-heavy marketplace browsing.

function phone() {
  return `9051${String(__VU).padStart(8, '0')}`;
}

let token = null;

function ensureAuth() {
  if (token) return;
  const p = phone();
  register(p, `Buyer VU${__VU}`); // no-op if already registered
  token = login(p);
}

export function runBuyer(data) {
  ensureAuth();
  if (!token) return;

  // Search stores (main discovery surface)
  const storesRes = get('/v1/stores/search', { pageNumber: 1, pageSize: 10 }, token);
  check(storesRes, { 'buyer — search stores: 200': r => r.status === 200 });

  const stores = storesRes.status === 200 ? (storesRes.json('data') || []) : [];

  // Use seeded store IDs as fallback if search returned nothing yet
  const allStoreIds = stores.map(s => s.id).concat(data.storeIds || []);
  if (allStoreIds.length === 0) { sleep(1); return; }

  // Drill into one store
  const storeId = allStoreIds[__ITER % allStoreIds.length];
  const storeRes = get(`/v1/stores/${storeId}`, {}, token);
  check(storeRes, { 'buyer — get store: 200': r => r.status === 200 });

  // Browse products in that store
  const productsRes = get('/v1/products/search', { storeId, pageNumber: 1, pageSize: 10 }, token);
  check(productsRes, { 'buyer — search products: 200': r => r.status === 200 });

  const products = productsRes.status === 200 ? (productsRes.json('data') || []) : [];
  if (products.length > 0) {
    const product = products[__ITER % products.length];
    const productRes = get(`/v1/products/${product.id}`, {}, token);
    check(productRes, { 'buyer — get product: 200': r => r.status === 200 });
  }

  // Browse the product catalog every 3rd iteration
  if (__ITER % 3 === 0) {
    const templatesRes = get('/v1/product-templates/search', { pageNumber: 1, pageSize: 10 }, token);
    check(templatesRes, { 'buyer — search templates: 200': r => r.status === 200 });
  }

  // Check own profile every 10th iteration
  if (__ITER % 10 === 0) {
    const meRes = get('/users/me', {}, token);
    check(meRes, { 'buyer — get me: 200': r => r.status === 200 });
  }

  sleep(1 + Math.random());
}

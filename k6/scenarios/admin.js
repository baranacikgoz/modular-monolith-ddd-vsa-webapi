import { check, sleep } from 'k6';
import { get, post } from '../lib/http.js';

// Admin VUs use the token minted in setup() — no concurrent OTP race.
// Simulates a system admin managing the product catalog and monitoring the marketplace.

export function runAdmin(data) {
  const token = data.adminToken;
  if (!token) return;

  // Create a new product template (expands the catalog)
  const createRes = post('/v1/product-templates', {
    brand: `Brand VU${__VU} Iter${__ITER}`,
    model: `Model ${__ITER}`,
    color: ['Black', 'White', 'Red', 'Blue', 'Green'][__ITER % 5],
  }, token);
  check(createRes, { 'admin — create template: 200': r => r.status === 200 });

  if (createRes.status === 200) {
    const templateId = createRes.json('id');
    const activateRes = get(`/v1/product-templates/${templateId}/activate`, {}, token);
    check(activateRes, { 'admin — activate template: 204': r => r.status === 204 });
  }

  // Monitor the marketplace — browse all stores
  const storesRes = get('/v1/stores/search', { pageNumber: 1, pageSize: 20 }, token);
  check(storesRes, { 'admin — search stores: 200': r => r.status === 200 });

  // User management — search users
  const usersRes = get('/users/search', { pageNumber: 1, pageSize: 20 }, token);
  check(usersRes, { 'admin — search users: 200': r => r.status === 200 });

  // Audit a known store
  if (data.storeIds && data.storeIds.length > 0) {
    const storeId = data.storeIds[__ITER % data.storeIds.length];
    const auditRes = get(`/v1/stores/${storeId}/audit-log`, { pageNumber: 1, pageSize: 10 }, token);
    check(auditRes, { 'admin — store audit log: 200': r => r.status === 200 });
  }

  // Spot-check a product's audit trail every other iteration
  if (__ITER % 2 === 0) {
    const productsRes = get('/v1/products/search', { pageNumber: 1, pageSize: 5 }, token);
    if (productsRes.status === 200) {
      const products = productsRes.json('data') || [];
      if (products.length > 0) {
        const productId = products[0].id;
        const productAuditRes = get(`/v1/products/${productId}/audit-log`, { pageNumber: 1, pageSize: 10 }, token);
        check(productAuditRes, { 'admin — product audit log: 200': r => r.status === 200 });
      }
    }
  }

  sleep(2 + Math.random());
}

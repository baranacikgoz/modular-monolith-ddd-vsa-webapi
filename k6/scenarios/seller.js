import { check, sleep } from 'k6';
import { get, post, put } from '../lib/http.js';
import { login, register } from '../lib/auth.js';

// Each seller VU gets a unique phone: 9052 + 8-digit VU number.
// On first iteration: register + login + create store (or recover existing store).
// Subsequent iterations: manage products — view, update prices, add new listings.

function phone() {
  return `9052${String(__VU).padStart(8, '0')}`;
}

let token = null;
let storeId = null;

function ensureAuth() {
  if (token) return;
  const p = phone();
  register(p, __VU + 100);
  token = login(p);
}

function ensureStore(templateIds) {
  if (storeId) return;

  // Try to create — fails with conflict if store already exists from a previous run
  const createRes = post('/v1/stores/my', {
    name: `Perf Store VU${__VU}`,
    description: 'Performance test store',
    address: 'Benchmark Street 1',
  }, token);

  if (createRes.status === 200) {
    storeId = createRes.json('id');
    // Seed an initial product so update iterations have something to work with
    if (templateIds && templateIds.length > 0) {
      post('/v1/stores/my/products', {
        productTemplateId: templateIds[0],
        name: 'Initial Listing',
        description: 'Seeded for perf test',
        quantity: 100,
        price: 99.99,
      }, token);
    }
    return;
  }

  // Store already exists — retrieve id
  const getRes = get('/v1/stores/my', {}, token);
  if (getRes.status === 200) storeId = getRes.json('id');
}

export default function(data) { runSeller(data || {}); }

export function runSeller(data) {
  ensureAuth();
  if (!token) return;
  ensureStore(data.templateIds);
  if (!storeId) return;

  // Store overview
  const storeRes = get('/v1/stores/my', {}, token);
  check(storeRes, { 'seller — get my store: 200': r => r.status === 200 });

  // Own product list
  const productsRes = get('/v1/products/my/search', { PageNumber: 1, PageSize: 10 }, token);
  check(productsRes, { 'seller — search my products: 200': r => r.status === 200 });

  const products = productsRes.status === 200 ? (productsRes.json('data') || []) : [];

  if (products.length > 0) {
    const product = products[__ITER % products.length];

    // Drill into product
    const productRes = get(`/v1/products/my/${product.id}`, {}, token);
    check(productRes, { 'seller — get my product: 200': r => r.status === 200 });

    // Update price (simulates a seller adjusting to market)
    const newPrice = parseFloat((50 + Math.random() * 950).toFixed(2));
    const updateRes = put(`/v1/products/my/${product.id}`, {
      name: product.name,
      description: product.description,
      quantity: product.quantity,
      price: newPrice,
    }, token);
    check(updateRes, { 'seller — update product: 204': r => r.status === 204 });
  } else {
    // No products yet — add one
    if (data.templateIds && data.templateIds.length > 0) {
      const templateId = data.templateIds[__ITER % data.templateIds.length];
      const addRes = post('/v1/stores/my/products', {
        productTemplateId: templateId,
        name: `Listing ${__ITER}`,
        description: 'Added during perf run',
        quantity: Math.ceil(Math.random() * 50) + 1,
        price: parseFloat((10 + Math.random() * 990).toFixed(2)),
      }, token);
      check(addRes, { 'seller — add product: 200': r => r.status === 200 });
    }
  }

  // Browse catalog to find new templates to list — every 4th iteration
  if (__ITER % 4 === 0) {
    const templatesRes = get('/v1/product-templates/search', { PageNumber: 1, PageSize: 10 }, token);
    check(templatesRes, { 'seller — browse templates: 200': r => r.status === 200 });
  }

  // Check store audit log every 5th iteration
  if (__ITER % 5 === 0) {
    const auditRes = get('/v1/stores/my/audit-log', { PageNumber: 1, PageSize: 10 }, token);
    check(auditRes, { 'seller — audit log: 200': r => r.status === 200 });
  }

  sleep(1 + Math.random() * 0.5);
}

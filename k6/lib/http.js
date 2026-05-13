import http from 'k6/http';

export const BASE_URL = __ENV.BASE_URL || 'http://localhost:5001';

function jsonHeaders(token) {
  const h = { 'Content-Type': 'application/json' };
  if (token) h['Authorization'] = `Bearer ${token}`;
  return h;
}

function bearerHeaders(token) {
  return { 'Authorization': `Bearer ${token}` };
}

export function post(path, body, token) {
  return http.post(
    `${BASE_URL}${path}`,
    body !== null && body !== undefined ? JSON.stringify(body) : null,
    { headers: jsonHeaders(token), tags: { name: path } },
  );
}

export function get(path, params, token) {
  let url = `${BASE_URL}${path}`;
  if (params && Object.keys(params).length > 0) {
    const qs = Object.entries(params).map(([k, v]) => `${encodeURIComponent(k)}=${encodeURIComponent(v)}`).join('&');
    url += `?${qs}`;
  }
  const opts = { tags: { name: path } };
  if (token) opts.headers = bearerHeaders(token);
  return http.get(url, opts);
}

export function put(path, body, token) {
  return http.put(
    `${BASE_URL}${path}`,
    JSON.stringify(body),
    { headers: jsonHeaders(token), tags: { name: path } },
  );
}

export function del(path, token) {
  return http.del(
    `${BASE_URL}${path}`,
    null,
    { headers: bearerHeaders(token), tags: { name: path } },
  );
}

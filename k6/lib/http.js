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
  const opts = { tags: { name: path } };
  if (token) opts.headers = bearerHeaders(token);
  if (params && Object.keys(params).length > 0) opts.params = params;
  return http.get(`${BASE_URL}${path}`, opts);
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

import { check } from 'k6';
import { post } from './http.js';

// DummyOtpService (always active) stores "123456" — no SMS sent.
// Captcha feature flag is false in featureFlags.json — captchaToken is ignored.

export function sendOtp(phone) {
  const res = post('/otp', { phoneNumber: phone, captchaToken: 'dummy' });
  check(res, { 'otp: 204': r => r.status === 204 });
  return res.status === 204;
}

// Returns accessToken string, or null on failure.
export function login(phone) {
  sendOtp(phone);
  const res = post('/tokens', { phoneNumber: phone, otp: '123456' });
  check(res, { 'login: 200': r => r.status === 200 });
  return res.status === 200 ? res.json('accessToken') : null;
}

// Registers user. Returns true on success, false if phone already exists (both are valid for load tests).
export function register(phone, fullName) {
  sendOtp(phone);
  const res = post('/users/register/self', {
    phoneNumber: phone,
    otp: '123456',
    fullName,
    birthDate: '20-06-2001',
    captchaToken: 'dummy',
  });
  return res.status === 200;
}

export function revoke(token) {
  const res = post('/tokens/revoke', null, token);
  check(res, { 'revoke: 204': r => r.status === 204 });
}

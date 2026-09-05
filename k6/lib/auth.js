import { check } from 'k6';
import { post } from './http.js';

// DummyOtpService (always active) stores "123456" — no SMS sent.
// Captcha feature flag is false in featureFlags.json — captchaToken is ignored.

// Name validators require ContainsOnlyTurkishCharacters: no digits, no ASCII-only letters.
const TURKISH_NAMES = [
  'Ahmet Yılmaz', 'Mehmet Kaya', 'Ayşe Demir', 'Fatma Çelik', 'Ali Şahin',
  'Hatice Yıldız', 'Mustafa Aydın', 'Emine Arslan', 'İbrahim Öztürk', 'Zeynep Doğan',
  'Hüseyin Kılıç', 'Elif Aslan', 'Hasan Çetin', 'Meryem Koç', 'Ömer Acar',
  'Şule Duman', 'Yusuf Güneş', 'Gül Polat', 'Süleyman Kuş', 'Merve Erdoğan',
  'Osman Ateş', 'Seda Güler', 'İsmail Özcan', 'Cansu Özdemir', 'Ramazan Bozkurt',
];

export function turkishName(seed) {
  return TURKISH_NAMES[seed % TURKISH_NAMES.length];
}

export function turkishFirstName(seed) {
  return turkishName(seed).split(' ')[0];
}

export function turkishLastName(seed) {
  return turkishName(seed).split(' ')[1];
}

// Every login binds the Keycloak session to a (deviceId, clientId) pair; one synthetic device per VU.
export function device() {
  return { deviceId: `00000000-0000-4000-8000-${String(__VU).padStart(12, '0')}`, clientId: 'mobile-app-1' };
}

export function sendOtpForLogin(phone) {
  const res = post('/otp/login', { phoneNumber: phone, captchaToken: 'dummy' });
  check(res, { 'otp/login: 204': r => r.status === 204 });
  return res.status === 204;
}

export function sendOtpForRegistration(phone) {
  const res = post('/otp/registration', { phoneNumber: phone, captchaToken: 'dummy' });
  check(res, { 'otp/registration: 204': r => r.status === 204 });
  return res.status === 204;
}

// Returns accessToken string, or null on failure.
export function login(phone) {
  sendOtpForLogin(phone);
  const res = post('/tokens', { phoneNumber: phone, otp: '123456', ...device() });
  check(res, { 'login: 200': r => r.status === 200 });
  return res.status === 200 ? res.json('accessToken') : null;
}

// Registers user and auto-logs in. Returns accessToken, or null on failure.
// 409 = already exists — valid for load tests (idempotent seed).
export function register(phone, seed) {
  sendOtpForRegistration(phone);
  const res = post('/users/register/self', {
    phoneNumber: phone,
    otp: '123456',
    firstName: turkishFirstName(seed),
    lastName: turkishLastName(seed),
    birthDate: '20-06-2001',
    captchaToken: 'dummy',
    ...device(),
  });
  check(res, { 'register: 200 or 409': r => r.status === 200 || r.status === 409 });
  return res.status === 200;
}

export function revoke(token) {
  const res = post('/tokens/revoke', null, token);
  check(res, { 'revoke: 204': r => r.status === 204 });
}

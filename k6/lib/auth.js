import { check } from 'k6';
import { post } from './http.js';

// DummyOtpService (always active) stores "123456" — no SMS sent.
// Captcha feature flag is false in featureFlags.json — captchaToken is ignored.

// FullName validator requires ContainsOnlyTurkishCharacters — no digits, no ASCII-only letters.
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

// Registers user. 200 = new user, 409 = already exists — both valid for load tests.
export function register(phone, seed) {
  sendOtp(phone);
  const res = post('/users/register/self', {
    phoneNumber: phone,
    otp: '123456',
    fullName: turkishName(seed),
    birthDate: '20-06-2001',
    captchaToken: 'dummy',
  });
  check(res, { 'register: 200 or 409': r => r.status === 200 || r.status === 409 });
  return res.status === 200;
}

export function revoke(token) {
  const res = post('/tokens/revoke', null, token);
  check(res, { 'revoke: 204': r => r.status === 204 });
}

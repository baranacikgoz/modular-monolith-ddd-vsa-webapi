import { check, sleep } from 'k6';
import { post } from '../lib/http.js';
import { sendOtpForRegistration, turkishName } from '../lib/auth.js';

// Registers N users to generate UserRegisteredIntegrationEvent bursts.
// Each event triggers UserRegisteredSignalRHandler → hub dispatch → real-time notification.
// Run while the POC UI is open at http://localhost:5001/signalr-poc.html to see notifications arrive.
//
// Usage:
//   k6 run k6/scenarios/signalr_poc.js
//   k6 run k6/scenarios/signalr_poc.js --vus 20 --duration 30s

export const options = {
  vus: 5,
  duration: '20s',
};

// Phone: 9099 + 3-digit VU + 4-digit ITER (avoids collision with other scenarios)
function phone() {
  return `9099${String(__VU).padStart(3, '0')}${String(__ITER).padStart(4, '0')}`;
}

export default function () {
  const p = phone();

  sendOtpForRegistration(p);

  const res = post('/users/register/self', {
    phoneNumber: p,
    otp: '123456',
    fullName: turkishName(__VU + __ITER),
    birthDate: '20-06-2001',
    captchaToken: 'dummy',
  });

  check(res, { 'register: 200': r => r.status === 200 });

  sleep(1);
}

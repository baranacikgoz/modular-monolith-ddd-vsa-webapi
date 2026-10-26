(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, svg = MM.svg, C = MM.colors, T = MM.t;

  var NODES = {
    IAM: { x: 165, y: 120, w: 140, h: 58, sub: 'Keycloak broker' },
    Products: { x: 395, y: 120, w: 140, h: 58, sub: 'stores · products' },
    Inventory: { x: 395, y: 340, w: 140, h: 58, sub: 'stock · reservations' },
    Notifications: { x: 165, y: 340, w: 140, h: 58, sub: 'SMS · email · push' },
    Outbox: { x: 280, y: 34, w: 130, h: 42, core: true, sub: 'core · always loads' },
    BackgroundJobs: { x: 280, y: 436, w: 150, h: 42, core: true, sub: 'core · always loads' }
  };
  var HUB = { x: 280, y: 230, w: 112, h: 30 };

  var EDGES = [
    { id: 'e1', a: 'IAM', b: 'Notifications', type: 'req', x: 151, label: { en: 'OTP, sessions, alerts', tr: 'OTP, oturum, uyarı' } },
    { id: 'e2', a: 'Notifications', b: 'IAM', type: 'req', x: 179, label: { en: 'active session ids', tr: 'aktif oturum id\'leri' } },
    { id: 'e3', a: 'Products', b: 'Inventory', type: 'event', x: 381, label: { en: 'ProductCreatedIntegrationEvent', tr: 'ProductCreatedIntegrationEvent' } },
    { id: 'e4', a: 'Products', b: 'Inventory', type: 'req', x: 395, label: { en: 'stock level', tr: 'stok seviyesi' } },
    { id: 'e5', a: 'Inventory', b: 'Products', type: 'req', x: 409, label: { en: 'product and store status', tr: 'ürün ve mağaza durumu' } },
    { id: 'e6', a: 'Products', b: 'IAM', type: 'req', horizontal: true, y: 120, label: { en: 'seed user ids', tr: 'tohum kullanıcı id\'leri' } }
  ];

  var DATA = {
    IAM: {
      kind: { en: 'optional · starts 3rd', tr: 'isteğe bağlı · 3. başlar' },
      desc: { en: 'Login, registration, tokens, sessions and permissions, brokered through Keycloak. Phone with an SMS code, or email with a password.',
        tr: 'Giriş, kayıt, token, oturum ve izinler Keycloak üzerinden yönetilir. SMS kodlu telefon ya da parolalı e-posta.' },
      db: { en: 'none, Keycloak is the identity store', tr: 'yok, kimlik deposu Keycloak' },
      routes: [['POST', '/otp/login', 'anonymous'], ['POST', '/tokens', 'anonymous'], ['POST', '/tokens/refresh', 'anonymous'], ['GET', '/users/me', 'users:view-own'], ['POST', '/tokens/sessions/revoke-all', 'sessions:revoke-own']],
      serves: ['GetSeedUserIds', 'GetActiveSessionIds', 'GetUsersInRolePage'],
      calls: [{ en: 'Notifications: OTP, verification tokens, device sessions, alerts', tr: 'Notifications: OTP, doğrulama tokenları, cihaz oturumları, uyarılar' }],
      test: 'make test-iam'
    },
    Products: {
      kind: { en: 'optional · starts 5th', tr: 'isteğe bağlı · 5. başlar' },
      desc: { en: 'Stores, product templates and products. The exemplar module: copy its shape when you add a new one.',
        tr: 'Mağazalar, ürün şablonları ve ürünler. Örnek modül: yenisini eklerken şeklini kopyala.' },
      db: { en: 'schema Products', tr: 'Products şeması' },
      routes: [['POST', '/v1/stores/my/products', 'products:create-own'], ['GET', '/v1/products/search', 'products:search'], ['POST', '/v1/product-templates/{id}/activate', 'product-templates:update'], ['GET', '/v1/probe/cross-module', 'anonymous']],
      publishes: ['ProductCreatedIntegrationEvent', 'StoreCreatedIntegrationEvent'],
      serves: ['GetProduct'],
      calls: ['GetStockLevel', 'GetSeedUserIds'],
      test: 'make test-products'
    },
    Inventory: {
      kind: { en: 'optional · starts 6th', tr: 'isteğe bağlı · 6. başlar' },
      desc: { en: 'Stock levels and reservations. A saga-shaped StockReservation moves through Active, Committed, Released, Expired and RequiresReconciliation.',
        tr: 'Stok seviyeleri ve rezervasyonlar. Saga biçimli StockReservation; Active, Committed, Released, Expired ve RequiresReconciliation durumlarından geçer.' },
      db: { en: 'schema Inventory', tr: 'Inventory şeması' },
      routes: [['POST', '/v1/stock-reservations', 'stock-reservations:create'], ['POST', '/v1/stock-reservations/{id}/commit', 'stock-reservations:update'], ['POST', '/v1/stock-reservations/{id}/release', 'stock-reservations:update'], ['POST', '/v1/stock-reservations/webhooks/warehouse', 'HMAC signature']],
      consumes: ['ProductCreatedIntegrationEvent'],
      serves: ['GetStockLevel'],
      calls: ['GetProduct'],
      test: 'make test-inventory'
    },
    Notifications: {
      kind: { en: 'optional · starts 4th', tr: 'isteğe bağlı · 4. başlar' },
      desc: { en: 'Everything that reaches a person: SMS, email, push, a SignalR hub, one-time codes and the device registry.',
        tr: 'Bir kişiye ulaşan her şey: SMS, e-posta, push, SignalR hub\'ı, tek kullanımlık kodlar ve cihaz kaydı.' },
      db: { en: 'schema Notifications', tr: 'Notifications şeması' },
      routes: [['PUT', '/notifications/devices/current/push-token', 'devices:update-own'], ['WS', '/hubs/notifications', 'any signed-in user']],
      serves: ['SendPhoneOtp', 'VerifyPhoneOtp', 'SendEmailOtp', 'VerifyEmailOtp', 'IssueVerificationToken', 'SendSecurityAlert', 'BindDeviceSession', 'DeactivateDeviceSessions', 'GetDeviceSessions'],
      calls: ['GetActiveSessionIds'],
      test: 'make test-notifications'
    },
    Outbox: {
      kind: { en: 'core · starts 2nd · always loads', tr: 'çekirdek · 2. başlar · her zaman yüklenir' },
      desc: { en: 'The platform side of reliable messaging. It runs on every instance because the write path of every other module depends on it.',
        tr: 'Güvenilir mesajlaşmanın platform tarafı. Diğer her modülün yazma yolu ona bağlı olduğu için her örnekte çalışır.' },
      db: { en: 'schema Outbox', tr: 'Outbox şeması' },
      jobs: ['OutboxProcessor', 'OutboxCleanupJob', 'OutboxMetricsJob', 'InboxCleanupJob'],
      test: 'make test-outbox'
    },
    BackgroundJobs: {
      kind: { en: 'core · starts 1st · always loads', tr: 'çekirdek · 1. başlar · her zaman yüklenir' },
      desc: { en: 'The Hangfire server and everything around it: the dashboard, job metrics and a filter that stops a recurring job from overlapping itself.',
        tr: 'Hangfire sunucusu ve çevresindeki her şey: pano, iş metrikleri ve tekrarlayan bir işin kendi üzerine binmesini engelleyen filtre.' },
      db: { en: 'schema BackgroundJobs, created by Hangfire', tr: 'BackgroundJobs şeması, Hangfire tarafından oluşturulur' },
      routes: [['GET', '/hangfire', 'hangfire:manage']],
      jobs: ['SkipOverlappingRecurringJobFilter', 'JobMetricsFilter'],
      test: 'make test-backgroundjobs'
    }
  };

  var map = $('#atlasSvg'), dossier = $('#dossier');
  var sel = 'Products';
  var nodeEls = {}, edgeEls = {};

  function arrowHead(tx, ty, ux, uy) {
    var bx = tx - ux * 8, by = ty - uy * 8, px = -uy * 4.5, py = ux * 4.5;
    return 'M' + tx + ' ' + ty + ' L' + (bx + px) + ' ' + (by + py) + ' L' + (bx - px) + ' ' + (by - py) + ' Z';
  }

  function edgeGeom(e) {
    var A = NODES[e.a], B = NODES[e.b];
    if (e.horizontal) {
      var dir = B.x < A.x ? -1 : 1;
      return { x1: A.x + dir * A.w / 2, y1: e.y, x2: B.x - dir * B.w / 2, y2: e.y, ux: dir, uy: 0 };
    }
    var d = B.y > A.y ? 1 : -1;
    return { x1: e.x, y1: A.y + d * A.h / 2, x2: e.x, y2: B.y - d * B.h / 2, ux: 0, uy: d };
  }

  function draw() {
    map.replaceChildren();
    var spokes = svg('g');
    ['IAM', 'Products', 'Inventory', 'Notifications', 'Outbox'].forEach(function (k) {
      spokes.appendChild(svg('line', { class: 'spoke', x1: HUB.x, y1: HUB.y, x2: NODES[k].x, y2: NODES[k].y }));
    });
    map.appendChild(spokes);
    map.appendChild(svg('g', { class: 'hub' },
      svg('rect', { x: HUB.x - HUB.w / 2, y: HUB.y - HUB.h / 2, width: HUB.w, height: HUB.h, rx: 15 }),
      svg('text', { x: HUB.x, y: HUB.y + 4 }, 'RabbitMQ')));

    EDGES.forEach(function (e) {
      var g = edgeGeom(e);
      var line = svg('path', { class: 'edge ' + (e.type === 'event' ? 'ev' : 'req'), d: 'M' + g.x1 + ' ' + g.y1 + ' L' + (g.x2 - g.ux * 6) + ' ' + (g.y2 - g.uy * 6) });
      var head = svg('path', { class: 'eh', d: arrowHead(g.x2, g.y2, g.ux, g.uy) });
      var hit = svg('path', { d: 'M' + g.x1 + ' ' + g.y1 + ' L' + g.x2 + ' ' + g.y2, stroke: 'transparent', 'stroke-width': 12, fill: 'none' });
      var title = svg('title');
      title.textContent = e.a + ' → ' + e.b + ': ' + T(e.label) + (e.type === 'event' ? ' (event)' : ' (request)');
      var grp = svg('g', { class: 'eg' }, line, head, hit, title);
      map.appendChild(grp);
      edgeEls[e.id] = { g: grp, line: line };
    });

    Object.keys(NODES).forEach(function (k) {
      var n = NODES[k];
      var g = svg('g', { class: 'node' + (n.core ? ' core' : ''), style: '--c:' + C[k], tabindex: 0, role: 'button', 'aria-label': k },
        svg('rect', { x: n.x - n.w / 2, y: n.y - n.h / 2, width: n.w, height: n.h, rx: n.core ? 12 : 14 }),
        svg('text', { x: n.x, y: n.y + (n.core ? -2 : -3) }, k),
        svg('text', { class: 'sub', x: n.x, y: n.y + (n.core ? 13 : 14) }, n.sub));
      g.addEventListener('click', function () { select(k); });
      g.addEventListener('keydown', function (ev) {
        if (ev.key === 'Enter' || ev.key === ' ') { ev.preventDefault(); select(k); }
      });
      map.appendChild(g);
      nodeEls[k] = g;
    });
  }

  function row(label, content) {
    return [el('dt', { text: label }), el('dd', null, content)];
  }

  function tags(list, cls) {
    return list.map(function (t) { return el('span', { class: 'tag' + (cls ? ' ' + cls : ''), text: T(t) }); });
  }

  function contracts(k) {
    var out = [];
    EDGES.forEach(function (e) {
      if (e.a !== k && e.b !== k) return;
      var out1 = e.a === k;
      out.push(el('span', { class: 'tag' + (e.type === 'event' ? ' ev' : '') },
        (out1 ? '→ ' + e.b : '← ' + e.a) + ' · ' + T(e.label)));
    });
    return out;
  }

  function renderDossier() {
    var d = DATA[sel], rows = [];
    rows.push(row(T({ en: 'Database', tr: 'Veritabanı' }), el('span', { class: 'tag', text: T(d.db) })));
    if (d.routes) {
      rows.push(row(T({ en: 'Routes', tr: 'Rotalar' }), el('ul', { class: 'routes' }, d.routes.map(function (r) {
        return el('li', null, el('b', { text: r[0] }), r[1], el('i', { text: r[2] }));
      }))));
    }
    if (d.jobs) rows.push(row(T({ en: 'Runs', tr: 'Çalıştırır' }), tags(d.jobs)));
    if (d.publishes) rows.push(row(T({ en: 'Publishes', tr: 'Yayınlar' }), tags(d.publishes, 'ev')));
    if (d.consumes) rows.push(row(T({ en: 'Consumes', tr: 'Tüketir' }), tags(d.consumes, 'ev')));
    if (d.serves) rows.push(row(T({ en: 'Serves', tr: 'Sunar' }), tags(d.serves)));
    if (d.calls) rows.push(row(T({ en: 'Calls', tr: 'Çağırır' }), tags(d.calls)));
    var c = contracts(sel);
    if (c.length) rows.push(row(T({ en: 'Contracts', tr: 'Sözleşmeler' }), c));
    rows.push(row(T({ en: 'Test', tr: 'Test' }), el('span', { class: 'tag', text: d.test })));
    dossier.style.setProperty('--c', C[sel]);
    dossier.replaceChildren(
      el('div', { class: 'dos-head' }, el('h3', { class: 'dos-name', text: sel }), el('span', { class: 'dos-kind', text: T(d.kind) })),
      el('p', { class: 'dos-desc', text: T(d.desc) }),
      el('dl', { class: 'dos-grid' }, rows));
  }

  function paintSelection() {
    Object.keys(nodeEls).forEach(function (k) {
      nodeEls[k].classList.toggle('sel', k === sel);
    });
    EDGES.forEach(function (e) {
      var on = e.a === sel || e.b === sel;
      edgeEls[e.id].g.classList.toggle('on', on);
      edgeEls[e.id].line.classList.toggle('on', on);
    });
  }

  function select(k) {
    sel = k;
    paintSelection();
    renderDossier();
  }

  draw();
  select(sel);
  MM.onLang(function () { draw(); select(sel); });
})();

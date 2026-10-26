(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, svg = MM.svg, C = MM.colors, T = MM.t;

  var LANES = [
    { id: 'seller', name: { en: 'Seller', tr: 'Satıcı' }, color: '#ece7db' },
    { id: 'api', name: { en: 'Products API', tr: 'Products API' }, color: C.Products },
    { id: 'agg', name: { en: 'Store aggregate', tr: 'Store aggregate' }, color: C.Products },
    { id: 'db', name: { en: 'PostgreSQL', tr: 'PostgreSQL' }, color: '#8fb4ff' },
    { id: 'out', name: { en: 'OutboxProcessor', tr: 'OutboxProcessor' }, color: C.Outbox },
    { id: 'mq', name: { en: 'RabbitMQ', tr: 'RabbitMQ' }, color: C.signal },
    { id: 'inv', name: { en: 'Inventory consumer', tr: 'Inventory tüketici' }, color: C.Inventory }
  ];
  var IDX = {};
  LANES.forEach(function (l, i) { IDX[l.id] = i; });
  var X = LANES.map(function (_, i) { return 78 + i * 141; });
  var Y0 = 86, PITCH = 42;

  var LEDGER = [
    { k: 'prod', name: 'Products', sub: function (s) { return T({ en: 'product rows', tr: 'ürün satırı' }); }, n: function (s) { return s.prod; } },
    { k: 'outb', name: 'OutboxMessages', n: function (s) { return s.outb + s.sent; },
      sub: function (s) { return T({ en: s.outb + ' pending · ' + s.sent + ' published', tr: s.outb + ' bekliyor · ' + s.sent + ' yayınlandı' }); } },
    { k: 'audit', name: 'AuditLog', sub: function () { return T({ en: 'domain events stored', tr: 'saklanan domain olayı' }); }, n: function (s) { return s.audit; } },
    { k: 'stock', name: 'StockLevel', sub: function () { return T({ en: 'in Inventory', tr: 'Inventory içinde' }); }, n: function (s) { return s.stock; } },
    { k: 'inbox', name: 'ProcessedMessages', sub: function () { return T({ en: 'consumer inbox', tr: 'tüketici inbox' }); }, n: function (s) { return s.inbox; } }
  ];

  var S = { script: [], groups: [], pos: 0, auto: false, busy: false, token: 0, led: null, deadY: {} };
  var seq = $('#seq'), narr = $('#narr'), verdict = $('#verdict'), ledgerEl = $('#ledger');
  var chA = $('#chA'), chB = $('#chB'), chC = $('#chC');
  var btnPlay = $('#flPlay'), btnStep = $('#flStep'), btnReset = $('#flReset');
  var ledEls = {};

  function flags() { return { A: chA.checked, B: chB.checked, C: chC.checked }; }

  function buildScript(f) {
    var s = [];
    s.push({ from: 'seller', to: 'api', label: 'POST /stores/my/products',
      cap: { en: 'A seller sends the request. So far it is plain HTTP.', tr: 'Satıcı isteği gönderir. Şimdilik düz HTTP.' } });
    s.push({ from: 'api', to: 'api', label: { en: 'validate + scope check', tr: 'doğrula + scope kontrolü' },
      cap: { en: 'The request is validated and Keycloak is asked for the products:create-own decision. A failure here short-circuits into a localized ProblemDetails.',
        tr: 'İstek doğrulanır ve Keycloak\'a products:create-own kararı sorulur. Buradaki bir hata, yerelleştirilmiş bir ProblemDetails ile hemen sonuçlanır.' } });
    s.push({ from: 'api', to: 'agg', label: 'Create + AddProduct',
      cap: { en: 'The endpoint calls the aggregate. Product.Create and store.AddProduct enforce the invariants.',
        tr: 'Uç nokta aggregate\'i çağırır. Product.Create ve store.AddProduct kuralları uygular.' } });
    s.push({ from: 'agg', to: 'api', label: { en: 'domain events raised', tr: 'domain olayları' },
      cap: { en: 'The aggregate hands back the domain events it raised. Nothing has been published and nothing has been saved yet.',
        tr: 'Aggregate, ürettiği domain olaylarını geri verir. Henüz hiçbir şey yayınlanmadı ve kaydedilmedi.' } });

    if (f.A) {
      s.push({ from: 'api', to: 'db', label: 'SaveChangesAsync', fail: true, kill: 'api',
        cap: { en: 'The process dies before the transaction commits. The database rolls back: no product, no outbox row, no audit row.',
          tr: 'Süreç, transaction commit edilmeden önce ölür. Veritabanı geri alır: ürün yok, outbox satırı yok, audit satırı yok.' } });
      return s;
    }

    s.push({ from: 'api', to: 'db', label: { en: 'SaveChanges (1 tx)', tr: 'SaveChanges (1 tx)' }, led: { prod: 1, outb: 1, audit: 1 }, kill: f.B ? 'api' : null,
      cap: f.B
        ? { en: 'One transaction commits the product, an OutboxMessages row and an AuditLog row. Then the API dies. The event row is already safe, because it was written in the same transaction as the product.',
            tr: 'Tek transaction ürünü, bir OutboxMessages satırını ve bir AuditLog satırını commit eder. Sonra API ölür. Olay satırı zaten güvendedir, çünkü ürünle aynı transaction içinde yazıldı.' }
        : { en: 'One SaveChangesAsync, one transaction. The product row, an OutboxMessages row and an AuditLog row commit together. There is no dual write.',
            tr: 'Tek SaveChangesAsync, tek transaction. Ürün satırı, bir OutboxMessages satırı ve bir AuditLog satırı birlikte commit edilir. İkili yazma yoktur.' } });
    s.push({ from: 'out', to: 'db', label: 'claim (SKIP LOCKED)',
      cap: f.B
        ? { en: 'The OutboxProcessor does not care that the API is gone. It claims a batch with FOR UPDATE SKIP LOCKED and finds the committed row.',
            tr: 'OutboxProcessor, API\'nin gittiğini umursamaz. FOR UPDATE SKIP LOCKED ile bir grubu sahiplenir ve commit edilmiş satırı bulur.' }
        : { en: 'The OutboxProcessor claims a batch with FOR UPDATE SKIP LOCKED. Several processors can run at once and they claim disjoint rows.',
            tr: 'OutboxProcessor, FOR UPDATE SKIP LOCKED ile bir grubu sahiplenir. Birden çok işlemci aynı anda çalışabilir ve ayrık satırları alır.' } });
    s.push({ from: 'out', to: 'mq', label: 'ProductCreated…Event', led: { outb: -1, sent: 1 },
      cap: { en: 'It publishes ProductCreatedIntegrationEvent to RabbitMQ. Publishing is at least once, and the trace context travels with the message.',
        tr: 'ProductCreatedIntegrationEvent olayını RabbitMQ\'ya yayınlar. Yayın en az bir kez olur ve izleme bağlamı mesajla birlikte taşınır.' } });
    s.push({ from: 'mq', to: 'inv', label: { en: 'deliver (maybe twice)', tr: 'teslim (belki iki kez)' },
      cap: { en: 'RabbitMQ delivers the event to the Inventory consumer.', tr: 'RabbitMQ olayı Inventory tüketicisine teslim eder.' } });
    s.push({ from: 'inv', to: 'inv', label: { en: 'pre-filter + inbox', tr: 'ön filtre + inbox' },
      cap: { en: 'The consumer inherits IntegrationEventHandlerBase. A FusionCache key is a fast pre-filter, then a ProcessedMessage row is tracked in the module\'s own DbContext.',
        tr: 'Tüketici IntegrationEventHandlerBase sınıfından türer. FusionCache anahtarı hızlı bir ön filtredir, ardından modülün kendi DbContext\'inde bir ProcessedMessage satırı izlenir.' } });
    s.push({ from: 'inv', to: 'db', label: { en: 'StockLevel + inbox row', tr: 'StockLevel + inbox satırı' }, led: { stock: 1, inbox: 1 },
      cap: { en: 'A StockLevel is created and committed together with the inbox row. Exactly one effect, no matter how often the message comes.',
        tr: 'Bir StockLevel oluşturulur ve inbox satırıyla birlikte commit edilir. Mesaj kaç kez gelirse gelsin tek bir etki olur.' } });

    if (f.C) {
      s.push({ from: 'mq', to: 'inv', label: { en: 'redelivery', tr: 'yeniden teslim' }, dup: true,
        cap: { en: 'The same message arrives again. It happens when a processor crashes between claiming and publishing: the lease expires and another instance publishes the row.',
          tr: 'Aynı mesaj yeniden gelir. Bir işlemci sahiplenme ile yayın arasında çökerse olur: kira süresi dolar ve başka bir örnek satırı yayınlar.' } });
      s.push({ from: 'inv', to: 'inv', label: { en: 'duplicate, ack', tr: 'tekrar, ack' }, dup: true,
        cap: { en: 'The FusionCache key and the inbox row recognize it. The redelivery is acknowledged as a duplicate and changes nothing.',
          tr: 'FusionCache anahtarı ve inbox satırı mesajı tanır. Yeniden teslim, tekrar olarak onaylanır ve hiçbir şeyi değiştirmez.' } });
    }
    return s;
  }

  function laneColor(id) { return LANES[IDX[id]].color; }

  function head(x, y, dir) {
    return 'M' + x + ' ' + y + ' L' + (x - 8 * dir) + ' ' + (y - 4.5) + ' L' + (x - 8 * dir) + ' ' + (y + 4.5) + ' Z';
  }

  function buildGroup(st, i) {
    var y = Y0 + i * PITCH, x1 = X[IDX[st.from]], x2 = X[IDX[st.to]];
    var self = st.from === st.to, d, tip, tipDir, lx, ly, anchor = 'middle';
    var labelText = T(st.label);
    if (self) {
      var sd = IDX[st.from] === LANES.length - 1 ? -1 : 1;
      d = 'M' + x1 + ' ' + (y - 8) + ' h' + (sd * 34) + ' v22 h' + (-sd * 34);
      tip = [x1, y + 14];
      tipDir = -sd;
      lx = x1 + sd * 44;
      ly = y + 10;
      anchor = sd > 0 ? 'start' : 'end';
    } else {
      var dir = x2 >= x1 ? 1 : -1;
      d = 'M' + x1 + ' ' + y + ' L' + (x2 - dir * 2) + ' ' + y;
      tip = [x2, y];
      tipDir = dir;
      lx = (x1 + x2) / 2;
      ly = y - 8;
    }
    var g = svg('g', { class: 'sq-msg sq-ghost', style: '--c:' + laneColor(st.from) });
    var bg = svg('rect', { class: 'sq-bg', rx: 4 });
    var text = svg('text', { x: lx, y: ly, 'text-anchor': anchor, class: 'sq-lbl' });
    text.textContent = labelText;
    var path = svg('path', { class: 'sq-line', d: d });
    var arrow = svg('path', { class: 'sq-arrow', d: head(tip[0], tip[1], tipDir), opacity: 0 });
    g.appendChild(path);
    g.appendChild(arrow);
    g.appendChild(bg);
    g.appendChild(text);
    return { g: g, path: path, arrow: arrow, bg: bg, text: text, y: y, x1: x1, x2: x2, self: self };
  }

  function layout() {
    var rows = S.script.length, H = Y0 + rows * PITCH + 18;
    seq.setAttribute('viewBox', '0 0 1000 ' + H);
    seq.replaceChildren();
    LANES.forEach(function (l, i) {
      seq.appendChild(svg('line', { class: 'sq-life', x1: X[i], x2: X[i], y1: 44, y2: H - 6 }));
    });
    LANES.forEach(function (l, i) {
      var w = 128;
      seq.appendChild(svg('rect', { class: 'sq-head', x: X[i] - w / 2, y: 8, width: w, height: 34, rx: 9, style: 'stroke:' + l.color + ';stroke-opacity:.55' }));
      var t = svg('text', { class: 'sq-head-t', x: X[i], y: 29 });
      t.textContent = T(l.name);
      seq.appendChild(t);
    });
    S.groups = S.script.map(buildGroup);
    S.groups.forEach(function (o) { seq.appendChild(o.g); });
    S.dot = svg('circle', { class: 'sq-dot', r: 5.5, cx: 0, cy: 0, opacity: 0 });
    seq.appendChild(S.dot);
    S.groups.forEach(function (o) {
      var b = o.text.getBBox();
      o.bg.setAttribute('x', b.x - 5);
      o.bg.setAttribute('y', b.y - 1);
      o.bg.setAttribute('width', b.width + 10);
      o.bg.setAttribute('height', b.height + 2);
    });
    S.deadY = {};
  }

  function newLedger() { return { prod: 0, outb: 0, sent: 0, audit: 0, stock: 0, inbox: 0 }; }

  function buildLedger() {
    ledgerEl.replaceChildren();
    LEDGER.forEach(function (c) {
      var n = el('span', { class: 'led-n', text: '0' });
      var sub = el('span', { class: 'led-s' });
      var box = el('div', { class: 'led' }, n, el('span', { class: 'led-l', text: c.name }), sub);
      ledEls[c.k] = { box: box, n: n, sub: sub };
      ledgerEl.appendChild(box);
    });
  }

  function paintLedger(flash) {
    LEDGER.forEach(function (c) {
      var o = ledEls[c.k];
      o.n.textContent = String(c.n(S.led));
      o.sub.textContent = c.sub(S.led);
      if (flash && flash.indexOf(c.k) >= 0) {
        o.box.classList.remove('flash');
        void o.box.offsetWidth;
        o.box.classList.add('flash');
      }
    });
  }

  function applyLedger(delta) {
    var changed = [];
    Object.keys(delta).forEach(function (k) {
      S.led[k] += delta[k];
      if (k === 'sent') changed.push('outb'); else changed.push(k);
    });
    paintLedger(changed);
  }

  function setNarr(i) {
    var st = S.script[i];
    narr.replaceChildren(
      el('small', { text: T({ en: 'Hop ', tr: 'Adım ' }) + (i + 1) + ' / ' + S.script.length }),
      document.createTextNode(T(st.cap)));
  }

  function idleNarr() {
    narr.replaceChildren(el('small', { text: T({ en: 'Ready', tr: 'Hazır' }) }),
      document.createTextNode(T({
        en: 'Press play, or step through the hops one at a time. Switch on a failure first to see which guarantee catches it.',
        tr: 'Oynat\'a bas ya da adımları tek tek ilerlet. Hangi garantinin devreye girdiğini görmek için önce bir arıza aç.' })));
  }

  function tween(dur, fn) {
    return new Promise(function (res) {
      var t0 = performance.now();
      (function f(now) {
        var p = MM.clamp((now - t0) / dur, 0, 1);
        fn(p);
        if (p < 1) requestAnimationFrame(f); else res();
      })(t0);
    });
  }

  function markDead(laneId, y) {
    var i = IDX[laneId], H = Y0 + S.script.length * PITCH + 18;
    seq.insertBefore(svg('line', { class: 'sq-life dead', x1: X[i], x2: X[i], y1: y, y2: H - 6 }), S.groups[0].g);
    var cx = X[i], cy = y;
    var k = 6;
    var x = svg('g', null,
      svg('line', { class: 'sq-x', x1: cx - k, y1: cy - k, x2: cx + k, y2: cy + k }),
      svg('line', { class: 'sq-x', x1: cx - k, y1: cy + k, x2: cx + k, y2: cy - k }));
    seq.appendChild(x);
  }

  async function animateStep(i) {
    var o = S.groups[i], st = S.script[i];
    var len = o.path.getTotalLength();
    var dur = MM.reduced ? 80 : (o.self ? 650 : 480 + Math.abs(o.x2 - o.x1) * 1.1);
    var stopAt = st.fail ? 0.6 : 1;
    o.g.classList.remove('sq-ghost');
    o.g.classList.add('on');
    o.path.style.strokeDasharray = len;
    o.path.style.strokeDashoffset = len;
    S.dot.setAttribute('opacity', 1);
    S.dot.style.setProperty('--c', laneColor(st.from));
    await tween(dur, function (p) {
      var q = p * stopAt;
      o.path.style.strokeDashoffset = len * (1 - q);
      var pt = o.path.getPointAtLength(len * q);
      S.dot.setAttribute('cx', pt.x);
      S.dot.setAttribute('cy', pt.y);
    });
    S.dot.setAttribute('opacity', 0);
    o.path.style.strokeDasharray = '';
    o.path.style.strokeDashoffset = '';
    o.g.classList.remove('on');
    if (st.fail) {
      o.g.classList.add('fail');
      var pt = o.path.getPointAtLength(len * stopAt);
      var k = 7;
      seq.appendChild(svg('g', null,
        svg('line', { class: 'sq-x', x1: pt.x - k, y1: pt.y - k, x2: pt.x + k, y2: pt.y + k }),
        svg('line', { class: 'sq-x', x1: pt.x - k, y1: pt.y + k, x2: pt.x + k, y2: pt.y - k })));
    } else {
      o.g.classList.add('done');
      o.arrow.setAttribute('opacity', 1);
    }
    if (st.kill) markDead(st.kill, st.fail ? o.y + 14 : o.y + 22);
  }

  function setVerdict() {
    var f = flags(), parts = [], cls = 'ok';
    if (f.A) {
      parts.push(T({ en: 'Consistent. No product, no outbox row, no event: the transaction commits everything or nothing.',
        tr: 'Tutarlı. Ürün yok, outbox satırı yok, olay yok: transaction ya her şeyi commit eder ya da hiçbir şeyi.' }));
    } else {
      if (f.B) parts.push(T({ en: 'The event survived the crash because it was committed with the product.', tr: 'Olay, ürünle birlikte commit edildiği için çökmeden sağ çıktı.' }));
      if (f.C) parts.push(T({ en: 'Delivered twice, applied once: one StockLevel, one inbox row.', tr: 'İki kez teslim edildi, bir kez uygulandı: tek StockLevel, tek inbox satırı.' }));
      if (!f.B && !f.C) parts.push(T({ en: 'Committed once, published once, applied once.', tr: 'Bir kez commit edildi, bir kez yayınlandı, bir kez uygulandı.' }));
    }
    verdict.className = 'verdict ' + cls;
    verdict.textContent = '✓ ' + parts.join(' ');
  }

  async function playOne() {
    if (S.busy || S.pos >= S.script.length) return;
    S.busy = true;
    var i = S.pos;
    S.pos += 1;
    var token = S.token;
    setNarr(i);
    await animateStep(i);
    if (token !== S.token) return;
    var st = S.script[i];
    if (st.led) applyLedger(st.led);
    S.busy = false;
    if (S.pos >= S.script.length) { setVerdict(); syncButtons(); }
  }

  async function playAll() {
    var token = S.token;
    S.auto = true;
    syncButtons();
    while (S.auto && S.pos < S.script.length && token === S.token) {
      await playOne();
      if (token !== S.token) return;
      await MM.sleep(520);
    }
    S.auto = false;
    syncButtons();
  }

  function syncButtons() {
    var done = S.pos >= S.script.length;
    btnPlay.textContent = S.auto ? T({ en: 'Pause', tr: 'Duraklat' }) : (done ? T({ en: 'Replay', tr: 'Tekrar' }) : (S.pos ? T({ en: 'Resume', tr: 'Devam' }) : T({ en: 'Play', tr: 'Oynat' })));
    btnStep.disabled = S.auto || S.busy || done;
  }

  function reset() {
    S.token += 1;
    S.auto = false;
    S.busy = false;
    S.pos = 0;
    S.led = newLedger();
    S.script = buildScript(flags());
    layout();
    paintLedger();
    idleNarr();
    verdict.textContent = '';
    verdict.className = 'verdict';
    syncButtons();
  }

  function onChaos(e) {
    if (chA.checked) {
      chB.checked = false;
      chC.checked = false;
    }
    chB.disabled = chA.checked;
    chC.disabled = chA.checked;
    chB.parentNode.style.opacity = chA.checked ? 0.4 : '';
    chC.parentNode.style.opacity = chA.checked ? 0.4 : '';
    reset();
  }

  btnPlay.addEventListener('click', function () {
    if (S.auto) { S.auto = false; syncButtons(); return; }
    if (S.pos >= S.script.length) reset();
    playAll();
  });
  btnStep.addEventListener('click', function () { playOne(); syncButtons(); });
  btnReset.addEventListener('click', reset);
  [chA, chB, chC].forEach(function (c) { c.addEventListener('change', onChaos); });
  MM.onLang(reset);

  buildLedger();
  reset();

  var ORD = { mode: 'smart', running: false };
  var ordStage = $('#ordStage'), ordGo = $('#ordGo');
  var ordSeg = MM.$$('#ordSeg button');

  function ordBuild() {
    ordStage.replaceChildren();
    var source = el('div', { class: 'ord-row' }, [1, 2, 3, 4, 5].map(function (v) { return el('span', { class: 'vchip', text: 'v' + v }); }));
    ORD.arrive = el('div', { class: 'ord-row' });
    ORD.ver = el('b', { text: 'v0' });
    ORD.model = el('div', { class: 'ord-model' }, ORD.ver, el('span', { text: T({ en: 'read model, current version', tr: 'okuma modeli, güncel sürüm' }) }));
    ORD.verdict = el('div', { class: 'ord-verdict' });
    ordStage.appendChild(el('div', null, el('div', { class: 'ord-label', text: T({ en: 'The source emits', tr: 'Kaynak şunu yayar' }) }), source));
    ordStage.appendChild(el('div', null, el('div', { class: 'ord-label', text: T({ en: 'They arrive in this order', tr: 'Şu sırayla gelirler' }) }), ORD.arrive));
    ordStage.appendChild(ORD.model);
    ordStage.appendChild(ORD.verdict);
  }

  function shuffle() {
    var a, tries = 0;
    do {
      a = [1, 2, 3, 4, 5].sort(function () { return Math.random() - 0.5; });
      var max = 0, stale = 0;
      a.forEach(function (v) { if (v < max) stale += 1; max = Math.max(max, v); });
      tries += 1;
      if (a[4] !== 5 && stale >= 1) return a;
    } while (tries < 200);
    return [3, 1, 5, 2, 4];
  }

  async function ordRun() {
    if (ORD.running) return;
    ORD.running = true;
    ordGo.disabled = true;
    ordBuild();
    var order = shuffle(), cur = 0;
    for (var i = 0; i < order.length; i++) {
      await MM.sleep(700);
      var v = order[i], chip = el('span', { class: 'vchip', text: 'v' + v });
      if (ORD.mode === 'smart') {
        if (v > cur) { cur = v; chip.classList.add('applied'); chip.appendChild(el('small', { text: T({ en: 'applied', tr: 'uygulandı' }) })); }
        else { chip.classList.add('stale'); chip.appendChild(el('small', { text: T({ en: 'stale, dropped', tr: 'eski, atıldı' }) })); }
      } else {
        var older = v < cur;
        cur = v;
        chip.classList.add(older ? 'stale' : 'applied');
        chip.appendChild(el('small', { text: older ? T({ en: 'overwrote a newer one', tr: 'yenisinin üstüne yazdı' }) : T({ en: 'applied', tr: 'uygulandı' }) }));
      }
      ORD.arrive.appendChild(chip);
      ORD.ver.textContent = 'v' + cur;
    }
    await MM.sleep(300);
    var ok = cur === 5;
    ORD.verdict.className = 'ord-verdict ' + (ok ? 'ok' : 'bad');
    ORD.verdict.textContent = ok
      ? '✓ ' + T({ en: 'Ended on v5. Stale events were dropped.', tr: 'v5 ile bitti. Eski olaylar atıldı.' })
      : '✕ ' + T({ en: 'Ended on v' + cur + '. The read model is stale although v5 was delivered.', tr: 'v' + cur + ' ile bitti. v5 teslim edildiği halde okuma modeli eski kaldı.' });
    ORD.running = false;
    ordGo.disabled = false;
  }

  ordSeg.forEach(function (b) {
    b.addEventListener('click', function () {
      if (ORD.running) return;
      ORD.mode = b.getAttribute('data-mode');
      ordSeg.forEach(function (x) { x.setAttribute('aria-checked', String(x === b)); });
    });
  });
  ordGo.addEventListener('click', ordRun);
  MM.onLang(function () { if (!ORD.running) ordBuild(); });
  ordBuild();
})();

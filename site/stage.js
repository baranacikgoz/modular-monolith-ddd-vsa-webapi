(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, C = MM.colors;

  var ORDER = ['IAM', 'Products', 'Inventory', 'Notifications'];
  var SUB = {
    IAM: { en: 'Keycloak broker', tr: 'Keycloak aracısı' },
    Products: { en: 'stores · products', tr: 'mağaza · ürün' },
    Inventory: { en: 'stock · reservations', tr: 'stok · rezervasyon' },
    Notifications: { en: 'SMS · email · push', tr: 'SMS · e-posta · push' }
  };
  var TOPOS = {
    mono: [['IAM', 'Products', 'Inventory', 'Notifications']],
    duo: [['IAM', 'Notifications'], ['Products', 'Inventory']],
    quad: [['IAM'], ['Products'], ['Inventory'], ['Notifications']]
  };
  var CYCLE = ['mono', 'duo', 'quad'];
  var MSGS = [
    { from: 'Products', to: 'Inventory', name: 'ProductCreatedIntegrationEvent', kind: 'event' },
    { from: 'Products', to: 'Inventory', name: 'GetStockLevel', kind: 'req' },
    { from: 'Inventory', to: 'Products', name: 'GetProduct', kind: 'req' },
    { from: 'IAM', to: 'Notifications', name: 'SendPhoneOtp', kind: 'req' },
    { from: 'IAM', to: 'Notifications', name: 'VerifyPhoneOtp', kind: 'req' },
    { from: 'IAM', to: 'Notifications', name: 'SendSecurityAlert', kind: 'req' },
    { from: 'Notifications', to: 'IAM', name: 'GetActiveSessionIds', kind: 'req' },
    { from: 'Products', to: 'IAM', name: 'GetSeedUserIds', kind: 'req' }
  ];

  var stage = $('#stage'), procsEl = $('#procs'), cv = $('#flow'), ctx = cv.getContext('2d');
  var busLabel = $('#busLabel'), ticker = $('#ticker');
  var cells = {}, procIndexOf = {}, parts = [];
  var topo = 'mono', geo = null, W = 0, H = 0, dpr = 1;
  var visible = true, busy = false, nextSpawn = 0, last = 0, total = 0, crossed = 0, wasVertical = null;

  ORDER.forEach(function (m) {
    var c = el('button', { class: 'cell', type: 'button', 'data-mod': m, style: '--c:' + C[m] },
      el('span', { class: 'cell-name', text: m }),
      el('span', { class: 'cell-sub' }));
    c.addEventListener('click', function () { fire(m); });
    cells[m] = c;
  });

  function isVertical() { return window.innerWidth <= 760; }

  function labelCells() {
    ORDER.forEach(function (m) {
      $('.cell-sub', cells[m]).textContent = MM.t(SUB[m]);
      cells[m].setAttribute('aria-label', m + ': ' + MM.t({ en: 'send a message', tr: 'mesaj gönder' }));
    });
  }

  function chip(name) {
    return el('span', { class: 'chip', style: '--c:' + C[name] },
      el('span', { class: 'chip-n', text: name }),
      el('span', { class: 'chip-t', text: MM.t({ en: 'core', tr: 'çekirdek' }) }));
  }

  function buildProc(i, mods, key, animate) {
    var lines = key === 'mono' ? [['0', '*']] : mods.map(function (m, k) { return [String(k), m]; });
    var env = el('p', { class: 'proc-env' },
      el('span', { text: 'ModulesOptions__EnabledModules' }),
      lines.map(function (l) { return el('span', null, '__' + l[0] + '=', el('b', { text: l[1] })); }));
    var cols = isVertical() ? Math.min(mods.length, 2) : mods.length;
    var body = el('div', { class: 'proc-body', style: '--cols:' + cols });
    mods.forEach(function (m) { body.appendChild(cells[m]); procIndexOf[m] = i; });
    return el('div', { class: 'proc' + (animate ? '' : ' static'), style: animate ? 'animation-delay:' + (i * 90) + 'ms' : null },
      el('div', { class: 'proc-head' },
        el('span', { class: 'proc-name', text: MM.t({ en: 'process ', tr: 'süreç ' }) + (i + 1) }),
        el('span', { text: MM.t({ en: 'same image', tr: 'aynı imaj' }) })),
      env,
      body,
      el('div', { class: 'proc-foot' }, chip('Outbox'), chip('BackgroundJobs')));
  }

  function measureCells() {
    var out = {};
    ORDER.forEach(function (m) { var r = cells[m].getBoundingClientRect(); out[m] = { left: r.left, top: r.top }; });
    return out;
  }

  function flip(before) {
    ORDER.forEach(function (m) {
      var r = cells[m].getBoundingClientRect();
      var dx = before[m].left - r.left, dy = before[m].top - r.top;
      if ((dx || dy) && cells[m].animate) {
        cells[m].animate([{ transform: 'translate(' + dx + 'px,' + dy + 'px)' }, { transform: 'none' }],
          { duration: 800, easing: 'cubic-bezier(.2,.8,.2,1)' });
      }
    });
  }

  function updateSeg() {
    MM.$$('#topoSeg button').forEach(function (b) {
      b.setAttribute('aria-checked', String(b.getAttribute('data-topo') === topo));
    });
  }

  function render(key, animate) {
    var before = animate ? measureCells() : null;
    topo = key;
    procIndexOf = {};
    procsEl.replaceChildren();
    TOPOS[key].forEach(function (mods, i) { procsEl.appendChild(buildProc(i, mods, key, animate)); });
    labelCells();
    updateSeg();
    wasVertical = isVertical();
    if (before) {
      flip(before);
      busy = true;
      parts.length = 0;
      total = 0;
      crossed = 0;
      updateCounters();
      ticker.replaceChildren();
      setTimeout(function () { busy = false; measureGeo(); }, MM.reduced ? 80 : 860);
    } else {
      measureGeo();
    }
  }

  function sizeCanvas() {
    var r = stage.getBoundingClientRect();
    W = r.width;
    H = r.height;
    dpr = Math.min(window.devicePixelRatio || 1, 2);
    cv.width = Math.max(1, Math.round(W * dpr));
    cv.height = Math.max(1, Math.round(H * dpr));
    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
  }

  function measureGeo() {
    var sr = stage.getBoundingClientRect();
    var vertical = isVertical();
    var cellR = {};
    ORDER.forEach(function (m) {
      var r = cells[m].getBoundingClientRect();
      cellR[m] = { x: r.left - sr.left, y: r.top - sr.top, w: r.width, h: r.height };
    });
    var pr = procsEl.getBoundingClientRect();
    geo = {
      vertical: vertical,
      cellR: cellR,
      busY: pr.bottom - sr.top + 46,
      busX: 24,
      top: pr.top - sr.top,
      bottom: pr.bottom - sr.top
    };
    busLabel.classList.toggle('v', vertical);
    if (vertical) {
      busLabel.style.left = (geo.busX - 14) + 'px';
      busLabel.style.top = (geo.top + 8) + 'px';
    } else {
      busLabel.style.left = '14px';
      busLabel.style.top = (geo.busY - 14) + 'px';
    }
  }

  function densify(pts, r) {
    var out = [], step = 2, cur = pts[0];
    for (var i = 1; i < pts.length; i++) {
      var p = pts[i], nxt = pts[i + 1];
      var dx = p[0] - cur[0], dy = p[1] - cur[1], len = Math.hypot(dx, dy) || 1;
      var ux = dx / len, uy = dy / len;
      var rr = nxt ? Math.min(r, len / 2, Math.hypot(nxt[0] - p[0], nxt[1] - p[1]) / 2) : 0;
      var ex = p[0] - ux * rr, ey = p[1] - uy * rr;
      var L = Math.hypot(ex - cur[0], ey - cur[1]);
      var n = Math.max(1, Math.round(L / step));
      for (var k = 0; k < n; k++) out.push([cur[0] + (ex - cur[0]) * k / n, cur[1] + (ey - cur[1]) * k / n]);
      if (nxt) {
        var nx = nxt[0] - p[0], ny = nxt[1] - p[1], nl = Math.hypot(nx, ny) || 1;
        var sx = p[0] + nx / nl * rr, sy = p[1] + ny / nl * rr;
        for (var j = 0; j <= 10; j++) {
          var t = j / 10, a = 1 - t;
          out.push([a * a * ex + 2 * a * t * p[0] + t * t * sx, a * a * ey + 2 * a * t * p[1] + t * t * sy]);
        }
        cur = [sx, sy];
      } else {
        out.push([p[0], p[1]]);
      }
    }
    return out;
  }

  function pathFor(a, b) {
    var A = geo.cellR[a], B = geo.cellR[b], pts;
    if (!geo.vertical) {
      var ax = A.x + A.w / 2, bx = B.x + B.w / 2;
      pts = [[ax, A.y + A.h], [ax, geo.busY], [bx, geo.busY], [bx, B.y + B.h]];
    } else {
      var ay = A.y + A.h / 2, by = B.y + B.h / 2;
      pts = [[A.x, ay], [geo.busX, ay], [geo.busX, by], [B.x, by]];
    }
    return densify(pts, 14);
  }

  function hit(m) {
    var c = cells[m];
    c.classList.remove('hit');
    void c.offsetWidth;
    c.classList.add('hit');
  }

  function updateCounters() {
    $('#cntAll').textContent = String(total);
    $('#cntCross').textContent = String(crossed);
  }

  function logTicker(msg, cross) {
    var li = el('li', { class: 'tk' },
      el('span', { class: 'tk-k ' + msg.kind, text: msg.kind === 'event' ? 'EVENT' : 'REQUEST' }),
      el('span', { class: 'tk-name', text: msg.from + ' → ' + msg.to + '  ' + msg.name }),
      el('span', { class: 'tk-x' + (cross ? ' cross' : ''), text: cross
        ? MM.t({ en: 'crosses a process boundary', tr: 'süreç sınırını geçer' })
        : MM.t({ en: 'same process', tr: 'aynı süreç' }) }));
    ticker.insertBefore(li, ticker.firstChild);
    while (ticker.children.length > 5) ticker.removeChild(ticker.lastChild);
  }

  function respond(msg) {
    parts.push({
      pts: pathFor(msg.to, msg.from), s: 0, speed: 135, color: C[msg.to], kind: 'res', label: '',
      onArrive: function () { hit(msg.from); }
    });
  }

  function spawn(msg) {
    if (busy) return;
    measureGeo();
    var cross = procIndexOf[msg.from] !== procIndexOf[msg.to];
    parts.push({
      pts: pathFor(msg.from, msg.to), s: 0, speed: MM.rand(120, 150), color: C[msg.from], kind: msg.kind, label: msg.name,
      onArrive: function () { hit(msg.to); if (msg.kind === 'req') respond(msg); }
    });
    total += 1;
    if (cross) crossed += 1;
    updateCounters();
    logTicker(msg, cross);
  }

  function fire(m) {
    var pool = MSGS.filter(function (x) { return x.from === m; });
    spawn(pool[Math.floor(Math.random() * pool.length)]);
  }

  function rgba(hex, a) {
    var n = parseInt(hex.slice(1), 16);
    return 'rgba(' + (n >> 16) + ',' + ((n >> 8) & 255) + ',' + (n & 255) + ',' + a.toFixed(3) + ')';
  }

  function drawBus(now) {
    ctx.save();
    ctx.lineCap = 'round';
    ctx.lineWidth = 1.5;
    ctx.strokeStyle = 'rgba(255,106,31,.2)';
    ORDER.forEach(function (m) {
      var r = geo.cellR[m];
      ctx.beginPath();
      if (geo.vertical) { ctx.moveTo(r.x, r.y + r.h / 2); ctx.lineTo(geo.busX, r.y + r.h / 2); }
      else { ctx.moveTo(r.x + r.w / 2, r.y + r.h); ctx.lineTo(r.x + r.w / 2, geo.busY); }
      ctx.stroke();
    });
    ctx.lineWidth = 2;
    ctx.strokeStyle = 'rgba(255,106,31,.5)';
    ctx.shadowColor = '#ff6a1f';
    ctx.shadowBlur = 12;
    ctx.beginPath();
    if (geo.vertical) { ctx.moveTo(geo.busX, geo.top); ctx.lineTo(geo.busX, geo.bottom); }
    else { ctx.moveTo(6, geo.busY); ctx.lineTo(W - 6, geo.busY); }
    ctx.stroke();
    ctx.shadowBlur = 0;
    ctx.strokeStyle = 'rgba(255,190,140,.85)';
    ctx.setLineDash([2, 14]);
    ctx.lineDashOffset = -now * 0.025;
    ctx.stroke();
    ctx.restore();
  }

  function drawPart(p) {
    var i0 = Math.floor(p.s), L = 36;
    var a0 = p.pts[Math.min(i0, p.pts.length - 1)];
    ctx.save();
    ctx.lineCap = 'round';
    for (var k = Math.max(1, i0 - L); k <= i0; k++) {
      var f = (k - (i0 - L)) / L;
      if (p.kind === 'event' && ((k >> 2) & 1)) continue;
      ctx.strokeStyle = rgba(p.color, f * 0.9);
      ctx.lineWidth = (p.kind === 'res' ? 1.6 : 2.4) * (0.35 + 0.65 * f);
      ctx.beginPath();
      ctx.moveTo(p.pts[k - 1][0], p.pts[k - 1][1]);
      ctx.lineTo(p.pts[k][0], p.pts[k][1]);
      ctx.stroke();
    }
    ctx.shadowColor = p.color;
    ctx.shadowBlur = 16;
    ctx.fillStyle = p.color;
    ctx.strokeStyle = p.color;
    ctx.beginPath();
    if (p.kind === 'event') {
      ctx.moveTo(a0[0], a0[1] - 5.5); ctx.lineTo(a0[0] + 5.5, a0[1]); ctx.lineTo(a0[0], a0[1] + 5.5); ctx.lineTo(a0[0] - 5.5, a0[1]); ctx.closePath();
      ctx.fill();
    } else if (p.kind === 'res') {
      ctx.lineWidth = 1.8;
      ctx.arc(a0[0], a0[1], 4, 0, 6.283);
      ctx.stroke();
    } else {
      ctx.arc(a0[0], a0[1], 4.2, 0, 6.283);
      ctx.fill();
    }
    ctx.restore();
    if (p.label) {
      ctx.save();
      ctx.font = '500 11px "Geist Mono", ui-monospace, monospace';
      var w = ctx.measureText(p.label).width + 14;
      var lx = MM.clamp(a0[0] + 12, 4, W - w - 4), ly = MM.clamp(a0[1] - 28, 4, H - 24);
      ctx.fillStyle = 'rgba(9,9,10,.88)';
      ctx.strokeStyle = rgba(p.color, 0.5);
      ctx.lineWidth = 1;
      ctx.beginPath();
      if (ctx.roundRect) ctx.roundRect(lx, ly, w, 20, 10); else ctx.rect(lx, ly, w, 20);
      ctx.fill();
      ctx.stroke();
      ctx.fillStyle = '#ece7db';
      ctx.fillText(p.label, lx + 7, ly + 14);
      ctx.restore();
    }
  }

  function frame(now) {
    requestAnimationFrame(frame);
    if (!geo || !visible || document.hidden) { last = now; return; }
    var dt = Math.min(0.05, (now - last) / 1000);
    last = now;
    ctx.clearRect(0, 0, W, H);
    drawBus(now);
    if (!MM.reduced && !busy && now > nextSpawn && parts.length < 6) {
      spawn(MSGS[Math.floor(Math.random() * MSGS.length)]);
      nextSpawn = now + MM.rand(900, 1600);
    }
    for (var i = parts.length - 1; i >= 0; i--) {
      var p = parts[i];
      p.s += p.speed * dt;
      if (p.s >= p.pts.length - 1) {
        parts.splice(i, 1);
        if (p.onArrive) p.onArrive();
        continue;
      }
      drawPart(p);
    }
  }

  function setTopo(key) {
    if (key === topo || busy) return;
    render(key, true);
  }

  MM.$$('#topoSeg button').forEach(function (b) {
    b.addEventListener('click', function () { setTopo(b.getAttribute('data-topo')); });
    b.addEventListener('keydown', function (e) {
      var d = e.key === 'ArrowRight' ? 1 : e.key === 'ArrowLeft' ? -1 : 0;
      if (!d) return;
      e.preventDefault();
      var next = CYCLE[(CYCLE.indexOf(topo) + d + CYCLE.length) % CYCLE.length];
      setTopo(next);
      $('#topoSeg [data-topo="' + next + '"]').focus();
    });
  });

  document.addEventListener('keydown', function (e) {
    if (e.key.toLowerCase() !== 's' || e.metaKey || e.ctrlKey || e.altKey) return;
    var t = e.target && e.target.tagName;
    if (t === 'INPUT' || t === 'TEXTAREA') return;
    setTopo(CYCLE[(CYCLE.indexOf(topo) + 1) % CYCLE.length]);
  });

  MM.onLang(function () { render(topo, false); ticker.replaceChildren(); });

  if ('ResizeObserver' in window) {
    new ResizeObserver(function () {
      sizeCanvas();
      if (wasVertical !== isVertical()) render(topo, false);
      else if (!busy) measureGeo();
    }).observe(stage);
  } else {
    window.addEventListener('resize', function () { sizeCanvas(); measureGeo(); });
  }
  if (document.fonts && document.fonts.ready) document.fonts.ready.then(function () { sizeCanvas(); measureGeo(); });

  MM.onVisible(stage, function (v) { visible = v; }, '120px');

  render('mono', false);
  sizeCanvas();
  requestAnimationFrame(frame);
})();

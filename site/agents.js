(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, C = MM.colors, T = MM.t;

  var GROUPS = {
    plan: { c: '#b99cff', n: { en: 'plan', tr: 'planla' } },
    build: { c: '#6fe3a5', n: { en: 'build', tr: 'üret' } },
    contract: { c: '#ff6a1f', n: { en: 'cross-module contract', tr: 'modüller arası sözleşme' } },
    platform: { c: '#ffb347', n: { en: 'platform', tr: 'platform' } },
    fix: { c: '#ff5252', n: { en: 'fix', tr: 'düzelt' } },
    guard: { c: '#5cc8ff', n: { en: 'guard', tr: 'koru' } }
  };

  var CMDS = [
    { n: 'plan-feature', g: 'plan', d: { en: 'Plan a feature: owning module, cross-module contracts, file inventory, data model, tests, telemetry. No code until approved.', tr: 'Bir özelliği planla: sahip modül, modüller arası sözleşmeler, dosya envanteri, veri modeli, testler, telemetri. Onaylanana kadar kod yok.' } },
    { n: 'plan-refactor', g: 'plan', d: { en: 'Plan a refactor: current violations, exact file changes, risks. No code until approved.', tr: 'Bir refactor planla: mevcut ihlaller, kesin dosya değişiklikleri, riskler. Onaylanana kadar kod yok.' } },
    { n: 'scaffold-feature', g: 'build', d: { en: 'Scaffold a vertical slice (Endpoint, Request with inline validator, Response, domain method) and register it in Setup.cs.', tr: 'Dikey bir dilim üret (Endpoint, satır içi doğrulayıcılı Request, Response, domain metodu) ve Setup.cs içine kaydet.' } },
    { n: 'scaffold-module', g: 'build', d: { en: 'Scaffold a new top-level module: four projects, DbContext, persistence setup, IModule, telemetry, tests, Makefile targets.', tr: 'Yeni bir üst düzey modül üret: dört proje, DbContext, kalıcılık kurulumu, IModule, telemetri, testler, Makefile hedefleri.' } },
    { n: 'scaffold-test', g: 'build', d: { en: 'Scaffold integration and unit tests for a feature. Use before implementation for a Red baseline, or after for coverage.', tr: 'Bir özellik için entegrasyon ve birim testleri üret. Kırmızı başlangıç için uygulamadan önce, kapsama için sonra kullan.' } },
    { n: 'execute-feature', g: 'build', d: { en: 'Implement an approved feature plan end-to-end.', tr: 'Onaylanmış bir özellik planını uçtan uca uygula.' } },
    { n: 'execute-refactor', g: 'build', d: { en: 'Execute an approved refactor plan incrementally with zero regressions.', tr: 'Onaylanmış bir refactor planını sıfır gerilemeyle adım adım uygula.' } },
    { n: 'add-integration-event', g: 'contract', d: { en: 'Declare an IntegrationEvent, publish it from a DomainEventHandler in the source module, and scaffold the consumer in the target module.', tr: 'Bir IntegrationEvent bildir, kaynak modüldeki DomainEventHandler içinden yayınla ve hedef modülde tüketiciyi üret.' } },
    { n: 'add-inter-module-request', g: 'contract', d: { en: 'Declare an InterModuleRequest contract, implement the handler in the source module, and wire the caller.', tr: 'Bir InterModuleRequest sözleşmesi bildir, handler\'ı kaynak modülde uygula ve çağıranı bağla.' } },
    { n: 'manage-feature-flag', g: 'platform', d: { en: 'Create, toggle, add a variant for, or apply a feature flag to endpoints.', tr: 'Bir özellik bayrağı oluştur, aç/kapat, varyant ekle ya da uç noktalara uygula.' } },
    { n: 'manage-migration', g: 'platform', d: { en: 'Add an EF Core migration, generate the idempotent SQL script, and verify with check-migration-drift.', tr: 'Bir EF Core migration\'ı ekle, idempotent SQL betiğini üret ve check-migration-drift ile doğrula.' } },
    { n: 'update-dependencies', g: 'platform', d: { en: 'Update NuGet packages via Central Package Management, then build and run the full suite.', tr: 'NuGet paketlerini Central Package Management ile güncelle, sonra derle ve tüm paketi çalıştır.' } },
    { n: 'fix-bug', g: 'fix', d: { en: 'Reproduce, diagnose, and fix a bug with a Red/Green test. No guesswork.', tr: 'Bir hatayı Kırmızı/Yeşil testle yeniden üret, teşhis et ve düzelt. Tahmin yok.' } },
    { n: 'audit-architecture', g: 'guard', d: { en: 'Audit the codebase against CLAUDE.md rules. Reports PASS, FAIL, or WARNING per check with file:line.', tr: 'Kod tabanını CLAUDE.md kurallarına göre denetle. Her kontrol için dosya:satır ile PASS, FAIL ya da WARNING raporlar.' } },
    { n: 'verify-feature', g: 'guard', d: { en: 'Quality gate: architecture audit, architecture tests, module or full test suite. Fix failures before reporting done.', tr: 'Kalite kapısı: mimari denetim, mimari testler, modül ya da tam test paketi. Bitti demeden önce hataları düzelt.' } }
  ];

  var RULES = [
    { n: 0, t: { en: 'Discovery: graph first', tr: 'Keşif: önce graf' } },
    { n: 1, t: { en: 'Architecture', tr: 'Mimari' } },
    { n: 2, t: { en: 'Exemplars to copy', tr: 'Kopyalanacak örnekler' } },
    { n: 3, t: { en: 'Functional pipeline', tr: 'Fonksiyonel boru hattı' } },
    { n: 4, t: { en: 'Persistence', tr: 'Kalıcılık' } },
    { n: 5, t: { en: 'Endpoints and C#', tr: 'Uç noktalar ve C#' } },
    { n: 6, t: { en: 'Cross-module and events', tr: 'Modüller arası ve olaylar' } },
    { n: 7, t: { en: 'Observability', tr: 'Gözlemlenebilirlik' } },
    { n: 8, t: { en: 'Testing', tr: 'Test' } },
    { n: 9, t: { en: 'Options pattern', tr: 'Options deseni' } },
    { n: 10, t: { en: 'Report every issue you see', tr: 'Gördüğün her sorunu bildir' } },
    { n: 11, t: { en: 'Make targets', tr: 'Make hedefleri' } }
  ];

  var REF = '<ProjectReference Include="..\\..\\Inventory\\Inventory.Domain\\Inventory.Domain.csproj"/>';
  var CSPROJ = 'src/Modules/Products/Products.Endpoints/Products.Endpoints.csproj';
  var HOOKS = {
    edit: {
      note: { en: 'A PostToolUse hook fires after every Edit or Write of a module project file and checks its ProjectReference lines.', tr: 'Bir PostToolUse hook\'u, bir modül proje dosyasının her Edit ya da Write işleminden sonra çalışır ve ProjectReference satırlarını denetler.' },
      lines: [
        { t: '$ edit ' + CSPROJ, c: 't-p', typed: true },
        { t: '  + ' + REF, c: 't-g' },
        { t: '' },
        { t: '⚠️  CROSS-MODULE REFERENCE in Products.Endpoints.csproj: fix before committing:', c: 't-r' },
        { t: '    ' + REF, c: 't-r' },
        { t: '' },
        { t: 'exit 2', c: 't-c' }
      ]
    },
    commit: {
      note: { en: 'A PreToolUse hook intercepts every git commit an agent runs and refuses it while any module project references another module.', tr: 'Bir PreToolUse hook\'u, bir ajanın çalıştırdığı her git commit komutunu yakalar ve herhangi bir modül projesi başka bir modüle referans veriyorsa reddeder.' },
      lines: [
        { t: '$ git commit -m "just this once"', c: 't-p', typed: true },
        { t: '⛔ COMMIT BLOCKED: cross-module references detected:', c: 't-r' },
        { t: '' },
        { t: CSPROJ + ':', c: 't-h' },
        { t: '    ' + REF, c: 't-r' },
        { t: '' },
        { t: 'Remove illegal ProjectReferences before committing.', c: 't-h' },
        { t: '' },
        { t: 'exit 2', c: 't-c' }
      ]
    }
  };

  var palQ = $('#palQ'), palList = $('#palList'), palDetail = $('#palDetail');
  var curCmd = CMDS[0].n;

  function renderPalette() {
    var q = palQ.value.trim().toLowerCase();
    palList.replaceChildren();
    var shown = CMDS.filter(function (c) { return !q || c.n.indexOf(q) >= 0 || T(c.d).toLowerCase().indexOf(q) >= 0; });
    shown.forEach(function (c) {
      var b = el('button', { class: 'pal-item', type: 'button', 'aria-pressed': String(c.n === curCmd), style: '--g:' + GROUPS[c.g].c }, el('i', { text: '●' }), c.n);
      b.addEventListener('click', function () { curCmd = c.n; renderPalette(); });
      palList.appendChild(el('li', null, b));
    });
    var cmd = CMDS.filter(function (c) { return c.n === curCmd; })[0];
    if (!shown.length) {
      palDetail.replaceChildren(el('b', { text: '/' + q }), T({ en: 'No command matches. The repository ships fifteen.', tr: 'Eşleşen komut yok. Depo on beş komut sunar.' }));
      return;
    }
    palDetail.style.setProperty('--g', GROUPS[cmd.g].c);
    palDetail.replaceChildren(el('b', { text: '/' + cmd.n }), T(cmd.d), el('small', { text: T(GROUPS[cmd.g].n) }));
  }

  palQ.addEventListener('input', renderPalette);

  var hookTerm = $('#hookTerm'), hookNote = $('#hookNote'), hookKey = 'edit', hookRun = 0;

  async function playHook() {
    var id = ++hookRun, h = HOOKS[hookKey];
    hookNote.textContent = T(h.note);
    hookTerm.replaceChildren();
    for (var i = 0; i < h.lines.length; i++) {
      if (id !== hookRun) return;
      var ln = h.lines[i], span = el('span', { class: ln.c || '' });
      hookTerm.appendChild(span);
      if (ln.typed && !MM.reduced) {
        var caret = el('span', { class: 'caret' });
        hookTerm.appendChild(caret);
        for (var k = 1; k <= ln.t.length; k++) {
          if (id !== hookRun) return;
          span.textContent = ln.t.slice(0, k);
          await new Promise(function (r) { setTimeout(r, 14); });
        }
        caret.remove();
        await MM.sleep(260);
      } else {
        span.textContent = ln.t;
        await MM.sleep(ln.t ? 170 : 40);
      }
      hookTerm.appendChild(document.createTextNode('\n'));
    }
  }

  MM.$$('#hookSeg button').forEach(function (b) {
    b.addEventListener('click', function () {
      hookKey = b.getAttribute('data-hook');
      MM.$$('#hookSeg button').forEach(function (x) { x.setAttribute('aria-checked', String(x === b)); });
      playHook();
    });
  });

  var started = false;
  MM.onVisible($('#hookTerm'), function (v) { if (v && !started) { started = true; playHook(); } }, '-10% 0px');

  function renderRules() {
    var ol = $('#ruleChips');
    ol.replaceChildren();
    RULES.forEach(function (r) {
      ol.appendChild(el('li', { class: 'rchip' }, el('b', { text: '§' + r.n }), T(r.t)));
    });
  }

  MM.onLang(function () { renderPalette(); renderRules(); hookNote.textContent = T(HOOKS[hookKey].note); });
  renderPalette();
  renderRules();
  hookNote.textContent = T(HOOKS[hookKey].note);

  var cv = $('#graphCv'), ctx = cv.getContext('2d');
  var GW = 0, GH = 0, nodes = [], edges = [], gvis = false, gmx = 0, gmy = 0, gtx = 0, gty = 0;
  var PAL = [C.IAM, C.Products, C.Inventory, C.Notifications, C.Outbox, C.BackgroundJobs, '#ece7db'];

  function rgba(hex, a) {
    var n = parseInt(hex.slice(1), 16);
    return 'rgba(' + (n >> 16) + ',' + ((n >> 8) & 255) + ',' + (n & 255) + ',' + a + ')';
  }

  function seedGraph() {
    nodes = [];
    edges = [];
    var centers = PAL.map(function (c, i) {
      return { x: GW * MM.rand(0.38, 0.94), y: GH * MM.rand(0.12, 0.88) };
    });
    var per = Math.round(MM.clamp(GW / 18, 28, 70) / PAL.length);
    PAL.forEach(function (col, ci) {
      for (var i = 0; i < per + 3; i++) {
        var a = Math.random() * 6.283, r = Math.pow(Math.random(), 0.7) * MM.rand(30, 110);
        nodes.push({ bx: centers[ci].x + Math.cos(a) * r, by: centers[ci].y + Math.sin(a) * r * 0.8, cl: ci, ph: Math.random() * 6.283, r: i === 0 ? 5 : MM.rand(1.4, 2.8), z: MM.rand(0.4, 1) });
      }
    });
    nodes.forEach(function (n, i) {
      var near = nodes.map(function (m, j) { return { j: j, d: Math.hypot(m.bx - n.bx, m.by - n.by) }; })
        .filter(function (o) { return o.j !== i; })
        .sort(function (a, b) { return a.d - b.d; })
        .slice(0, 2);
      near.forEach(function (o) { if (o.j > i) edges.push([i, o.j]); });
    });
  }

  function sizeGraph() {
    var r = cv.getBoundingClientRect(), dpr = Math.min(window.devicePixelRatio || 1, 2);
    GW = r.width;
    GH = r.height;
    cv.width = Math.round(GW * dpr);
    cv.height = Math.round(GH * dpr);
    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
    seedGraph();
  }

  function pos(n, t) {
    return {
      x: n.bx + Math.sin(t * 0.0004 + n.ph) * 7 + gmx * 14 * n.z,
      y: n.by + Math.cos(t * 0.0005 + n.ph) * 6 + gmy * 14 * n.z
    };
  }

  function drawGraph(t) {
    ctx.clearRect(0, 0, GW, GH);
    var P = nodes.map(function (n) { return pos(n, t); });
    ctx.lineWidth = 1;
    edges.forEach(function (e) {
      ctx.strokeStyle = rgba(PAL[nodes[e[0]].cl], 0.2);
      ctx.beginPath();
      ctx.moveTo(P[e[0]].x, P[e[0]].y);
      ctx.lineTo(P[e[1]].x, P[e[1]].y);
      ctx.stroke();
    });
    nodes.forEach(function (n, i) {
      ctx.fillStyle = rgba(PAL[n.cl], 0.9);
      ctx.shadowColor = PAL[n.cl];
      ctx.shadowBlur = n.r > 4 ? 14 : 5;
      ctx.beginPath();
      ctx.arc(P[i].x, P[i].y, n.r, 0, 6.283);
      ctx.fill();
    });
    ctx.shadowBlur = 0;
  }

  function gloop(t) {
    requestAnimationFrame(gloop);
    if (!gvis || document.hidden) return;
    gmx += (gtx - gmx) * 0.05;
    gmy += (gty - gmy) * 0.05;
    drawGraph(t);
  }

  var gbox = $('.graph');
  gbox.addEventListener('pointermove', function (e) {
    var r = gbox.getBoundingClientRect();
    gtx = (e.clientX - r.left) / r.width - 0.5;
    gty = (e.clientY - r.top) / r.height - 0.5;
  });
  MM.onVisible(gbox, function (v) { gvis = v; });
  if ('ResizeObserver' in window) new ResizeObserver(function () { sizeGraph(); if (MM.reduced) drawGraph(0); }).observe(gbox);
  else { sizeGraph(); }
  if (!MM.reduced) requestAnimationFrame(gloop);
})();

(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, svg = MM.svg, T = MM.t;

  var MAINX = 62, ERRX = 24;

  var LINES = [
    { parts: [['[AsParameters] ', 'p'], ['Request request', ''], [',  // validated first', 'p']],
      fail: { code: '400', key: 'Validation', on: { en: 'RequestValidator rejects the request', tr: 'RequestValidator isteği reddeder' },
        txt: { en: 'The request validator failed before the pipeline started. The error becomes a localized ProblemDetails.',
          tr: 'İstek doğrulayıcısı, boru hattı başlamadan önce başarısız oldu. Hata, yerelleştirilmiş bir ProblemDetails olur.' } } },
    { parts: [['return await ', 'k'], ['dbContext', '']] },
    { parts: [['    .ProductTemplates', '']] },
    { parts: [['    .', ''], ['TagWith', 'f'], ['(nameof(ActivateProductTemplateAsync), request.Id)', '']] },
    { parts: [['    .', ''], ['Where', 'f'], ['(p => p.Id == request.Id)', '']] },
    { parts: [['    .', ''], ['SingleAsResultAsync', 'f'], ['(nameof(ProductTemplate), cancellationToken)', '']],
      fail: { code: '404', key: 'NotFound', on: { en: 'no row with that id', tr: 'bu id\'ye sahip satır yok' },
        txt: { en: 'SingleAsResultAsync found no ProductTemplate with that id. Every later TapAsync is skipped.',
          tr: 'SingleAsResultAsync bu id\'ye sahip bir ProductTemplate bulamadı. Sonraki tüm TapAsync adımları atlanır.' } } },
    { parts: [['    .', ''], ['TapAsync', 'f'], ['(productTemplate => productTemplate.Activate())', '']],
      fail: { code: '400', key: 'SameValue', on: { en: 'the aggregate refuses', tr: 'aggregate reddeder' },
        txt: { en: 'An aggregate method returned a failed Result, for example Error.SameValue when the value to change is the same as before. SaveChangesAsync never runs.',
          tr: 'Bir aggregate metodu başarısız Result döndürdü, örneğin değiştirilecek değer öncekiyle aynıysa Error.SameValue. SaveChangesAsync hiç çalışmaz.' } } },
    { parts: [['    .', ''], ['TapAsync', 'f'], ['(async _ => await dbContext.SaveChangesAsync(cancellationToken));', '']],
      fail: { code: '409', key: 'ViolatesUniqueConstraint', sw: 'Unique', on: { en: 'unique constraint hit', tr: 'benzersizlik kısıtı ihlali' },
        txt: { en: 'SaveChangesAsync hit a unique constraint. The violation is mapped to a Result failure instead of leaking as an exception.',
          tr: 'SaveChangesAsync bir benzersizlik kısıtına çarptı. İhlal, istisna olarak sızmak yerine Result hatasına eşlenir.' } } }
  ];

  var block = $('#codeblock'), resultEl = $('#railResult'), runBtn = $('#railRun'), clearBtn = $('#railClear');
  var lineEls = [], swEls = {}, on = {}, running = false, runId = 0, ys = [], train = null;

  function buildCode() {
    block.replaceChildren();
    lineEls = [];
    swEls = {};
    LINES.forEach(function (ln, i) {
      var code = el('span', null, ln.parts.map(function (p) { return el('span', { class: p[1], text: p[0] }); }));
      var kids = [code];
      if (ln.fail) {
        var sw = el('button', { class: 'sw', type: 'button', 'aria-pressed': String(!!on[i]), 'aria-label': ln.fail.key + ': ' + T(ln.fail.on) },
          el('i'), el('span', { text: ln.fail.sw || ln.fail.key }));
        sw.addEventListener('click', function () {
          on[i] = !on[i];
          sw.setAttribute('aria-pressed', String(!!on[i]));
          if (on[i]) setTimeout(run, 120);
        });
        swEls[i] = sw;
        kids.push(sw);
      }
      var row = el('div', { class: 'cl' }, kids);
      lineEls.push(row);
      block.appendChild(row);
    });
    buildGutter();
  }

  function buildGutter() {
    var old = $('.gut', block);
    if (old) old.remove();
    var br = block.getBoundingClientRect(), H = block.scrollHeight;
    ys = lineEls.map(function (n) { var r = n.getBoundingClientRect(); return r.top - br.top + r.height / 2 + block.scrollTop; });
    var top = ys[0] - 16, bottom = H - 22;
    var g = svg('svg', { class: 'gut', width: 92, height: H, viewBox: '0 0 92 ' + H });
    g.appendChild(svg('path', { class: 'track', d: 'M' + MAINX + ' ' + top + ' V' + bottom }));
    g.appendChild(svg('path', { class: 'track-err', d: 'M' + ERRX + ' ' + (ys[0] + 6) + ' V' + bottom }));
    LINES.forEach(function (ln, i) {
      if (ln.fail) {
        g.appendChild(svg('path', { class: 'track-err', d: 'M' + MAINX + ' ' + ys[i] + ' L' + ERRX + ' ' + (ys[i] + 16) }));
        g.appendChild(svg('circle', { class: 'station', id: 'st' + i, cx: MAINX, cy: ys[i], r: 7 }));
      } else {
        g.appendChild(svg('circle', { class: 'station', id: 'st' + i, cx: MAINX, cy: ys[i], r: 3 }));
      }
    });
    g.appendChild(svg('circle', { class: 'station', cx: MAINX, cy: bottom, r: 9 }));
    g.appendChild(svg('circle', { class: 'station fail', cx: ERRX, cy: bottom, r: 9 }));
    train = svg('circle', { class: 'train', cx: MAINX, cy: top, r: 6 });
    g.appendChild(train);
    block.insertBefore(g, block.firstChild);
    g._bottom = bottom;
    g._top = top;
    block._gut = g;
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

  async function moveTo(x, y, dur) {
    var x0 = +train.getAttribute('cx'), y0 = +train.getAttribute('cy');
    await tween(MM.reduced ? 30 : dur, function (p) {
      train.setAttribute('cx', x0 + (x - x0) * p);
      train.setAttribute('cy', y0 + (y - y0) * p);
    });
  }

  function idle() {
    resultEl.replaceChildren(el('p', { class: 'res-idle', text: T({ en: 'Flip a switch, or just send the train.', tr: 'Bir makası çevir ya da treni gönder.' }) }));
  }

  function showResult(code, key, txt, ok) {
    resultEl.replaceChildren(
      el('div', { class: 'res-code ' + (ok ? 'ok' : 'err'), text: code }),
      el('div', { class: 'res-key', text: key }),
      el('p', { class: 'res-txt', text: T(txt) }));
  }

  function resetVisuals() {
    lineEls.forEach(function (n) { n.classList.remove('act', 'skipped', 'bad'); });
    MM.$$('.station', block).forEach(function (s) { s.classList.remove('pass', 'fail'); });
    train.setAttribute('cx', MAINX);
    train.setAttribute('cy', block._gut._top);
    train.classList.remove('err');
  }

  async function run() {
    if (running) return;
    running = true;
    var id = ++runId;
    runBtn.disabled = true;
    resetVisuals();
    resultEl.replaceChildren(el('p', { class: 'res-idle', text: '…' }));
    var failed = -1;
    for (var i = 0; i < LINES.length; i++) {
      await moveTo(MAINX, ys[i], 230);
      if (id !== runId) return;
      lineEls[i].classList.add('act');
      var st = $('#st' + i, block);
      if (LINES[i].fail && on[i]) {
        failed = i;
        lineEls[i].classList.add('bad');
        st.classList.add('fail');
        break;
      }
      if (st) st.classList.add('pass');
      await MM.sleep(120);
    }
    var bottom = block._gut._bottom;
    if (failed >= 0) {
      for (var k = failed + 1; k < LINES.length; k++) lineEls[k].classList.add('skipped');
      train.classList.add('err');
      await MM.sleep(240);
      await moveTo(ERRX, ys[failed] + 16, 260);
      await moveTo(ERRX, bottom, 520);
      var f = LINES[failed].fail;
      showResult(f.code, f.key, f.txt, false);
    } else {
      await moveTo(MAINX, bottom, 380);
      showResult('204', 'NoContent', {
        en: 'The chain finished. TransformResultToNoContentResponse turns Result.Success into 204.',
        tr: 'Zincir tamamlandı. TransformResultToNoContentResponse, Result.Success değerini 204\'e çevirir.' }, true);
    }
    running = false;
    runBtn.disabled = false;
  }

  runBtn.addEventListener('click', run);
  clearBtn.addEventListener('click', function () {
    on = {};
    runId += 1;
    running = false;
    runBtn.disabled = false;
    Object.keys(swEls).forEach(function (k) { swEls[k].setAttribute('aria-pressed', 'false'); });
    resetVisuals();
    idle();
  });

  buildCode();
  idle();

  if ('ResizeObserver' in window) {
    new ResizeObserver(function () { if (!running) { buildGutter(); } }).observe(block);
  }
  if (document.fonts && document.fonts.ready) document.fonts.ready.then(function () { if (!running) buildGutter(); });
  MM.onLang(function () { if (!running) { buildCode(); idle(); } });
})();

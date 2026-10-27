(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, T = MM.t;

  function restart(node, cls) {
    node.classList.remove(cls);
    void node.offsetWidth;
    node.classList.add(cls);
  }

  /* Lab 1: accept, queue, poll. The shape of a bulk stock adjustment job. */

  var ROWS = 10, BATCH = 2;
  var REJECTS = { 4: 'UnknownProduct', 8: 'InsufficientStock' };
  var API = '/v1/stock-levels/bulk-adjustments';

  var Q = { running: false };
  var qStage = $('#qStage'), qGo = $('#qGo');
  var qToggles = [$('#qDbl'), $('#qBad'), $('#qKill')];

  function qBuild() {
    qStage.replaceChildren();
    Q.jobs = el('b', { text: '0' });
    Q.outbox = el('b', { text: '0' });
    Q.status = el('b', { text: '·' });
    Q.boxes = [
      el('div', { class: 'qbox' }, Q.jobs, el('span', { text: 'StockBulkAdjustJobs' })),
      el('div', { class: 'qbox' }, Q.outbox, el('span', { text: 'OutboxMessages' })),
      el('div', { class: 'qbox' }, Q.status, el('span', { text: T({ en: 'job status', tr: 'iş durumu' }) }))
    ];
    Q.cells = [];
    for (var i = 1; i <= ROWS; i++) Q.cells.push(el('span', { class: 'qrow', text: String(i) }));
    Q.log = el('pre', { class: 'term qlog', 'aria-live': 'polite' });
    Q.verdict = el('div', { class: 'ord-verdict' });
    qStage.appendChild(el('div', { class: 'qgrid' }, Q.boxes));
    qStage.appendChild(el('div', null,
      el('div', { class: 'ord-label', text: T({ en: 'Rows of the file (batches of two here, 200 in the settings)', tr: 'Dosyanın satırları (burada ikişerli, ayarlarda 200)' }) }),
      el('div', { class: 'qrows' }, Q.cells)));
    qStage.appendChild(Q.log);
    qStage.appendChild(Q.verdict);
  }

  function qSetStatus(s) {
    Q.status.textContent = s;
    Q.status.className = 'st-' + s;
    restart(Q.boxes[2], 'pulse');
  }

  function qSetCount(node, box, n) {
    node.textContent = String(n);
    restart(box, 'pulse');
  }

  function qLine(text, cls) {
    Q.log.appendChild(el('span', { class: cls || '', text: text }));
    Q.log.appendChild(document.createTextNode('\n'));
    while (Q.log.childNodes.length > 80) Q.log.removeChild(Q.log.firstChild);
    Q.log.scrollTop = Q.log.scrollHeight;
  }

  async function qRun() {
    if (Q.running) return;
    Q.running = true;
    qGo.disabled = true;
    qToggles.forEach(function (c) { c.disabled = true; });
    qBuild();

    var dbl = qToggles[0].checked, bad = qToggles[1].checked, kill = qToggles[2].checked;
    var didKill = false, resumedAfter = 0;
    var processed = 0, adjusted = 0, rejected = 0, messages = 1, errors = [];

    async function poll(note, cls) {
      qLine('→ GET  …/j-1', 't-p');
      await MM.sleep(350);
      qLine('← 200  Running  ' + processed + ' / ' + ROWS + ' rows' + (note || ''), cls || 't-g');
    }

    qLine('→ POST ' + API, 't-p');
    qLine('  ClientRequestId c-7f3a', 't-c');
    await MM.sleep(600);
    qSetCount(Q.jobs, Q.boxes[0], 1);
    qSetCount(Q.outbox, Q.boxes[1], 1);
    qSetStatus('Queued');
    qLine('← 202 Accepted  j-1  Queued', 't-g');
    qLine(T({ en: '  one transaction: job row + queue message', tr: '  tek transaction: iş satırı + kuyruk mesajı' }), 't-c');

    if (dbl) {
      await MM.sleep(800);
      qLine('→ POST ' + API, 't-p');
      qLine('  ClientRequestId c-7f3a', 't-c');
      await MM.sleep(600);
      qLine('← 202 Accepted  j-1  Queued', 't-g');
      qLine(T({ en: '  same id, same job, nothing new written', tr: '  aynı id, aynı iş, yeni bir şey yazılmadı' }), 't-c');
    }

    await MM.sleep(900);
    qLine(T({ en: 'consumer: claim the job', tr: 'tüketici: işi sahiplen' }), 't-h');
    qLine('  UPDATE … WHERE Status = Queued', 't-c');
    qSetStatus('Running');

    var batches = ROWS / BATCH;
    for (var b = 0; b < batches; b++) {
      if (kill && b === 3) {
        kill = false;
        didKill = true;
        resumedAfter = processed;
        await MM.sleep(500);
        qLine(T({ en: '✕ the consumer process dies', tr: '✕ tüketici süreci ölür' }), 't-r');
        qLine(T({ en: '  the job stays Running, nobody works on it', tr: '  iş Running kalır, onunla kimse ilgilenmez' }), 't-c');
        await MM.sleep(900);
        await poll(T({ en: '  (no progress)', tr: '  (ilerleme yok)' }), 't-r');
        await MM.sleep(900);
        qLine(T({ en: '… 15 minutes pass (StockJobRunningLeaseMinutes)', tr: '… 15 dakika geçer (StockJobRunningLeaseMinutes)' }), 't-c');
        await MM.sleep(800);
        qLine(T({ en: 'sweep: Running → Queued, RequeueCount 1', tr: 'tarama: Running → Queued, RequeueCount 1' }), 't-h');
        qLine(T({ en: '  fresh queue message, same transaction', tr: '  yeni kuyruk mesajı, aynı transaction' }), 't-c');
        qSetStatus('Queued');
        messages = 2;
        qSetCount(Q.outbox, Q.boxes[1], messages);
        await MM.sleep(900);
        qLine(T({ en: 'consumer: claim again, resume after row ' + processed, tr: 'tüketici: yeniden sahiplen, ' + processed + '. satırdan sonra devam et' }), 't-h');
        qSetStatus('Running');
        await MM.sleep(500);
      }

      var first = b * BATCH + 1, r;
      for (r = first; r < first + BATCH; r++) Q.cells[r - 1].className = 'qrow cur';
      await MM.sleep(750);
      for (r = first; r < first + BATCH; r++) {
        if (bad && REJECTS[r]) {
          Q.cells[r - 1].className = 'qrow rej';
          rejected += 1;
          errors.push(r + ' ' + REJECTS[r]);
        } else {
          Q.cells[r - 1].className = 'qrow done';
          adjusted += 1;
        }
      }
      processed += BATCH;
      if (b === 0 || b === 2) await poll();
    }

    await MM.sleep(500);
    qSetStatus('Succeeded');
    qLine('→ GET  …/j-1', 't-p');
    await MM.sleep(350);
    qLine('← 200  Succeeded  ' + adjusted + ' adjusted, ' + rejected + ' rejected', 't-g');
    if (errors.length) qLine('  rowErrors: ' + errors.join(', '), 't-r');

    await MM.sleep(300);
    var en = 'One job, ' + messages + (messages > 1 ? ' queue messages' : ' queue message') + '. ' + adjusted + ' rows adjusted, ' + rejected +
      ' rejected. The request was answered long before the last row was done.';
    var tr = 'Tek iş, ' + messages + ' kuyruk mesajı. ' + adjusted + ' satır düzeltildi, ' + rejected +
      ' reddedildi. İstek, son satır bitmeden çok önce yanıtlandı.';
    if (dbl) {
      en += ' The double click wrote nothing new.';
      tr += ' Çift tıklama yeni bir şey yazmadı.';
    }
    if (didKill) {
      en += ' The crash cost one requeue: the job resumed after row ' + resumedAfter + ' and rows 1 to ' + resumedAfter + ' did not run twice.';
      tr += ' Çökme tek bir yeniden kuyruğa almaya mal oldu: iş ' + resumedAfter + '. satırdan sonra devam etti, 1 ile ' + resumedAfter + '. satırlar iki kez çalışmadı.';
    }
    Q.verdict.className = 'ord-verdict ok';
    Q.verdict.textContent = '✓ ' + T({ en: en, tr: tr });

    Q.running = false;
    qGo.disabled = false;
    qToggles.forEach(function (c) { c.disabled = false; });
  }

  qGo.addEventListener('click', qRun);

  /* Lab 2: a projection that missed an event, healed by reconciliation. */

  var NAMES = [
    ['Wool sock', 'Merino sock', 'Thermal sock', 'Ski sock'],
    ['Rain jacket', 'Storm jacket', 'Shell jacket', 'Fleece jacket'],
    ['Trail cap', 'Sun cap', 'Wool cap', 'Rain cap'],
    ['Tin mug', 'Steel mug', 'Camp mug', 'Enamel mug']
  ];

  var P = { busy: false };
  var pStage = $('#pStage'), pLose = $('#pLose'), pRenameBtn = $('#pRename'), pRecBtn = $('#pRec'), pResetBtn = $('#pReset');

  function pInit() {
    P.src = NAMES.map(function (n) { return { name: n[0], v: 1 }; });
    P.copy = NAMES.map(function (n) { return { name: n[0], v: 1 }; });
    P.next = 0;
    P.event = null;
    P.recDone = null;
    P.pages = 0;
    P.healed = 0;
  }

  function pBusy(on) {
    P.busy = on;
    pRenameBtn.disabled = on;
    pRecBtn.disabled = on;
    pResetBtn.disabled = on;
  }

  function pPaint(i) {
    var s = P.src[i], c = P.copy[i], r = P.rows[i], drift = c.v < s.v;
    r.sn.textContent = s.name;
    r.sv.textContent = 'v' + s.v;
    r.cn.textContent = c.name;
    r.cv.textContent = 'v' + c.v;
    r.row.classList.toggle('drift', drift);
    r.mark.textContent = drift ? '≠' : '=';
  }

  function pStatus() {
    var labels = {
      sent: T({ en: 'in flight', tr: 'yolda' }),
      lost: T({ en: 'lost', tr: 'kayboldu' }),
      applied: T({ en: 'applied (newer than the row)', tr: 'uygulandı (satırdan yeni)' })
    };
    P.evt.className = 'pevent' + (P.event ? ' ' + P.event.state : '');
    P.evt.replaceChildren(
      el('code', { text: 'ProductChangedIntegrationEvent' }),
      P.event ? el('code', { text: 'v' + P.event.v + ' · ' + P.event.name }) : el('span', { text: T({ en: 'no event yet', tr: 'henüz olay yok' }) }),
      P.event ? el('b', { text: labels[P.event.state] }) : null);

    P.info.textContent = T({ en: 'reconciliation: pages read ', tr: 'reconciliation: okunan sayfa ' }) + P.pages +
      ' · ' + T({ en: 'rows healed ', tr: 'onarılan satır ' }) + P.healed;

    var d = -1;
    for (var i = 0; i < NAMES.length; i++) {
      if (P.copy[i].v < P.src[i].v) { d = i; break; }
    }
    if (d >= 0) {
      P.verdict.className = 'ord-verdict bad';
      P.verdict.textContent = '✕ ' + T({
        en: 'The stock list still shows “' + P.copy[d].name + '”. Products already says “' + P.src[d].name + '”.',
        tr: 'Stok listesi hâlâ “' + P.copy[d].name + '” gösteriyor. Products artık “' + P.src[d].name + '” diyor.'
      });
    } else if (P.recDone) {
      var n = P.recDone.healed, pages = P.recDone.pages;
      P.verdict.className = 'ord-verdict ok';
      P.verdict.textContent = n > 0
        ? '✓ ' + T({
          en: 'Healed ' + n + (n > 1 ? ' rows' : ' row') + ' in ' + pages + ' pages. Nothing on the request path ever asked Products.',
          tr: n + ' satır ' + pages + ' sayfada onarıldı. İstek yolundaki hiçbir şey Products\'a sormadı.'
        })
        : '✓ ' + T({ en: 'Nothing to heal, the copy was already in step.', tr: 'Onarılacak bir şey yok, kopya zaten günceldi.' });
    } else if (P.event && P.event.state === 'applied') {
      P.verdict.className = 'ord-verdict ok';
      P.verdict.textContent = '✓ ' + T({
        en: 'The event arrived and the copy took v' + P.event.v + '.',
        tr: 'Olay geldi ve kopya v' + P.event.v + '\'i aldı.'
      });
    } else {
      P.verdict.className = 'ord-verdict';
      P.verdict.textContent = '';
    }
  }

  function pBuild() {
    pStage.replaceChildren();
    P.rows = [];
    var table = el('div', { class: 'ptable' },
      el('div', { class: 'prow phead' }, el('span', { text: 'Products' }), el('span', { text: 'Inventory · ProductSummary' }), el('span')));
    NAMES.forEach(function () {
      var r = { row: el('div', { class: 'prow' }), sn: el('b'), sv: el('i', { class: 'pv' }), cn: el('b'), cv: el('i', { class: 'pv' }), mark: el('span', { class: 'pmark' }) };
      r.s = el('span', { class: 'pcell' }, r.sn, r.sv);
      r.c = el('span', { class: 'pcell copy' }, r.cn, r.cv);
      r.row.appendChild(r.s);
      r.row.appendChild(r.c);
      r.row.appendChild(r.mark);
      table.appendChild(r.row);
      P.rows.push(r);
    });
    P.evt = el('div', { class: 'pevent' });
    P.info = el('div', { class: 'pinfo' });
    P.verdict = el('div', { class: 'ord-verdict' });
    pStage.appendChild(P.evt);
    pStage.appendChild(table);
    pStage.appendChild(P.info);
    pStage.appendChild(P.verdict);
    NAMES.forEach(function (_, i) { pPaint(i); });
    pStatus();
  }

  async function pRename() {
    if (P.busy) return;
    pBusy(true);
    var i = P.next, s = P.src[i];
    P.next = (P.next + 1) % NAMES.length;
    s.v += 1;
    s.name = NAMES[i][(s.v - 1) % NAMES[i].length];
    P.recDone = null;
    pPaint(i);
    restart(P.rows[i].s, 'blink-src');
    P.event = { v: s.v, name: s.name, state: 'sent' };
    pStatus();
    await MM.sleep(800);
    if (pLose.checked) {
      P.event.state = 'lost';
    } else {
      P.copy[i] = { name: s.name, v: s.v };
      P.event.state = 'applied';
      pPaint(i);
      restart(P.rows[i].c, 'blink-ok');
    }
    pStatus();
    pBusy(false);
  }

  async function pReconcile() {
    if (P.busy) return;
    pBusy(true);
    P.pages = 0;
    P.healed = 0;
    P.recDone = null;
    var order = [3, 2, 1, 0];
    for (var p = 0; p < order.length; p += 2) {
      var page = [order[p], order[p + 1]];
      page.forEach(function (i) { P.rows[i].row.classList.add('scan'); });
      P.pages += 1;
      pStatus();
      await MM.sleep(850);
      page.forEach(function (i) {
        var s = P.src[i];
        if (s.v > P.copy[i].v) {
          P.copy[i] = { name: s.name, v: s.v };
          P.healed += 1;
          pPaint(i);
          restart(P.rows[i].c, 'blink-ok');
        }
        P.rows[i].row.classList.remove('scan');
      });
      pStatus();
    }
    P.recDone = { healed: P.healed, pages: P.pages };
    pStatus();
    pBusy(false);
  }

  pRenameBtn.addEventListener('click', pRename);
  pRecBtn.addEventListener('click', pReconcile);
  pResetBtn.addEventListener('click', function () {
    if (P.busy) return;
    pInit();
    pBuild();
  });

  MM.onLang(function () {
    if (!Q.running) qBuild();
    if (!P.busy) pBuild();
  });

  qBuild();
  pInit();
  pBuild();
})();

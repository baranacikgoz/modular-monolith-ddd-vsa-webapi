(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, T = MM.t;
  var REPO = 'https://github.com/baranacikgoz/modular-monolith-ddd-vsa-webapi';

  function c(en, tr) { return { comment: { en: en, tr: tr } }; }

  var TABS = {
    run: [
      c('clone the repository', 'depoyu klonla'),
      'git clone ' + REPO + '.git',
      'cd modular-monolith-ddd-vsa-webapi',
      '',
      c('one shared Docker network, created once', 'ortak bir Docker ağı, bir kez oluşturulur'),
      'docker network create local_shared_network',
      '',
      c('backing services (Keycloak imports the realm on first start)', 'arka plan servisleri (Keycloak realm\'i ilk açılışta içe aktarır)'),
      'docker compose up -d mm.postgres mm.rabbitmq mm.redis mm.keycloak mm.aspire-dashboard',
      '',
      c('create the database and apply every module\'s migrations (local development only)', 'veritabanını oluştur ve her modülün migration\'larını uygula (yalnızca yerel geliştirme)'),
      'make ef-update-all',
      '',
      c('build and start the API on http://localhost:5001', 'API\'yi derle ve http://localhost:5001 üzerinde başlat'),
      'docker compose up --build host'
    ],
    signin: [
      c('ask for a one-time code (in development it is always 123456)', 'tek kullanımlık kod iste (geliştirmede her zaman 123456)'),
      'curl -s -X POST http://localhost:5001/otp/login \\',
      '  -H \'Content-Type: application/json\' \\',
      '  -d \'{"phoneNumber":"901111111111"}\'',
      '',
      c('exchange the code for an access token and a refresh token', 'kodu erişim ve yenileme tokenı ile değiştir'),
      'curl -s -X POST http://localhost:5001/tokens \\',
      '  -H \'Content-Type: application/json\' \\',
      '  -d \'{"phoneNumber":"901111111111","otp":"123456","deviceId":"00000000-0000-4000-8000-000000000001","clientId":"mobile-app-1"}\''
    ],
    split: [
      c('the infrastructure from the Run tab must be up. Both setups use port 5001, so stop the single process first', 'Çalıştır sekmesindeki altyapı ayakta olmalı. İki kurulum da 5001 portunu kullanır, bu yüzden önce tek süreci durdur'),
      'docker compose stop host',
      '',
      c('the same image twice: IAM on 5001, Products on 5002', 'aynı imaj iki kez: IAM 5001, Products 5002 portunda'),
      'docker compose -f docker-compose.split.yml up --build',
      '',
      c('a call that crosses the process boundary through RabbitMQ, with no HTTP between the two', 'iki süreç arasında HTTP olmadan RabbitMQ üzerinden süreç sınırını geçen bir çağrı'),
      'curl "http://localhost:5002/v1/probe/cross-module?count=3"'
    ],
    test: [
      c('one module', 'tek modül'),
      'make test-products',
      '',
      c('everything, one test project after another', 'her şey, test projeleri sırayla'),
      'make test',
      '',
      c('fails when a model migration has no committed SQL script', 'bir model migration\'ının commit edilmiş SQL betiği yoksa başarısız olur'),
      'make check-migration-drift'
    ]
  };

  var DOCS = [
    { f: 'docs/getting-started.md', t: { en: 'Getting started', tr: 'Başlangıç' }, d: { en: 'Run the system, try the API, run the tests, split the deployment, run the load test.', tr: 'Sistemi çalıştır, API\'yi dene, testleri koş, dağıtımı böl, yük testini çalıştır.' } },
    { f: 'docs/ARCHITECTURE.md', t: { en: 'Architecture', tr: 'Mimari' }, d: { en: 'Understand why the architecture looks the way it does.', tr: 'Mimarinin neden bu şekilde olduğunu anla.' } },
    { f: 'docs/modules.md', t: { en: 'Modules', tr: 'Modüller' }, d: { en: 'Every module\'s routes, scopes, events, requests, jobs and settings.', tr: 'Her modülün rotaları, scope\'ları, olayları, istekleri, işleri ve ayarları.' } },
    { f: 'docs/platform.md', t: { en: 'Platform', tr: 'Platform' }, d: { en: 'How the shared building blocks work and when to use each.', tr: 'Ortak yapı taşlarının nasıl çalıştığı ve hangisinin ne zaman kullanılacağı.' } },
    { f: 'docs/full-text-search.md', t: { en: 'Full-text search', tr: 'Tam metin arama' }, d: { en: 'Add or tune multilingual search.', tr: 'Çok dilli aramayı ekle ya da ayarla.' } },
    { f: 'docs/split-deployment-poc.md', t: { en: 'Split deployment', tr: 'Bölünmüş dağıtım' }, d: { en: 'Watch two processes cooperate with no HTTP between them.', tr: 'İki sürecin aralarında HTTP olmadan işbirliği yapışını izle.' } },
    { f: 'docs/ai-workflow.md', t: { en: 'AI workflow', tr: 'Yapay zeka iş akışı' }, d: { en: 'Use or adapt the agent workflow.', tr: 'Ajan iş akışını kullan ya da uyarla.' } },
    { f: 'keycloak/README.md', t: { en: 'Keycloak realm', tr: 'Keycloak realm\'i' }, d: { en: 'Edit the realm, understand the permission model, rotate secrets.', tr: 'Realm\'i düzenle, izin modelini anla, sırları yenile.' } }
  ];

  var term = $('#term'), copyBtn = $('#termCopy'), tab = 'run', runId = 0;

  function plain(tabKey) {
    return TABS[tabKey].map(function (l) {
      if (typeof l === 'string') return l;
      return '# ' + T(l.comment);
    }).join('\n');
  }

  async function show(tabKey, instant) {
    var id = ++runId;
    term.replaceChildren();
    for (var i = 0; i < TABS[tabKey].length; i++) {
      if (id !== runId) return;
      var l = TABS[tabKey][i], line;
      if (l === '') {
        line = document.createTextNode('');
      } else if (typeof l === 'string') {
        var cont = l.charAt(0) === ' ';
        line = el('span', null, cont ? el('span', { class: 't-p', text: '  ' }) : el('span', { class: 't-p', text: '$ ' }), document.createTextNode(l));
      } else {
        line = el('span', { class: 't-c', text: '# ' + T(l.comment) });
      }
      term.appendChild(line);
      term.appendChild(document.createTextNode('\n'));
      if (!instant && !MM.reduced) await new Promise(function (r) { setTimeout(r, 55); });
    }
  }

  MM.$$('#termTabs button').forEach(function (b) {
    b.addEventListener('click', function () {
      tab = b.getAttribute('data-tab');
      MM.$$('#termTabs button').forEach(function (x) { x.setAttribute('aria-selected', String(x === b)); });
      show(tab, false);
    });
  });

  function fallbackCopy(text) {
    var ta = el('textarea', { style: 'position:fixed;opacity:0' });
    ta.value = text;
    document.body.appendChild(ta);
    ta.select();
    try { document.execCommand('copy'); } catch (e) { /* clipboard unavailable */ }
    ta.remove();
  }

  copyBtn.addEventListener('click', function () {
    var text = plain(tab);
    var done = function () {
      copyBtn.textContent = T({ en: 'Copied', tr: 'Kopyalandı' });
      setTimeout(function () { copyBtn.textContent = T({ en: 'Copy', tr: 'Kopyala' }); }, 1600);
    };
    if (navigator.clipboard && navigator.clipboard.writeText) {
      navigator.clipboard.writeText(text).then(done, function () { fallbackCopy(text); done(); });
    } else {
      fallbackCopy(text);
      done();
    }
  });

  function renderDocs() {
    var ul = $('#docs');
    ul.replaceChildren();
    DOCS.forEach(function (d) {
      ul.appendChild(el('li', null,
        el('a', { class: 'doc', href: REPO + '/blob/main/' + d.f, target: '_blank', rel: 'noopener' },
          el('b', null, T(d.t), el('span', { text: '↗' })),
          el('small', { text: T(d.d) }))));
    });
  }

  MM.onLang(function () { show(tab, true); renderDocs(); });
  renderDocs();
  show('run', true);
})();

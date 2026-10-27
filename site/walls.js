(function () {
  'use strict';

  var MM = window.MM, $ = MM.$, el = MM.el, T = MM.t;

  function roman(n) {
    var out = '';
    [[10, 'X'], [9, 'IX'], [5, 'V'], [4, 'IV'], [1, 'I']].forEach(function (p) {
      while (n >= p[0]) { out += p[1]; n -= p[0]; }
    });
    return out;
  }

  var RULES = [
    {
      rule: { en: 'A module never depends on another module.', tr: 'Bir modül asla başka bir modüle bağlı olmaz.' },
      file: 'src/Modules/Products/Products.Endpoints/Products.Endpoints.csproj',
      code: '<ItemGroup>\n  <ProjectReference Include="..\\..\\..\\Common\\Common.Endpoints\\Common.Endpoints.csproj"/>\n  «<ProjectReference Include="..\\..\\Inventory\\Inventory.Domain\\Inventory.Domain.csproj"/>»\n</ItemGroup>',
      by: ['ModuleBoundaryTests', 'pre-commit-guard.sh'],
      msg: '⛔ COMMIT BLOCKED: cross-module references detected:'
    },
    {
      rule: { en: 'Integration events live only in Common.IntegrationEvents.', tr: 'Entegrasyon olayları yalnızca Common.IntegrationEvents içinde yaşar.' },
      file: 'src/Modules/Inventory/Inventory.Application/StockChanged.cs',
      code: 'namespace Inventory.Application;\n\n«public sealed record StockChanged(Guid ProductId, int Quantity) : IntegrationEvent;»',
      by: ['ModuleBoundaryTests']
    },
    {
      rule: { en: 'Domain events carry only frozen payload types.', tr: 'Domain olayları yalnızca dondurulmuş payload türleri taşır.' },
      file: 'Products.Domain/Stores/DomainEvents/v1/V1StoreRenamedDomainEvent.cs',
      code: 'public sealed record V1StoreRenamedDomainEvent(«Store Store») : DomainEvent;',
      by: ['DomainEventContractTests']
    },
    {
      rule: { en: 'A query never compares a strongly typed id through .Value.', tr: 'Bir sorgu, güçlü tipli bir id\'yi asla .Value üzerinden karşılaştırmaz.' },
      file: 'Endpoint.cs',
      code: 'db.Stores\n  .AsNoTracking()\n  .Where(m => «m.Id.Value == rawGuid»)\n  .Select(m => new Response { /* … */ })',
      by: ['StronglyTypedIdQueryTests']
    },
    {
      rule: { en: 'Aggregate methods take child entities, never child ids.', tr: 'Aggregate metotları alt varlıkları alır, alt id\'leri asla.' },
      file: 'Order.cs',
      code: 'public Result RemoveLine(«OrderLineId id»)\n{\n    var line = «_lines.SingleOrDefault(l => l.Id == id)»;\n    // …\n}',
      by: ['AggregateChildEntityParameterTests']
    },
    {
      rule: { en: 'Only the StockLevel aggregate writes the stock ledger.', tr: 'Stok defterine yalnızca StockLevel aggregate\'i yazar.' },
      file: 'src/Modules/Inventory/Inventory.Infrastructure/Fixups/TidyLedgerJob.cs',
      code: 'foreach (var movement in oldMovements)\n    «db.StockMovements.Remove(movement);»',
      by: ['StockMovementWriteGuardTests']
    },
    {
      rule: { en: 'Every endpoint scope exists in the Keycloak realm, and every realm scope is used.', tr: 'Her uç nokta scope\'u Keycloak realm\'inde vardır ve her realm scope\'u kullanılır.' },
      file: 'src/Common/Common.Application/Auth/KeycloakScopes.cs',
      code: 'public static class Products\n{\n    «public const string Teleport = "products:teleport";»\n}\n// keycloak/realm-modular-monolith.json has no such scope',
      by: ['PermissionCoverageTests']
    },
    {
      rule: { en: 'Handlers that bind request parts declare [AsParameters].', tr: 'İstek parçalarını bağlayan handler\'lar [AsParameters] bildirir.' },
      file: 'Endpoint.cs',
      code: 'private static async Task<Result> HandleAsync(\n    «Request request»,\n    IProductsDbContext db,\n    CancellationToken ct)',
      by: ['EndpointParameterBindingTests']
    },
    {
      rule: { en: 'Every model migration has a committed SQL script.', tr: 'Her model migration\'ının commit edilmiş bir SQL betiği vardır.' },
      file: 'terminal',
      code: '$ make ef-add-Products name=AddPriceColumn\n$ git add -A && git commit -m "price column"\n«# no ef-script, so no migrations/Products/*.sql»',
      by: ['make check-migration-drift', 'CI']
    },
    {
      rule: { en: 'The app never starts against an unmigrated database.', tr: 'Uygulama, migration\'ı uygulanmamış bir veritabanına karşı asla açılmaz.' },
      file: 'terminal',
      code: '$ docker compose up --build host\n«# the database still has pending migrations»',
      by: ['MigrationGuard']
    },
    {
      rule: { en: 'Production never boots with a fake provider.', tr: 'Production hiçbir zaman sahte bir sağlayıcıyla açılmaz.' },
      file: 'sms.json',
      code: '{\n  "SmsOptions": {\n    «"Provider": "Dummy"»\n  }\n}',
      by: [{ en: 'options validator at startup', tr: 'açılışta seçenek doğrulayıcısı' }],
      msg: 'SmsOptions.Provider is \'Dummy\' in Production. Dummy SMS gateway is a no-op: OTPs and notifications would never reach users.'
    },
    {
      rule: { en: 'The server never calls an address that leads into its own network.', tr: 'Sunucu, kendi ağının içine çıkan bir adresi asla çağırmaz.' },
      file: 'POST /v1/warehouse-connections',
      code: '{\n  "provider": "JsonApi",\n  "baseUrl": "«https://169.254.169.254/latest/meta-data/»"\n}',
      by: ['RequestValidator', 'WarehouseConnectGuard'],
      msg: 'Base URL cannot lead to a local, private or reserved network address.'
    },
    {
      rule: { en: 'Zero warnings.', tr: 'Sıfır uyarı.' },
      file: 'Program.cs',
      code: '«var unused = 42;»',
      by: ['TreatWarningsAsErrors'],
      msg: 'error CS0219: The variable \'unused\' is assigned but its value is never used'
    }
  ];

  var tabletsEl = $('#tablets'), countEl = $('#wallCount'), doneEl = $('#wallDone'), allBtn = $('#wallAll');
  var tried = {}, nodes = [], busy = false;

  function codeNodes(src) {
    return src.split(/(«[^»]*»)/).map(function (part) {
      if (part.charAt(0) === '«') return el('span', { class: 'bad', text: part.slice(1, -1) });
      return document.createTextNode(part);
    });
  }

  function count() { return Object.keys(tried).length; }

  function paintCount() {
    countEl.textContent = String(count());
    if (count() === RULES.length) {
      doneEl.innerHTML = T({ en: 'The compiler does not have <em>deadlines.</em>', tr: 'Derleyicinin <em>teslim tarihi yoktur.</em>' });
    } else {
      doneEl.textContent = '';
    }
  }

  function setOpen(i, open) {
    var n = nodes[i];
    n.tablet.classList.toggle('open', open);
    n.btn.setAttribute('aria-expanded', String(open));
    n.btn.textContent = open ? T({ en: 'Blocked. Close', tr: 'Engellendi. Kapat' }) : T({ en: 'Try to break it', tr: 'Bozmayı dene' });
    if (open) {
      tried[i] = true;
      n.tablet.classList.remove('shake');
      void n.tablet.offsetWidth;
      n.tablet.classList.add('shake');
    }
    paintCount();
  }

  function render() {
    var openSet = nodes.map(function (n) { return n.tablet.classList.contains('open'); });
    tabletsEl.replaceChildren();
    nodes = RULES.map(function (r, i) {
      var btn = el('button', { class: 'tablet-btn', type: 'button', 'aria-expanded': 'false' });
      var by = el('span', null, r.by.map(function (b, k) { return [k ? ' · ' : '', el('code', { text: T(b) })]; }));
      var stamp = el('div', { class: 'stamp' }, el('b', { text: T({ en: 'blocked', tr: 'engellendi' }) }), by);
      var parts = [
        el('p', { class: 'demo-file', text: r.file }),
        el('pre', { class: 'demo-code' }, codeNodes(r.code)),
        stamp
      ];
      if (r.msg) parts.splice(2, 0, el('pre', { class: 'demo-code', style: 'margin-top:8px;color:#ffb0b0;white-space:pre-wrap' }, r.msg));
      var tablet = el('article', { class: 'tablet' },
        el('span', { class: 'tablet-n', text: roman(i + 1) }),
        el('p', { class: 'tablet-t', text: T(r.rule) }),
        btn,
        el('div', { class: 'tablet-demo' }, el('div', null, parts)));
      btn.addEventListener('click', function () { setOpen(i, !tablet.classList.contains('open')); });
      tabletsEl.appendChild(tablet);
      return { tablet: tablet, btn: btn };
    });
    nodes.forEach(function (n, i) { setOpen(i, !!(openSet[i] && tried[i])); });
    paintCount();
  }

  allBtn.addEventListener('click', async function () {
    if (busy) return;
    busy = true;
    allBtn.disabled = true;
    for (var i = 0; i < RULES.length; i++) {
      if (!nodes[i].tablet.classList.contains('open')) {
        setOpen(i, true);
        await MM.sleep(380);
      }
    }
    busy = false;
    allBtn.disabled = false;
  });

  MM.setNum('walls', RULES.length);
  MM.onLang(render);
  render();
})();

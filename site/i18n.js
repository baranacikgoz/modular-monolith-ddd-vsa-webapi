(function () {
  'use strict';

  var MM = window.MM;

  MM.TR = {
    'skip': 'İçeriğe geç',
    'nav.deploy': 'Dağıtım',
    'nav.flight': 'Uçuş',
    'nav.walls': 'Duvarlar',
    'nav.atlas': 'Atlas',
    'nav.rails': 'Raylar',
    'nav.agents': 'Ajanlar',
    'nav.start': 'Başla',
    'nav.work': 'Hizmetler',
    'menu.aria': 'Menü',
    'slab.aria': 'Monoliti böl',
    'slab.hint': 'monolite tıkla',
    'h1.a': 'Monolit olarak geliştir.',
    'h1.b': '<em>Mikroservis</em> olarak yayınla.',
    'hero.lede': 'Tek imaj. Tek ortam değişkeni. Modüller aradaki farkı hiç anlamadı.',
    'hero.cta1': 'Monoliti böl',
    'hero.cta2': 'Dört adımda çalıştır',
    'deploy.eyebrow': 'Dağıtım',
    'deploy.h': 'Aynı imaj. <em>Farklı topoloji.</em>',
    'topo.mono': 'Monolit',
    'topo.duo': 'İki süreç',
    'topo.quad': 'Dört süreç',
    'stage.cycle': 'şekli değiştirir',
    'stage.click': 'bir modüle tıklayarak onu konuştur',
    'stage.zero': 'değişen çağrı noktası',
    'leg.event': 'entegrasyon olayı (asenkron)',
    'leg.req': 'modüller arası istek (senkron)',
    'leg.core': 'çekirdek modül, her örnekte yüklenir',
    'cnt.all': 'mesaj',
    'cnt.cross': 'süreç sınırını geçen',
    'fl.eyebrow': 'Uçuş kaydedici',
    'fl.h': 'Tek bir isteği izle. <em>Sonra bozmayı dene.</em>',
    'fl.p': 'Bir mağaza sahibi mağazasına ürün ekler. <span data-num="hops" data-word>On</span> adım sonra Inventory modülünde bir stok kaydı oluşur. Bir arıza ekle, oynat ve hangi garantinin devreye girdiğini izle.',
    'fl.chaos': 'Arıza ekle',
    'fl.a': 'Commit öncesi çökme',
    'fl.b': 'Commit sonrası hemen çökme',
    'fl.c': 'Olayı iki kez teslim et',
    'fl.play': 'Oynat',
    'fl.step': 'Adım',
    'fl.reset': 'Sıfırla',
    'ord.h': 'Sıra garanti edilmez',
    'ord.p': 'İşlemci bir grubu paralel yayınlar ve tüketiciler eşzamanlı çalışır, bu yüzden olaylar herhangi bir sırada gelebilir. Olaylarla beslenen bir okuma modeli kaynak sürümünü taşır ve eskimiş ya da tekrarlanan olayı eleyen UpsertIfNewerAsync ile uygular.',
    'ord.naive': 'Sıra varsayar',
    'ord.go': 'Karıştır ve teslim et',
    'q.h': 'Ağır iş asla istek içinde çalışmaz',
    'q.p': 'Uç nokta dosyayı saklar, tek transaction içinde bir iş satırı ve bir kuyruk mesajı yazar ve sorgulanacak bir işle 202 yanıtlar. Dosyayı sonra bir tüketici partiler halinde işler. Bir şeyi boz ve gönder.',
    'q.dbl': 'Çift tıklama',
    'q.bad': 'İki hatalı satır',
    'q.kill': 'Tüketiciyi yarı yolda öldür',
    'q.go': 'Dosyayı gönder',
    'p.h': 'Bir kopya bir olayı kaçırabilir',
    'p.p': 'Inventory, ihtiyaç duyduğu ürün bilgilerinin yerel bir kopyasını tutar, bu yüzden bir istek Products\'a asla sormaz. Olaylar kopyayı taze tutar, yine de bir kopya olayı kaçırabilir. Bir ürünü yeniden adlandır, olayı kaybet, sonra reconciliation işini çalıştır: Products\'ı sayfa sayfa gezer ve satırı, satırın sürümüyle korunarak onarır.',
    'p.lose': 'Olayı kaybet',
    'p.rename': 'Bir ürünü yeniden adlandır',
    'p.rec': 'Reconciliation\'ı çalıştır',
    'walls.eyebrow': 'Korumalar',
    'walls.h': 'Gelenekler çürür. <em>Duvarlar çürümez.</em>',
    'walls.p': '<span data-num="walls" data-word>On üç</span> kuralın her biri bir test, bir açılış doğrulayıcısı, bir hook ya da CI tarafından denetlenir. Bir kural seç ve bozmayı dene.',
    'walls.count': 'deneme engellendi',
    'walls.all': 'Hepsini bozmayı dene',
    'atlas.eyebrow': 'Modüller',
    'atlas.h': '<span data-num="modules" data-word>Altı</span> modül. <em>Sıfır referans.</em>',
    'atlas.p': 'Birbirlerini yalnızca sözleşmeler üzerinden tanırlar. Kesik çizgiler entegrasyon olaylarını, düz çizgiler modüller arası istekleri gösterir ve hepsi RabbitMQ üzerinden geçer. Bir modül seç.',
    'rails.eyebrow': 'Ray',
    'rails.h': 'Mutlu yol <em>kodun kendisidir.</em>',
    'rails.p': 'Bir uç nokta tek bir zincirdir. Herhangi bir istasyondaki hata treni hata rayına geçirir ve yerelleştirilmiş bir ProblemDetails ile biter. Bir makası çevir ve treni gönder.',
    'rails.run': 'Treni gönder',
    'rails.clear': 'Makasları temizle',
    'rails.stat': 'elle yazılmış',
    'rails.note': 'Hatalar gösteri için enjekte edilir. Activate metodunun kendisi her zaman başarılı olur. Boru hattı ve hata eşlemesi gerçektir.',
    'ag.eyebrow': 'Ajanlar için yapıldı',
    'ag.h': 'İnsanlar <em>ve ajanları için yazıldı.</em>',
    'ag.p': 'Katı mimari, bir yapay zeka ajanını işe yarar kılan şeydir. Depo; kural kitabını, iş akışını ve korumaları birlikte sunar ve kodu kullanmak için bunların hiçbiri zorunlu değildir.',
    'ag.cmds': '<span data-num="commands" data-word>On beş</span> slash komutu',
    'ag.hooks': 'Hayır diyen iki hook',
    'ag.h1': 'Bir .csproj düzenle',
    'ag.h2': 'git commit',
    'ag.rules': 'Kural kitabı: CLAUDE.md',
    'ag.graph': 'Grep yerine bir graf',
    'ag.graphp': 'Üretilmiş bir bilgi grafı, ağacı taramadan bir şeyin nerede olduğunu söyler. Görsel süs içindir, sayılar deponun kendi raporundandır.',
    'ag.nodes': 'düğüm',
    'ag.edges': 'kenar',
    'ag.comm': 'topluluk',
    'n.modules': 'modül',
    'n.projects': 'proje',
    'n.cs': 'C# dosyası',
    'n.tests': 'test metodu',
    'n.commits': 'commit',
    'n.refs': 'çapraz modül referansı',
    'n.note': 'Sayılar her yayında depodan git ls-files, test öznitelikleri ve git rev-list ile sayılır.',
    'st.eyebrow': 'Başla',
    'st.h': 'Çalıştır. <em>Sonra böl.</em>',
    'st.p': '.NET 10 SDK, Docker, GNU Make ve EF Core CLI gerekir. Keycloak realm\'i ilk açılışta bir dosyadan içe aktarır ve geliştirmede tek kullanımlık kod her zaman 123456\'dır.',
    'st.t1': 'Çalıştır',
    'st.t2': 'Giriş yap',
    'st.t3': 'Böl',
    'st.t4': 'Test et',
    'st.copy': 'Kopyala',
    'docs.h': 'Dokümanları oku',
    'wk.eyebrow': 'Benimle çalışın',
    'wk.h': 'Bunu canlıda çalıştırıyorum. <em>Sizinkini canlıya almanıza yardım edebilirim.</em>',
    'wk.p': 'Bu şablon, Türkiye\'deki berber, kuaför ve kliniklere yönelik online randevu platformu <a href="https://randevufast.com" target="_blank" rel="noopener">randevufast.com</a>\'un backend temelidir. Aynı yaklaşımla kurulan ikinci bir canlı sistem ise, bir ürün tedarikçisinin satıcılarının Trendyol ve Hepsiburada mağazalarını bağlayıp tedarikçinin kataloğunu sattığı ve stokların hepsinde takip edildiği bir dropshipping platformudur.',
    'wk.a.t': 'Mimari inceleme',
    'wk.a.d': 'Kod tabanınızı, bu deponun testlerle denetlediği mimari kurallara göre incelerim ve dosya ile satır referansları içeren, öncelik sıralı yazılı bir rapor hazırlarım.',
    'wk.b.t': 'Uyarlama sprinti',
    'wk.b.d': 'Şablonu birlikte fork\'layıp projenize taşırız, şablondaki örnek modülleri ilk gerçek modülünüzle değiştiririz, mimari kuralları denetleyen testleri CI pipeline\'ınıza bağlarız ve şablonda çıkan güncellemeleri sonradan alabilmeniz için upstream akışını kurarız. Ekibiniz yapay zeka ajanları kullanıyorsa kural kitabı ve komutlar da kurulur. Kapsam sonuca göre değil, gün sayısına göre belirlenir.',
    'wk.c.t': 'Ekip atölyesi',
    'wk.c.d': 'Ayrıntılar için bana yazın.',
    'wk.note': 'Özel odak: pazaryeri ve depo stok senkronizasyonunun güvenilir kurulması.',
    'wk.cta': 'Bana yazın',
    'ft.license': 'Apache Lisansı 2.0',
    'ft.built': 'Deponun README, doküman ve kaynak koduna dayanarak hazırlandı.'
  };

  function capture() {
    MM.$$('[data-i18n]').forEach(function (n) {
      if (n.getAttribute('data-en') === null) n.setAttribute('data-en', n.innerHTML);
    });
    MM.$$('[data-i18n-aria]').forEach(function (n) {
      if (n.getAttribute('data-en-aria') === null) n.setAttribute('data-en-aria', n.getAttribute('aria-label') || '');
    });
  }

  function apply(lang) {
    MM.lang = lang;
    document.documentElement.lang = lang;
    MM.$$('[data-i18n]').forEach(function (n) {
      var tr = MM.TR[n.getAttribute('data-i18n')];
      n.innerHTML = lang === 'tr' && tr ? tr : n.getAttribute('data-en');
    });
    MM.$$('[data-i18n-aria]').forEach(function (n) {
      var tr = MM.TR[n.getAttribute('data-i18n-aria')];
      n.setAttribute('aria-label', lang === 'tr' && tr ? tr : n.getAttribute('data-en-aria'));
    });
    MM.$$('.lang button').forEach(function (b) {
      b.setAttribute('aria-pressed', String(b.getAttribute('data-lang') === lang));
    });
    MM.fillNums();
    document.dispatchEvent(new CustomEvent('langchange', { detail: lang }));
  }

  MM.setLang = function (lang) {
    apply(lang);
    try { localStorage.setItem('mm-lang', lang); } catch (e) { /* storage may be blocked */ }
  };

  capture();

  var saved = null;
  try { saved = localStorage.getItem('mm-lang'); } catch (e) { /* storage may be blocked */ }
  var pref = saved || ((navigator.language || '').toLowerCase().indexOf('tr') === 0 ? 'tr' : 'en');
  if (pref === 'tr') apply('tr');
  else MM.fillNums();

  MM.$$('.lang button').forEach(function (b) {
    b.addEventListener('click', function () { MM.setLang(b.getAttribute('data-lang')); });
  });
})();

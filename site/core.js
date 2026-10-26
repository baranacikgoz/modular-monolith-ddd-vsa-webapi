(function () {
  'use strict';

  var MM = (window.MM = window.MM || {});
  var SVGNS = 'http://www.w3.org/2000/svg';

  MM.reduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  MM.lang = 'en';
  MM.colors = {
    IAM: '#b99cff',
    Products: '#6fe3a5',
    Inventory: '#5cc8ff',
    Notifications: '#ff8fb8',
    Outbox: '#ffb347',
    BackgroundJobs: '#a3acb9',
    signal: '#ff6a1f',
    bad: '#ff5252',
    ok: '#6fe3a5',
    ink: '#ece7db',
    dim: '#8a867e'
  };

  MM.$ = function (s, r) { return (r || document).querySelector(s); };
  MM.$$ = function (s, r) { return Array.prototype.slice.call((r || document).querySelectorAll(s)); };
  MM.clamp = function (v, a, b) { return Math.min(b, Math.max(a, v)); };
  MM.rand = function (a, b) { return a + Math.random() * (b - a); };
  MM.t = function (v) { return typeof v === 'string' ? v : (v[MM.lang] || v.en); };
  MM.sleep = function (ms) {
    return new Promise(function (r) { setTimeout(r, MM.reduced ? Math.min(ms, 70) : ms); });
  };

  function setAttrs(n, attrs) {
    if (!attrs) return;
    Object.keys(attrs).forEach(function (k) {
      var v = attrs[k];
      if (v == null || v === false) return;
      if (k === 'class') n.setAttribute('class', v);
      else if (k === 'text') n.textContent = v;
      else if (k === 'html') n.innerHTML = v;
      else if (k.slice(0, 2) === 'on' && typeof v === 'function') n.addEventListener(k.slice(2), v);
      else n.setAttribute(k, v === true ? '' : v);
    });
  }

  function append(n, args, from) {
    for (var i = from; i < args.length; i++) {
      var c = args[i];
      if (c == null || c === false) continue;
      if (Array.isArray(c)) append(n, c, 0);
      else n.appendChild(c.nodeType ? c : document.createTextNode(String(c)));
    }
  }

  MM.el = function (tag, attrs) {
    var n = document.createElement(tag);
    setAttrs(n, attrs);
    append(n, arguments, 2);
    return n;
  };

  MM.svg = function (tag, attrs) {
    var n = document.createElementNS(SVGNS, tag);
    setAttrs(n, attrs);
    append(n, arguments, 2);
    return n;
  };

  MM.onLang = function (fn) { document.addEventListener('langchange', fn); };

  MM.onVisible = function (node, cb, margin) {
    if (!('IntersectionObserver' in window)) { cb(true); return; }
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (e) { cb(e.isIntersecting); });
    }, { rootMargin: margin || '0px' });
    io.observe(node);
  };

  function initNav() {
    var nav = MM.$('#nav');
    var onScroll = function () { nav.classList.toggle('solid', window.scrollY > 24); };
    onScroll();
    window.addEventListener('scroll', onScroll, { passive: true });
  }

  function initReveal() {
    var items = MM.$$('.reveal');
    if (!('IntersectionObserver' in window) || MM.reduced) {
      items.forEach(function (n) { n.classList.add('in'); });
      return;
    }
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (e) {
        if (e.isIntersecting) { e.target.classList.add('in'); io.unobserve(e.target); }
      });
    }, { threshold: 0.08, rootMargin: '0px 0px -6% 0px' });
    items.forEach(function (n) { io.observe(n); });
  }

  function animateCount(node) {
    var target = Number(node.getAttribute('data-count'));
    var fmt = function (v) { return Math.round(v).toLocaleString('en-US'); };
    if (MM.reduced || target === 0) { node.textContent = fmt(target); return; }
    var t0 = performance.now();
    var dur = 1500;
    (function tick(now) {
      var p = MM.clamp((now - t0) / dur, 0, 1);
      var e = 1 - Math.pow(1 - p, 4);
      node.textContent = fmt(target * e);
      if (p < 1) requestAnimationFrame(tick);
    })(t0);
  }

  function initCounters() {
    var nodes = MM.$$('[data-count]');
    nodes.forEach(function (n) { n.textContent = '0'; });
    if (!('IntersectionObserver' in window)) { nodes.forEach(animateCount); return; }
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (e) {
        if (e.isIntersecting) { animateCount(e.target); io.unobserve(e.target); }
      });
    }, { threshold: 0.6 });
    nodes.forEach(function (n) { io.observe(n); });
  }

  function initDots() {
    var box = MM.$('#dots');
    var secs = [{ id: 'top', title: 'Top' }].concat(MM.$$('section[data-title]').map(function (s) {
      return { id: s.id, title: s.getAttribute('data-title') };
    }));
    var links = secs.map(function (s) {
      var a = MM.el('a', { href: '#' + s.id, 'aria-label': s.title }, MM.el('span', { text: s.title }));
      box.appendChild(a);
      return a;
    });
    var navLinks = MM.$$('.nav-links a');
    if (!('IntersectionObserver' in window)) return;
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (e) {
        if (!e.isIntersecting) return;
        var id = e.target.id;
        links.forEach(function (a, i) { a.classList.toggle('active', secs[i].id === id); });
        navLinks.forEach(function (a) { a.classList.toggle('active', a.getAttribute('href') === '#' + id); });
      });
    }, { rootMargin: '-45% 0px -50% 0px' });
    secs.forEach(function (s) { var n = document.getElementById(s.id); if (n) io.observe(n); });
  }

  function banner() {
    if (!window.console || !console.log) return;
    console.log(
      '%c  ▌▐  \n  ▌▐  Build it as a monolith.\n  ▌▐  Ship it as microservices.',
      'color:#ff6a1f;font:600 13px/1.4 ui-monospace,monospace'
    );
  }

  function init() {
    initNav();
    initReveal();
    initCounters();
    initDots();
    banner();
  }

  if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', init);
  else init();
})();

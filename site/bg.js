(function () {
  'use strict';

  var MM = window.MM;
  var cv = MM.$('#stars');
  var ctx = cv.getContext('2d');
  var W = 0, H = 0, stars = [], mx = 0, my = 0, tx = 0, ty = 0, shoot = null;
  var nextShoot = performance.now() + 5000;

  function resize() {
    var dpr = Math.min(window.devicePixelRatio || 1, 2);
    W = window.innerWidth;
    H = window.innerHeight;
    cv.width = W * dpr;
    cv.height = H * dpr;
    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
    var n = Math.round(MM.clamp((W * H) / 7000, 70, 260));
    stars = [];
    for (var i = 0; i < n; i++) {
      stars.push({ x: Math.random() * W, y: Math.random() * H, z: Math.random() * 0.8 + 0.2, ph: Math.random() * 6.28, sp: MM.rand(0.4, 1.4) });
    }
  }

  function spawnShoot(now) {
    var fromRight = Math.random() > 0.5;
    shoot = { x: fromRight ? W * MM.rand(0.5, 1) : W * MM.rand(0, 0.5), y: H * MM.rand(0, 0.35), vx: fromRight ? -1 : 1, t0: now, life: 900 };
    nextShoot = now + MM.rand(7000, 12000);
  }

  function draw(now) {
    ctx.clearRect(0, 0, W, H);
    mx += (tx - mx) * 0.04;
    my += (ty - my) * 0.04;
    var sy = window.scrollY;
    for (var i = 0; i < stars.length; i++) {
      var s = stars[i];
      var x = s.x + mx * s.z * 18;
      var y = ((((s.y - sy * 0.12 * s.z) % H) + H) % H) + my * s.z * 18;
      var a = (0.22 + 0.55 * s.z) * (0.65 + 0.35 * Math.sin(now * 0.001 * s.sp + s.ph));
      ctx.fillStyle = 'rgba(236,231,219,' + a.toFixed(3) + ')';
      var sz = s.z * 1.7;
      ctx.fillRect(x, y, sz, sz);
    }
    if (shoot) {
      var p = (now - shoot.t0) / shoot.life;
      if (p >= 1) shoot = null;
      else {
        var hx = shoot.x + shoot.vx * p * 420;
        var hy = shoot.y + p * 260;
        var g = ctx.createLinearGradient(hx, hy, hx - shoot.vx * 90, hy - 56);
        g.addColorStop(0, 'rgba(255,214,180,' + (1 - p).toFixed(3) + ')');
        g.addColorStop(1, 'rgba(255,214,180,0)');
        ctx.strokeStyle = g;
        ctx.lineWidth = 1.4;
        ctx.beginPath();
        ctx.moveTo(hx, hy);
        ctx.lineTo(hx - shoot.vx * 90, hy - 56);
        ctx.stroke();
      }
    } else if (now > nextShoot) spawnShoot(now);
  }

  function loop(now) {
    draw(now);
    requestAnimationFrame(loop);
  }

  resize();
  window.addEventListener('resize', function () { resize(); if (MM.reduced) draw(0); });
  if (MM.reduced) draw(0);
  else requestAnimationFrame(loop);

  var slab = MM.$('#slab');
  var hero = MM.$('#top');
  var envval = MM.$('#envval');
  var shards = MM.$$('.shard', slab);
  var burst = false, hover = false, scrollP = 0, pending = false, px = null, py = null;

  function paintSlab() {
    pending = false;
    var split = Math.max(scrollP, burst ? 1 : 0, hover ? 0.1 : 0);
    slab.style.setProperty('--split', split.toFixed(3));
    if (px !== null) {
      var nx = px / window.innerWidth - 0.5;
      var ny = py / window.innerHeight - 0.5;
      slab.style.setProperty('--ry', (nx * 12).toFixed(2) + 'deg');
      slab.style.setProperty('--rx', (-ny * 7).toFixed(2) + 'deg');
      shards.forEach(function (sh) {
        var r = sh.getBoundingClientRect();
        sh.style.setProperty('--gx', (px - r.left).toFixed(0) + 'px');
        sh.style.setProperty('--gy', (py - r.top).toFixed(0) + 'px');
      });
    }
  }

  function schedule() {
    if (pending) return;
    pending = true;
    requestAnimationFrame(paintSlab);
  }

  function onScroll() {
    scrollP = MM.clamp(window.scrollY / (hero.offsetHeight * 0.78), 0, 1);
    schedule();
  }

  slab.addEventListener('click', function () {
    burst = !burst;
    if (envval) envval.textContent = burst ? 'Products' : '*';
    schedule();
  });
  slab.addEventListener('pointerenter', function () { hover = true; schedule(); });
  slab.addEventListener('pointerleave', function () { hover = false; schedule(); });
  window.addEventListener('scroll', onScroll, { passive: true });
  window.addEventListener('pointermove', function (e) {
    px = e.clientX;
    py = e.clientY;
    tx = px / W - 0.5;
    ty = py / H - 0.5;
    schedule();
  }, { passive: true });
  onScroll();
})();

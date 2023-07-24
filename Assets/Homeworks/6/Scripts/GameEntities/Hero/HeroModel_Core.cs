using System;
using Atomic;
using Declarative;
using Homeworks5.Custom;
using UnityEngine;

namespace Homeworks5.Hero
{
    [Serializable]
    public class HeroModel_Core
    {
        [Section]
        [SerializeField]
        public LifeSection life = new LifeSection();

        [Section]
        [SerializeField]
        public Shoot shoot = new Shoot();

        [Section]
        [SerializeField]
        public ShootReload shootReload = new ShootReload();

        [Section]
        [SerializeField]
        public MoveSection move = new MoveSection();
        [HideInInspector] public AtomicVariable<bool> moveRequired;

        [Construct]
        public void Init(HeroModel model)
        {
            move.Init(model);

            life.onDeath += () =>
            {
                Debug.Log("ИГРА ЗАВЕРШЕНА");
                Time.timeScale = 0f;
            };

            move.onMove += dir =>
            {
                moveRequired.Value = true;
            };
            model.onFixedUpdate += deltaTime =>
            {
                if (moveRequired.Value)
                {
                    move.onMoveEvent.Invoke(deltaTime);
                    moveRequired.Value = false;
                }
            };
        }

        [Serializable]
        public class Shoot
        {
            [SerializeField] private Transform _shootPoint;
            [SerializeField] private GameObject _bulletPrefab;
            [SerializeField] public AtomicVariable<float> cooldownTime;
            [HideInInspector] public AtomicVariable<int> kills;  
            [HideInInspector] public AtomicVariable<bool> isReadyShoot;
            [HideInInspector] public AtomicEvent onShoot;
            [HideInInspector] public AtomicEvent onShootPerformed;
            [HideInInspector] public AtomicEvent onKilled;
            private BulletShooter _shooter = new BulletShooter();
            private Timer _timer;

            [Construct]
            public void Init(LifeSection life, ShootReload shootReloader, HeroModel model)
            {
                _timer = new Timer(cooldownTime.Value, model, true);
                _timer.onEnd += () => isReadyShoot.Value = true;
                onShoot.AddListener(() =>
                {
                    if (shootReloader.hasBullets.Value && isReadyShoot.Value && !life.isDead.Value)
                    {
                        isReadyShoot.Value = false;
                        _timer.ResetTimer();
                        _shooter.Shoot(_bulletPrefab, _shootPoint, _shootPoint.forward);
                        onShootPerformed?.Invoke();
                    }
                });
                onKilled += () =>
                {
                    kills.Value++;
                };
            }
        }

        [Serializable]
        public class ShootReload
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [SerializeField] public AtomicVariable<float> bulletRestoreCooldown;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> hasBullets;
            private AutoTimer _timer;

            [Construct]
            public void Init(Shoot shooter, HeroModel model)
            {
                _timer = new AutoTimer(bulletRestoreCooldown.Value, model);
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        hasBullets.Value = false;
                    else
                        hasBullets.Value = true;
                };
                _timer.onEnd += () =>
                {
                    if (currentBullets.Value < maxBullets.Value)
                        currentBullets.Value++;
                };
                shooter.onShootPerformed += () =>
                {
                    currentBullets.Value--;
                };
                currentBullets.Value = maxBullets.Value;
            }
        }
    }
}
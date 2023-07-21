using System;
using Atomic;
using Declarative;
using Homeworks5.Custom;
using Homeworks5.Custom.Wrappers;
using UnityEngine;

namespace Homeworks5.Hero
{
    [Serializable]
    public class HeroModel_Core
    {
        [Section]
        [SerializeField]
        public LifeSection life = new();

        [Section]
        [SerializeField]
        public Shooter shooter = new();

        [Section]
        [SerializeField]
        public ShootReloader shootReloader = new();

        [Section]
        [SerializeField]
        public MoveSection mover = new();
        [HideInInspector] public AtomicVariable<bool> moveRequired;

        [Construct]
        public void Init()
        {
            life.onDeath += () =>
            {
                Debug.Log("ИГРА ЗАВЕРШЕНА");
                Time.timeScale = 0f;
            };
            mover.onMove += dir =>
            {
                moveRequired.Value = true;
            };
            mover.onUpdated += deltaTime =>
            {
                if (moveRequired.Value)
                {
                    mover.onMoveEvent.Invoke(deltaTime);
                    moveRequired.Value = false;
                }
            };
        }

        [Serializable]
        public class Shooter
        {
            [SerializeField] private Transform _shootPoint;
            [SerializeField] private GameObject _bulletPrefab;
            [SerializeField] public AtomicVariable<float> cooldownTime;
            [HideInInspector] public AtomicVariable<int> kills;  
            [HideInInspector] public AtomicVariable<bool> isReadyShoot;
            [HideInInspector] public AtomicVariable<float> cooldownTimer;
            [HideInInspector] public AtomicEvent onShoot;
            [HideInInspector] public AtomicEvent onShootPerformed;
            private UpdateWrapper _updateWrapper = new();
            private ShootEngine _shooter = new();

            [Construct]
            public void Init(LifeSection life, ShootReloader shootReloader)
            {  
                cooldownTimer.Value = cooldownTime.Value;
                _updateWrapper.onUpdate += deltaTime =>
                {
                    if (cooldownTimer.Value < cooldownTime.Value)
                        cooldownTimer.Value += deltaTime;
                    else
                        isReadyShoot.Value = true;
                };
                onShoot.AddListener(() =>
                {
                    if (shootReloader.hasBullets.Value && isReadyShoot.Value && !life.isDead.Value)
                    {
                        isReadyShoot.Value = false;
                        cooldownTimer.Value = 0f;
                        _shooter.Shoot(_bulletPrefab, _shootPoint, _shootPoint.forward);
                        onShootPerformed?.Invoke();
                    }
                });
            }
        }

        [Serializable]
        public class ShootReloader
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [SerializeField] public AtomicVariable<float> bulletRestoreCooldown;
            [HideInInspector] public AtomicVariable<float> bulletRestoreTimer;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> hasBullets;
            private UpdateWrapper _updateWrapper = new();

            [Construct]
            public void Init(Shooter shooter)
            {
                currentBullets.Value = maxBullets.Value;
                bulletRestoreTimer.Value = bulletRestoreCooldown.Value;
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        hasBullets.Value = false;
                    else
                        hasBullets.Value = true;
                    if (newBullets > maxBullets.Value)
                        currentBullets.Value = maxBullets.Value;
                };
                _updateWrapper.onUpdate += deltaTime =>
                {
                    if (bulletRestoreTimer.Value < bulletRestoreCooldown.Value)
                        bulletRestoreTimer.Value += deltaTime;
                    else
                    {
                        currentBullets.Value++;
                        bulletRestoreTimer.Value = 0f;
                    }
                };
                shooter.onShootPerformed += () =>
                {
                    currentBullets.Value--;
                };
            }
        }
    }
}
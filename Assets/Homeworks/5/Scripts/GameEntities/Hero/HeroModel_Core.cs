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
        public LifeSection life = new();

        [Section]
        [SerializeField]
        public Shoot shoot = new();

        [Section]
        [SerializeField]
        public ShootReload shootReload = new();

        [Section]
        [SerializeField]
        public MoveSection move = new();
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
            move.onUpdated += deltaTime =>
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
            [HideInInspector] public AtomicVariable<float> cooldownTimer;
            [HideInInspector] public AtomicEvent onShoot;
            [HideInInspector] public AtomicEvent onShootPerformed;
            [HideInInspector] public AtomicEvent onKilled;
            private ShootEngine _shooter = new();

            [Construct]
            public void Init(LifeSection life, ShootReload shootReloader, HeroModel model)
            {
                model.onUpdate += deltaTime =>
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
                onKilled += () =>
                {
                    kills.Value++;
                };
                cooldownTimer.Value = cooldownTime.Value;
            }
        }

        [Serializable]
        public class ShootReload
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [SerializeField] public AtomicVariable<float> bulletRestoreCooldown;
            [HideInInspector] public AtomicVariable<float> bulletRestoreTimer;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> hasBullets;

            [Construct]
            public void Init(Shoot shooter, HeroModel model)
            {
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        hasBullets.Value = false;
                    else
                        hasBullets.Value = true;
                };
                model.onUpdate += deltaTime =>
                {
                    if (bulletRestoreTimer.Value < bulletRestoreCooldown.Value)
                        bulletRestoreTimer.Value += deltaTime;
                    else
                    {
                        if(currentBullets.Value < maxBullets.Value)
                            currentBullets.Value++;
                        bulletRestoreTimer.Value = 0f;
                    }
                };
                shooter.onShootPerformed += () =>
                {
                    currentBullets.Value--;
                };
                currentBullets.Value = maxBullets.Value;
                bulletRestoreTimer.Value = bulletRestoreCooldown.Value;
            }
        }
    }
}
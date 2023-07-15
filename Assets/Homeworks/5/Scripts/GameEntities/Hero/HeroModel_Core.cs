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
        public Life life = new();

        [Section] 
        [SerializeField] 
        public Shooter shooter = new();

        [Section] 
        [SerializeField] 
        public Mover mover = new();

        [Serializable]
        public class Life
        {
            [SerializeField] public AtomicVariable<int> health;
            [HideInInspector] public AtomicVariable<bool> isDead;
            [HideInInspector] public AtomicEvent<int> onTakeDamage;

            [Construct]
            public void Init()
            {
                health.OnChanged += newHealth =>
                {
                    if (newHealth <= 0 && !isDead.Value)
                        isDead.Value = true;
                };
                onTakeDamage += damage =>
                {
                    if (!isDead.Value)
                        health.Value -= damage;
                };
                isDead.OnChanged += dead =>
                {
                    if (dead)
                    {
                        Debug.Log("ИГРА ЗАВЕРШЕНА");
                        Time.timeScale = 0f;
                    }
                };
            }
        }

        [Serializable]
        public class Shooter
        {
            [SerializeField] private Transform _shootPoint;
            [SerializeField] private GameObject _bulletPrefab;
            [SerializeField] public AtomicVariable<int> maxBullets;
            [SerializeField] public AtomicVariable<float> bulletRestoreCooldown;
            [SerializeField] public AtomicVariable<float> cooldownTime;
            [HideInInspector] public AtomicVariable<int> kills;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> isReadyShoot;
            [HideInInspector] public AtomicVariable<bool> canShoot;
            [HideInInspector] public AtomicVariable<float> bulletRestoreTimer;
            [HideInInspector] public AtomicVariable<float> cooldownTimer;
            [HideInInspector] public AtomicEvent onShoot;
            private UpdateWrapper _updateWrapper = new();
            private ShootEngine _shooter = new();

            [Construct]
            public void Init(Life life)
            {
                currentBullets.Value = maxBullets.Value;
                bulletRestoreTimer.Value = bulletRestoreCooldown.Value;
                cooldownTimer.Value = cooldownTime.Value;
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        canShoot.Value = false;
                    else
                        canShoot.Value = true;
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
                    
                    if (cooldownTimer.Value < cooldownTime.Value)
                        cooldownTimer.Value += deltaTime;
                    else
                        isReadyShoot.Value = true;
                };
                onShoot += () =>
                {
                    if(canShoot.Value && isReadyShoot.Value && !life.isDead.Value)
                    {
                        isReadyShoot.Value = false;
                        cooldownTimer.Value = 0f;
                        currentBullets.Value--;
                        _shooter.Shoot(_bulletPrefab, _shootPoint, _shootPoint.forward);
                    }
                };
            }
        }

        [Serializable]
        public class Mover
        {
            [SerializeField] private Transform _transform;
            [SerializeField] public AtomicVariable<float> maxSpeed;
            [HideInInspector] public AtomicVariable<bool> moveRequired;
            [HideInInspector] public AtomicEvent<Vector2> onMove;
            private MoveEngine _moveEngine = new();
            private FixedUpdateWrapper _fixedUpdate = new();

            [Construct]
            public void Init()
            {
                onMove += dir =>
                {
                    _moveEngine.Cache(_transform, dir, maxSpeed.Value);
                    moveRequired.Value = true;
                };
                _fixedUpdate.onUpdate += deltaTime =>
                {
                    if (moveRequired.Value)
                    {
                        _moveEngine.Move(deltaTime);
                        moveRequired.Value = false;
                    }
                };
            }
        }
    }
}
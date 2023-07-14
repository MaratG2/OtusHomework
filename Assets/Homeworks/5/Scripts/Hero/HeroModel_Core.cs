using System;
using Atomic;
using Declarative;
using Homeworks5.Custom;
using Homeworks5.Custom.Wrappers;
using Sirenix.OdinInspector;
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
            [SerializeField] public AtomicVariable<bool> isDead;
            [SerializeField] public AtomicEvent<int> onTakeDamage;

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
            }
        }

        [Serializable]
        public class Shooter
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [SerializeField] public AtomicVariable<int> currentBullets;
            [SerializeField] public AtomicVariable<bool> canShoot;
            [SerializeField] public AtomicVariable<int> reloadCooldown;

            [Construct]
            public void Init()
            {
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        canShoot.Value = false;
                    if (newBullets > maxBullets.Value)
                        currentBullets.Value = maxBullets.Value;
                };
            }
        }

        [Serializable]
        public class Mover
        {
            [SerializeField]
            private MoveEngine _moveEngine = new();

            [SerializeField] 
            private FixedUpdateWrapper _fixedUpdate = new();
            
            [SerializeField] 
            private Transform _transform;

            [SerializeField] 
            public AtomicVariable<float> maxSpeed;

            [SerializeField] 
            public AtomicVariable<bool> moveRequired;

            [ShowInInspector] 
            public AtomicEvent<Vector2> onMove;

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
using System;
using Atomic;
using Declarative;
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
            public AtomicVariable<float> maxSpeed;

            [ShowInInspector] 
            public AtomicEvent<Vector3> onMove;
            
            [Construct]
            public void Init()
            {
                
            }
        }
    }
}
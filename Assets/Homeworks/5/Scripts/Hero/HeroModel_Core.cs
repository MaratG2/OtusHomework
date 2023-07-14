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
        [SerializeField]
        public AtomicVariable<int> health;
        [SerializeField]
        public AtomicVariable<bool> isDeath;
        [SerializeField]
        public AtomicVariable<int> maxBullets;
        [SerializeField]
        public AtomicVariable<int> currentBullets;
        [SerializeField]
        public AtomicVariable<bool> canShoot;
        [SerializeField]
        public AtomicVariable<float> maxSpeed;

        [ShowInInspector]
        public AtomicEvent onDeath;
        [ShowInInspector]
        public AtomicEvent<Vector3> onMove;

        [Construct]
        public void Init()
        {
            health.OnChanged += newHealth =>
            {
                if (newHealth <= 0 && !isDeath.Value)
                    isDeath.Value = true;
            };
            isDeath.OnChanged += isDead =>
            {
                if (isDead)
                    onDeath?.Invoke();
            };
            currentBullets.OnChanged += newBullets =>
            {
                if (newBullets <= 0)
                    canShoot.Value = false;
                if (newBullets > maxBullets.Value)
                    currentBullets.Value = maxBullets.Value;
            };
        }
    }
}
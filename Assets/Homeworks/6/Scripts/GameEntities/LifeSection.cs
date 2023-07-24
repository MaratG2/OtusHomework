using Atomic;
using Declarative;
using System;
using UnityEngine;

namespace Homeworks5
{
    [Serializable]
    public class LifeSection
    {
        [SerializeField] private Transform _transform;
        [SerializeField] public AtomicVariable<int> health;
        [HideInInspector] public AtomicVariable<bool> isDead;
        [HideInInspector] public AtomicEvent<int> onTakeDamage;
        [HideInInspector] public AtomicEvent onDeath;
        public Transform Transform => _transform;

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
                    onDeath?.Invoke();
            };
        }
    }
}

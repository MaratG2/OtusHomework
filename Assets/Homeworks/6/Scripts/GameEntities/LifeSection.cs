﻿using Atomic;
using Declarative;
using System;
using UnityEngine;

namespace Homeworks6
{
    [Serializable]
    public class LifeSection
    {
        public AtomicVariable<int> health;
        [HideInInspector] public AtomicVariable<bool> isDead;
        [HideInInspector] public AtomicEvent<int> onTakeDamage;
        [HideInInspector] public AtomicEvent onDeath;

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

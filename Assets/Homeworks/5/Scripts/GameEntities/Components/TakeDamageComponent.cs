using Atomic;
using System;
using UnityEngine;

namespace Homeworks5.Components
{
    public interface ITakeDamageComponent
    {
        void TakeDamage(int damage);
    }

    public class TakeDamageComponent : ITakeDamageComponent
    {
        public IAtomicAction<int> onTakeDamage;

        public TakeDamageComponent(IAtomicAction<int> onTakeDamage)
        {
            this.onTakeDamage = onTakeDamage;
        }

        void ITakeDamageComponent.TakeDamage(int damage)
        {
            onTakeDamage?.Invoke(damage);
        }
    }
}

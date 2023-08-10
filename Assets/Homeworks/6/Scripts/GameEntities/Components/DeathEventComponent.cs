using System;
using Atomic;

namespace Homeworks6.Components
{
    public class DeathEventComponent
    {
        public event Action OnDeath;

        public DeathEventComponent(AtomicEvent onDeathEvent)
        {
            onDeathEvent += () => OnDeath?.Invoke();
        }
    }
}
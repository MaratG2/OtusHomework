using Atomic;
using Declarative;
using Homeworks6.Hero;
using UnityEngine;

namespace Homeworks6.Zombie
{
    public class ZombieModel : DeclarativeModel
    {
        public Transform Target { get; private set; }
        
        [Section]
        [SerializeField]
        public ZombieModel_Core core = new();

        [Section]
        [SerializeField]
        public ZombieModel_View view = new();

        public AtomicEvent<HeroEntity> onConstruct;
        
        [Construct]
        public void Construct()
        {
            onConstruct += heroEntity =>
            {
                this.Target = heroEntity.transform;
            };
        }
    }
}
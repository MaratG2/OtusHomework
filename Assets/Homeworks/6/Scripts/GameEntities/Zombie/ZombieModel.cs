using Declarative;
using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Zombie
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
  
        public void Construct(HeroEntity heroEntity)
        {
            this.Target = heroEntity.transform;
            core.OuterInit(heroEntity);
        }
    }
}
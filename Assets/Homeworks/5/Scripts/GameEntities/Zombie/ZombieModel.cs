using Declarative;
using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Zombie
{
    public class ZombieModel : DeclarativeModel
    {
        private Transform _target;
        public Transform Target => _target;
        
        [Section]
        [SerializeField]
        public ZombieModel_Core core = new();

        [Section]
        [SerializeField]
        public ZombieModel_View view = new();
        
        [Inject]
        private void Construct(HeroModel heroModel)
        {
            _target = heroModel.transform;
        }
    }
}
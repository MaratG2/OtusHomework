using Declarative;
using Homeworks5.Hero;
using Homeworks5.Interfaces;
using UnityEngine;
using Zenject;

namespace Homeworks5.Zombie
{
    public class ZombieModel : DeclarativeModel, IDamageable
    {
        private Transform _target;
        public IScores scoresHolder;
        
        public Transform Target => _target;
        
        [Section]
        [SerializeField]
        public ZombieModel_Core core = new();

        [Section]
        [SerializeField]
        public ZombieModel_View view = new();
  
        public void Construct(HeroModel heroModel, IScores scoresHolder)
        {
            this._target = heroModel.transform;
            this.scoresHolder = scoresHolder;
        }

        public void TakeDamage(int damage)
        {
            core.life.onTakeDamage?.Invoke(damage);
        }
    }
}
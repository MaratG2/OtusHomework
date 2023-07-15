using Atomic;
using Declarative;
using Homeworks5.Interfaces;
using UnityEngine;

namespace Homeworks5.Hero
{
    public class HeroModel : DeclarativeModel, IPlayerDamageable, IScores
    {
        [Section] 
        [SerializeField] 
        public HeroModel_Core core = new();

        [Section] 
        [SerializeField] 
        public HeroModel_View view = new();

        public void TakeDamage(int damage)
        {
            core.life.onTakeDamage?.Invoke(damage);
        }

        public AtomicVariable<int> Kills => core.shooter.kills;
    }
}
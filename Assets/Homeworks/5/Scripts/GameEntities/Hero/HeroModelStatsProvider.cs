using Atomic;
using UnityEngine;
using Zenject;

namespace Homeworks5.Hero
{
    public class HeroModelStatsProvider
    {
        private HeroModel _heroModel;
        
        public AtomicVariable<int> HP => _heroModel.core.life.health;
        public AtomicVariable<int> CurrentBullets => _heroModel.core.shooter.currentBullets;
        public AtomicVariable<int> MaxBullets => _heroModel.core.shooter.maxBullets;
        public AtomicVariable<int> Kills => _heroModel.core.shooter.kills;

        [Inject]
        private void Construct(HeroModel heroModel)
        {
            this._heroModel = heroModel;
        }
    }
}
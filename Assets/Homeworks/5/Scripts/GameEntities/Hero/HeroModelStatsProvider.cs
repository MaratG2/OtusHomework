using Atomic;
using UnityEngine;
using Zenject;

namespace Homeworks5.Hero
{
    public class HeroModelStatsProvider
    {
        private HeroModel _heroModel;
        
        public AtomicVariable<int> HP => _heroModel.core.life.health;
        public AtomicVariable<int> CurrentBullets => _heroModel.core.shootReloader.currentBullets;
        public AtomicVariable<int> MaxBullets => _heroModel.core.shootReloader.maxBullets;
        public AtomicVariable<int> Kills => _heroModel.core.shooter.kills;

        [Inject]
        private void Construct(HeroModel heroModel)
        {
            this._heroModel = heroModel;
        }
    }
}
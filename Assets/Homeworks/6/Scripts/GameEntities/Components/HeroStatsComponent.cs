using Atomic;
using System;

namespace Homeworks5.Hero
{
    public interface IHeroStatsComponent
    {
        IAtomicVariable<int> HP();
        IAtomicVariable<int> CurrentBullets();
        IAtomicVariable<int> MaxBullets();
        IAtomicVariable<int> Kills();
    }

    public class HeroStatsComponent : IHeroStatsComponent
    {
        private IAtomicVariable<int> _hp;
        private IAtomicVariable<int> _currentBullets;
        private IAtomicVariable<int> _maxBullets;
        private IAtomicVariable<int> _kills;

        public HeroStatsComponent
            (
            IAtomicVariable<int> hp,
            IAtomicVariable<int> currentBullets,
            IAtomicVariable<int> maxBullets,
            IAtomicVariable<int> kills
            )
        {
            this._hp = hp;
            this._currentBullets = currentBullets; 
            this._maxBullets = maxBullets;
            this._kills = kills;
        }

        public IAtomicVariable<int> HP()
        {
            return _hp;
        }

        public IAtomicVariable<int> CurrentBullets()
        {
            return _currentBullets;
        }

        public IAtomicVariable<int> MaxBullets()
        {
            return _maxBullets;
        }

        public IAtomicVariable<int> Kills()
        {
            return _kills;
        }
    }
}

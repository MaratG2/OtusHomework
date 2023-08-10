using Atomic;

namespace Homeworks6.Hero
{
    public interface IHeroStatsComponent
    {
        IAtomicVariable<int> HP();
        IAtomicVariable<int> CurrentBullets();
        IAtomicVariable<int> MaxBullets();
    }

    public class HeroStatsComponent : IHeroStatsComponent
    {
        private IAtomicVariable<int> _hp;
        private IAtomicVariable<int> _currentBullets;
        private IAtomicVariable<int> _maxBullets;

        public HeroStatsComponent
            (
            IAtomicVariable<int> hp,
            IAtomicVariable<int> currentBullets,
            IAtomicVariable<int> maxBullets
            )
        {
            this._hp = hp;
            this._currentBullets = currentBullets; 
            this._maxBullets = maxBullets;
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
    }
}

using Atomic;

namespace Homeworks6.Components
{
    public interface IShootComponent
    {
        void Shoot();
    }

    public class ShootComponent : IShootComponent
    {
        public IAtomicAction onShoot;

        public ShootComponent(IAtomicAction onShoot)
        {
            this.onShoot = onShoot;
        }

        void IShootComponent.Shoot()
        {
            onShoot?.Invoke();
        }
    }
}
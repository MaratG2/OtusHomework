using ShootEmUp.Pool;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class BulletSystemInstaller : MonoInstaller<BulletSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PoolSettings>().FromComponentInChildren(true).AsSingle();
        }
    }
}
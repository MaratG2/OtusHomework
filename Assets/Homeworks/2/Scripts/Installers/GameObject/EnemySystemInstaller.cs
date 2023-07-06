using ShootEmUp.Pool;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class EnemySystemInstaller : MonoInstaller<EnemySystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PoolSettings>().FromComponentInChildren(true).AsSingle();
        }
    }
}
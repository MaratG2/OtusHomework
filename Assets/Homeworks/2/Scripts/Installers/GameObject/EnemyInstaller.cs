using ShootEmUp.Character;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<WeaponController>().FromComponentInChildren(true).AsSingle();
            Container.Bind<HitPointsComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<HitPointsControllerEnemy>().FromComponentInChildren(true).AsSingle();
        }
    }
}
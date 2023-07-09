using ShootEmUp.Enemies;
using ShootEmUp.Enemies.Agents;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<EnemyWeaponController>().FromComponentInChildren(true).AsSingle();
            Container.Bind<HitPointsComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<HitPointsControllerEnemy>().FromComponentInChildren(true).AsSingle();
            Container.Bind<MoveComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<EnemyMoveAgent>().FromComponentInChildren(true).AsSingle();
        }
    }
}
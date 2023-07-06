using ShootEmUp.Character;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<WeaponController>().FromComponentInChildren(true).AsSingle();
            Container.Bind<HitPointsComponent>().FromComponentInChildren(true).AsSingle();
            Container.Bind<HitPointsControllerPlayer>().FromComponentInChildren(true).AsSingle();
        }
    }
}
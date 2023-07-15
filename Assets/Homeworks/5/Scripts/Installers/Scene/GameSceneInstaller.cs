using Homeworks5.Hero;
using Homeworks5.Input;
using Zenject;

namespace Homeworks5.Installers.Scene
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<HeroModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerMovementInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLookInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerShootInput>().FromComponentInHierarchy().AsSingle();
        }
    }
}
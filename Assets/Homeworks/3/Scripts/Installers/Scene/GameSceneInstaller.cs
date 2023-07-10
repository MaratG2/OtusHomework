using Homework3.Database;
using Homework3.PM;
using Zenject;

namespace Homework3.Installers.Scene
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveLoad>().FromComponentsInHierarchy().AsCached();
            Container.Bind<UserPopup>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUserPresenter>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterPopup>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ICharacterPresenter>().FromComponentInHierarchy().AsSingle();
        }
    }
}
using Homework3.Database;
using Zenject;

namespace Homework3.Installers.Scene
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveLoad>().FromComponentsInHierarchy().AsCached();
        }
    }
}
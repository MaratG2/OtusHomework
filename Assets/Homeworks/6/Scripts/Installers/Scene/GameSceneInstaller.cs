using Homeworks6.Hero;
using Homeworks6.Input;
using Homeworks6.Models;
using Homeworks6.Spawner;
using Homeworks6.UI;
using Zenject;

namespace Homeworks6.Installers.Scene
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<HeroEntity>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerMovementInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLookInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IStatsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SpawnerPosition>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameObjectSpawner>().FromComponentInHierarchy().AsSingle();
            Container.Bind<KillsModel>().FromNew().AsSingle();
            Container.Bind<KillsObserver>().FromNew().AsSingle();
        }
    }
}
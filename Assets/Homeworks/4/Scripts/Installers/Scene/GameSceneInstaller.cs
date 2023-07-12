using Homeworks.SaveLoad;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ISaveLoader>().FromComponentsInHierarchy().AsCached();
        Container.Bind<SaveLoadManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameRepository>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameRepository>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerResources>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ResourceObject>().FromComponentsInHierarchy().AsCached();
        Container.Bind<UnitObject>().FromComponentsInHierarchy().AsCached();
    }
}

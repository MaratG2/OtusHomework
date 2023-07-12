using Homeworks.SaveLoad;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SaveLoadManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameRepository>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameRepository>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ISaveLoader>().FromComponentsInHierarchy().AsCached();
        Container.Bind<PlayerResources>().FromComponentInHierarchy().AsSingle();
    }
}

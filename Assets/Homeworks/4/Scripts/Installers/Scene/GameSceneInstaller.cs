using Homeworks.SaveLoad;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerResources>().FromComponentsInHierarchy().AsSingle();
    }
}

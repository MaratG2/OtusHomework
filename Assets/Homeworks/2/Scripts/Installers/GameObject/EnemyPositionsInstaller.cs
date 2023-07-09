using ShootEmUp.Level;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class EnemyPositionsInstaller : MonoInstaller<EnemyPositionsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PositionsContainer>().FromComponentsInChildren().AsCached();
        }
    }
}
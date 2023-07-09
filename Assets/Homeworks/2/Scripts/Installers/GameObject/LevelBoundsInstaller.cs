using UnityEngine;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class LevelBoundsInstaller : MonoInstaller<LevelBoundsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromComponentsInChildren().AsCached();
        }
    }
}
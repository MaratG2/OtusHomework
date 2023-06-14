using ShootEmUp.Inputs;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Installers.Scene
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private MoveComponent _playerMovement;
        public override void InstallBindings()
        {
            Container.Bind<MoveComponent>().FromInstance(_playerMovement).AsSingle();
            Container.Bind<PlayerMovementInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerShootInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle();
        }
    }
}
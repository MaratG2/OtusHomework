using ShootEmUp.Character;
using ShootEmUp.Enemies;
using ShootEmUp.GameManagement;
using ShootEmUp.Inputs;
using ShootEmUp.Level;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Installers.Scene
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private MoveComponent _playerMovement;
        [SerializeField] private WeaponController _playerWeaponController;
        public override void InstallBindings()
        {
            Container.Bind<MoveComponent>().FromInstance(_playerMovement).AsSingle();
            Container.Bind<WeaponController>().FromInstance(_playerWeaponController).AsSingle();
            Container.Bind<PlayerMovementInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerShootInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BulletSystem>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameListener>().FromComponentsInHierarchy().AsCached();
            Container.Bind<LevelBoundsController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemySystem>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemySetuper>().FromNew().AsSingle();
        }
    }
}
using ShootEmUp.Bullets;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Installers.GameObject
{
    public class BulletInstaller : MonoInstaller<BulletInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Rigidbody2D>().FromComponentInChildren(true).AsSingle();
            Container.Bind<SpriteRenderer>().FromComponentInChildren(true).AsSingle();
        }
    }
}
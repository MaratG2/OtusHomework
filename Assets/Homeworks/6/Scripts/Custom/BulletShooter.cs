using Homeworks5.Bullet;
using Homeworks5.Components;
using UnityEngine;

namespace Homeworks5.Custom
{
    public class BulletShooter
    {
        public void Shoot(GameObject prefab, Transform origin, Vector3 direction)
        {
            var newBullet = GameObject.Instantiate
                (prefab, origin.position, Quaternion.identity);
            
            var bulletEntity = newBullet.GetComponentInChildren<BulletEntity>();
            if (bulletEntity != null)
                if(bulletEntity.TryGet<IDirectionComponent>(out var directionComponent))
                    directionComponent.SetDirection(direction);
        }
    }
}
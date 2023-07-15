using Homeworks5.Bullet;
using UnityEngine;

namespace Homeworks5.Custom
{
    public class ShootEngine
    {
        public void Shoot(GameObject prefab, Transform origin, Vector3 direction)
        {
            var newBullet = GameObject.Instantiate
                (prefab, origin.position, Quaternion.identity);
            
            var model = newBullet.GetComponent<BulletModel>();
            if (model != null)
                model.core.mover.direction.Value = direction;
        }
    }
}
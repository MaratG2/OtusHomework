using Homeworks5.Interfaces;
using UnityEngine;

namespace Homeworks5.Custom
{
    public class ShootEngine
    {
        public void Shoot(GameObject prefab, Transform origin, Vector3 direction)
        {
            var newBullet = GameObject.Instantiate
                (prefab, origin.position, Quaternion.identity);
            
            var directionProvider = newBullet.GetComponentInChildren<IDirection>();
            if (directionProvider != null)
                directionProvider.Direction.Value = direction;
        }
    }
}
using System;
using Homeworks6.Bullet;
using Homeworks6.Components;
using UnityEngine;

namespace Homeworks6.Custom
{
    [Serializable]
    public class BulletFactory
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _origin;
        
        public void Create()
        {
            var newBullet = GameObject.Instantiate
                (_bulletPrefab, _origin.position, Quaternion.identity);
            
            var bulletEntity = newBullet.GetComponentInChildren<BulletEntity>();
            if (bulletEntity != null)
                if(bulletEntity.TryGet<IDirectionComponent>(out var directionComponent))
                    directionComponent.SetDirection(_origin.forward);
        }
    }
}
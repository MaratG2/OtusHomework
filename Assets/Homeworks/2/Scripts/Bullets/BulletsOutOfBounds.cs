using System.Linq;
using ShootEmUp.Level;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletsOutOfBounds : MonoBehaviour
    {
        private BulletSystem _bulletSystem;
        private LevelBoundsController _levelBoundsController;

        [Inject]
        private void Construct(BulletSystem bulletSystem, LevelBoundsController levelBoundsController)
        {
            this._bulletSystem = bulletSystem;
            this._levelBoundsController = levelBoundsController;
        }
        
        private void FixedUpdate()
        {
            CheckBulletsOutOfBounds();
        }
        
        private void CheckBulletsOutOfBounds()
        {
            var poolObjects = _bulletSystem.PoolFacade.GetAllActive();
            foreach (var poolObj in poolObjects.ToList())
            {
                if (!_levelBoundsController.IsInBounds(poolObj.transform.position))
                    _bulletSystem.PoolFacade.EnPool(poolObj);
            }
        }
    }
}
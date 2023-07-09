using ShootEmUp.Level;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        private PositionsContainer _spawnPositions;
        private PositionsContainer _attackPositions;

        [Inject]
        private void Construct(PositionsContainer[] positions)
        {
            this._spawnPositions = positions[0];
            this._attackPositions = positions[1];
        }
        
        public Transform RandomSpawnPosition()
        {
            return RandomTransform(_spawnPositions.Positions);
        }

        public Transform RandomAttackPosition()
        {
            return RandomTransform(_attackPositions.Positions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}
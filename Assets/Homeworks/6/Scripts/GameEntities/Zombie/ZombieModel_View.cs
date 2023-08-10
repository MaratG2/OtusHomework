using System;
using Declarative;
using UnityEngine;

namespace Homeworks6.Zombie
{
    [Serializable]
    public class ZombieModel_View
    {
        [SerializeField] private Transform _transform;
        
        [Construct]
        public void Init(ZombieModel_Core.EnemyAISection enemyAISection)
        {
            enemyAISection.targetDirection.OnChanged += dir =>
            {
                Vector3 dir3D = new Vector3(dir.x, 0f, dir.y);
                _transform.rotation = Quaternion.LookRotation(dir3D, Vector3.up);
            };
        }
    }
}
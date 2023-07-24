using System;
using Declarative;
using UnityEngine;

namespace Homeworks5.Zombie
{
    [Serializable]
    public class ZombieModel_View
    {
        [SerializeField] private Transform _transform;
        
        [Construct]
        public void Init(ZombieModel_Core.EnemyAI enemyAI)
        {
            enemyAI.targetDirection.OnChanged += dir =>
            {
                Vector3 dir3D = new Vector3(dir.x, 0f, dir.y);
                _transform.rotation = Quaternion.LookRotation(dir3D, Vector3.up);
            };
        }
    }
}
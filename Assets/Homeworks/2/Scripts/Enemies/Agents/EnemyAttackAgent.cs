using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        private EnemyWeaponController _weapon;
        private EnemyMoveAgent _moveAgent;
     
        private readonly float _cooldownTime = 1f;
        private float _cooldownTimer = 0f;

        [Inject]
        private void Construct(EnemyWeaponController weapon, EnemyMoveAgent moveAgent)
        {
            this._weapon = weapon;
            this._moveAgent = moveAgent;
        }

        public void Reset()
        {
            _cooldownTimer = _cooldownTime;
        }

        private void FixedUpdate()
        {
            if (!_moveAgent.IsReached)
                return;
            
            _cooldownTimer -= Time.fixedDeltaTime;
            if (_cooldownTimer <= 0)
            {
                _weapon.Fire();
                _cooldownTimer += _cooldownTime;
            }
        }
    }
}
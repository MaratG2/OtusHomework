using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        private MoveComponent _moveComponent;
        private Vector2 _destination;
        public bool IsReached { get; private set; }

        [Inject]
        private void Construct(MoveComponent moveComponent)
        {
            this._moveComponent = moveComponent;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        private void FixedUpdate()
        {
            if (IsReached)
                return;

            var path = _destination - (Vector2)transform.position;
            if (path.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            var direction = path.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}
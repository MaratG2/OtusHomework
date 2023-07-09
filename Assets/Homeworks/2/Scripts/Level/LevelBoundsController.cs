using UnityEngine;

namespace ShootEmUp.Level
{
    public sealed class LevelBoundsController : MonoBehaviour
    {
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _downBorder;
        [SerializeField] private Transform _topBorder;
        
        public bool IsInBounds(Vector3 position)
        {
            return position.x > _leftBorder.position.x
                   && position.x < _rightBorder.position.x
                   && position.y > _downBorder.position.y
                   && position.y < _topBorder.position.y;
        }
    }
}
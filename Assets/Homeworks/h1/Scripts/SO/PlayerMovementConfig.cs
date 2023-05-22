using UnityEngine;

namespace Homeworks.h1.SO
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "SO/PlayerMovementConfig")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _shiftSize;

        public float Speed => _speed;
        public float ShiftSize => _shiftSize;
    }
}
using UnityEngine;

namespace Homework7.Ecs
{
    [CreateAssetMenu(menuName = "ECS/Data/World", fileName = "WorldConfig")]
    public class WorldSO : ScriptableObject
    {
        [SerializeField]
        private float _borderX;
        [SerializeField]
        private float _borderY;
        [SerializeField]
        private float _gap;
        [SerializeField]
        private Vector2 _leftPoint;
        [SerializeField]
        private Vector2 _rightPoint;

        public float BorderX => _borderX;
        public float BorderY => _borderY;
        public float Gap => _gap;
        public Vector2 LeftPoint => _leftPoint;
        public Vector2 RightPoint => _rightPoint;
    }
}
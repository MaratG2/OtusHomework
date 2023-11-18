using Homework7.Enums;
using Homework7.Helpers;
using UnityEngine;

namespace Homework7.Ecs
{
    [CreateAssetMenu(menuName = "ECS/Data/Cube", fileName = "CubeConfig")]
    public class CubeSO : ScriptableObject
    {
        [SerializeField]
        private int _damage;
        [SerializeField]
        private IntRange _health;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _bulletSpeed;
        [SerializeField]
        private float _reloadTime;
        [SerializeField]
        private Color _color;
        [SerializeField]
        private Team _team;
        
        public int Damage => _damage;
        public IntRange Health => _health;
        public float MovementSpeed => _movementSpeed;
        public float BulletSpeed => _bulletSpeed;
        public float ReloadTime => _reloadTime;
        public Color Color => _color;
        public Team Team => _team;
    }
}
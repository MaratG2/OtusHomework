using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int _hitPoints;
        private int _initHitPoints = 0;
        public event Action OnDeath;
        public bool IsAlive => _hitPoints > 0;

        private void Awake()
        {
            _initHitPoints = _hitPoints;
        }

        public void TakeDamage(int damage)
        {
            bool wasAlive = _hitPoints > 0;
            bool hasSurvived = _hitPoints - damage > 0;
            _hitPoints -= damage;
            
            if (!hasSurvived && wasAlive)
                OnDeath?.Invoke();
        }

        public void Reset()
        {
            _hitPoints = _initHitPoints;
        }
    }
}
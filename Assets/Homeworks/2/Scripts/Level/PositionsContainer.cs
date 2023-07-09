using UnityEngine;

namespace ShootEmUp.Level
{
    public class PositionsContainer : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions;

        public Transform[] Positions => _positions;
    }
}
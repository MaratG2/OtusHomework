using System.Collections.Generic;
using UnityEngine;

namespace Homeworks6.Spawner
{
    public class SpawnerPosition : MonoBehaviour
    {
        [SerializeField] private List<Transform> _positions;
        public Vector3 GetRandomPosition()
        {
            int index = Random.Range(0, _positions.Count);
            return _positions[index].position;
        }
    }
}
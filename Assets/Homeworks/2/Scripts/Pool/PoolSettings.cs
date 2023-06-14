using UnityEngine;
using Zenject;

namespace ShootEmUp.Pool
{
    public class PoolSettings : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _length;
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public Transform Parent => _parent;
        public GameObject Prefab => _prefab;
        public int Length => _length;
        public DiContainer Container => _container;
    }
}
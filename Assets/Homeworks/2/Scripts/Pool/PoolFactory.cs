using UnityEngine;
using Zenject;

namespace ShootEmUp.Pool
{
    internal class PoolFactory<T> where T : MonoBehaviour
    {
        private readonly DiContainer _container;
        private readonly T _prefab;
        public PoolFactory(DiContainer container, T prefab)
        {
            this._container = container;
            this._prefab = prefab;
        }

        public T Create(Transform parent)
        {
            var newPrefab = _container.InstantiatePrefabForComponent<T>
                (_prefab, parent);
            SetGameObjectName(newPrefab.gameObject);
            return newPrefab;
        }

        private void SetGameObjectName(GameObject go) //7 symbols for (clone)
        {
            string nameWithoutClone = go.name.Remove(go.name.Length - 7);
            go.name = nameWithoutClone + go.transform.GetSiblingIndex();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Pool
{
    internal class PoolManager<T> where T : MonoBehaviour
    {
        private readonly int _length;
        private readonly Queue<T> _pool;
        private readonly HashSet<T> _activeObjects;

        public PoolManager(int length)
        {
            this._length = length;
            _pool = new Queue<T>(this._length);
            _activeObjects = new HashSet<T>(this._length);
        }

        public T Depool() //+active, -pool
        {
            if (_pool.TryDequeue(out T obj))
            {
                _activeObjects.Add(obj);
                return obj;
            }
            return null;
        }

        public void Enpool(T obj) //-active, +pool
        {
            if (_activeObjects.TryGetValue(obj, out T stored))
            {
                _pool.Enqueue(stored);
                _activeObjects.Remove(stored);
            }
        }
    }
}
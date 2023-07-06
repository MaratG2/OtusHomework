using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp.Pool
{
    internal class PoolManager<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _pool;
        private readonly HashSet<T> _activeObjects;

        public PoolManager()
        {
            _pool = new Queue<T>();
            _activeObjects = new HashSet<T>();
        }

        public void AddPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        public void AddActive(T obj)
        {
            obj.gameObject.SetActive(true);
            _activeObjects.Add(obj);
        }

        public T DePool() //+active, -pool
        {
            if (_pool.TryDequeue(out T obj))
            {
                obj.gameObject.SetActive(true);
                _activeObjects.Add(obj);
                return obj;
            }
            return null;
        }

        public void EnPool(T obj) //-active, +pool
        {
            if (_activeObjects.TryGetValue(obj, out T stored))
            {
                var toReset = obj.GetComponent<IResetReceiver>();
                if(toReset != null)
                    toReset.OnReset();
                
                stored.gameObject.SetActive(false);
                _pool.Enqueue(stored);
                _activeObjects.Remove(stored);
            }
        }

        public IEnumerable<T> GetAllPool()
        {
            return _pool.AsEnumerable();
        }

        public IEnumerable<T> GetAllActive()
        {
            return _activeObjects.AsEnumerable();
        }
    }
}
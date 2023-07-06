using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Pool
{
    public class PoolFacade<T> where T : MonoBehaviour
    {
        private readonly PoolManager<T> _poolManager;
        private readonly PoolFactory<T> _poolFactory;
        private readonly PoolSettings _poolSettings;
        
        public PoolFacade(PoolSettings poolSettings)
        {
            this._poolSettings = poolSettings;
            _poolManager = new PoolManager<T>(); 
            _poolFactory = new PoolFactory<T>
                (_poolSettings.Container, _poolSettings.Prefab.GetComponent<T>());
            InitializePool();
        }

        private void InitializePool()
        {
            for(int i = 0; i < _poolSettings.Length; i++)
                _poolManager.AddPool(_poolFactory.Create(_poolSettings.Parent));
        }

        ///<summary>
        /// +active, -pool
        ///</summary>  
        public T DePool()
        {
            return _poolManager.DePool();
        }

        ///<summary>
        /// -active, +pool
        ///</summary>  
        public void EnPool(T obj)
        {
            _poolManager.EnPool(obj);
        }

        public T AddActive()
        {
            var newObj = _poolFactory.Create(_poolSettings.Parent);
            _poolManager.AddActive(newObj);
            return newObj;
        }
        
        public IEnumerable<T> GetAllPool()
        {
            return _poolManager.GetAllPool();
        }

        public IEnumerable<T> GetAllActive()
        {
            return _poolManager.GetAllActive();
        }
    }
}
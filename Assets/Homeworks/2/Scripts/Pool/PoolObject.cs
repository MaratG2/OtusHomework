using UnityEngine;

namespace ShootEmUp.Pool
{
    public class PoolObject<T> where T : MonoBehaviour
    {
        private readonly PoolFacade<T> _poolFacade;

        public PoolObject(PoolFacade<T> poolFacade)
        {
            _poolFacade = poolFacade;
        }

        public void EnPool(T obj)
        {
            _poolFacade.EnPool(obj);
        }
    }
}
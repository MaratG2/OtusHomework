using System;
using Homeworks.h1.Tags;
using UnityEngine;

namespace Homeworks.h1
{
    public class PlayerCollisionComponent : MonoBehaviour
    {
        public Action OnObstacleHit;
        
        public void OnCollisionEnter(Collision collision)
        {
            var obstacleTag = collision.gameObject.GetComponent<ObstacleTag>();
            if (!obstacleTag)
                return;
            
            OnObstacleHit?.Invoke();
        }
    }
}
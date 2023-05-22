using System;
using Homeworks.h1.Tags;
using UnityEngine;

namespace Homeworks.h1
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        public Action OnPlayerHit;
        
        public void OnCollisionEnter(Collision collision)
        {
            var playerTag = collision.gameObject.GetComponent<PlayerTag>();
            if (!playerTag)
                return;
            
            OnPlayerHit?.Invoke();
        }
    }
}
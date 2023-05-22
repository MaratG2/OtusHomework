using UnityEngine;

namespace Homeworks.h1.GameManagement
{
    [RequireComponent(typeof(GameManager))]
    public class GameListenersLinker : MonoBehaviour
    {
        private void Awake()
        { 
            var gameManager = GetComponent<GameManager>();
            var listeners = GetComponentsInChildren<IGameListener>();
            foreach (var listener in listeners)
                gameManager.AddListener(listener);
        }
    }
}
using UnityEngine;

namespace ShootEmUp.GameManagement
{
    [RequireComponent(typeof(GameManager))]
    public class GameListenersInstaller : MonoBehaviour
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
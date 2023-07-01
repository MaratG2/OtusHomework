using UnityEngine;
using Zenject;

namespace ShootEmUp.GameManagement
{
    [RequireComponent(typeof(GameManager))]
    public class GameListenersInstaller : MonoBehaviour
    {
        private GameManager _gameManager;
        private IGameListener[] _gameListeners;

        [Inject]
        private void Construct(GameManager gameManager, IGameListener[] gameListeners)
        {
            this._gameManager = gameManager;
            this._gameListeners = gameListeners;
            Init();
        }
        
        private void Init()
        { 
            Debug.Log(_gameListeners.Length);
            foreach (var listener in _gameListeners)
                _gameManager.AddListener(listener);
        }
    }
}
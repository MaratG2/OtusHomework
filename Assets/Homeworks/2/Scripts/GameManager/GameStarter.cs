using UnityEngine;
using Zenject;

namespace ShootEmUp.GameManagement
{
    public class GameStarter : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        void Start()
        {
            _gameManager.StartGame();
        }
    }
}
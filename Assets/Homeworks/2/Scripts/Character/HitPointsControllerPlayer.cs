using ShootEmUp.GameManagement;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class HitPointsControllerPlayer : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGameResumeListener,
        IGamePauseListener
    {
        private GameManager _gameManager;
        private HitPointsComponent _hitPointsComponent;

        [Inject]
        private void Construct(GameManager gameManager, HitPointsComponent hitPointsComponent)
        {
            this._gameManager = gameManager;
            this._hitPointsComponent = hitPointsComponent;
        }
        
        public void OnGameStart()
        {
            _hitPointsComponent.OnDeath += _gameManager.EndGame;
        }

        public void OnGameEnd()
        {
            _hitPointsComponent.OnDeath -= _gameManager.EndGame;
        }

        public void OnGameResume()
        {
            _hitPointsComponent.OnDeath += _gameManager.EndGame;
        }

        public void OnGamePause()
        {
            _hitPointsComponent.OnDeath -= _gameManager.EndGame;
        }
    }
}
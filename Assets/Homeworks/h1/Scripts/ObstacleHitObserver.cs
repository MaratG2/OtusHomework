using Homeworks.h1.GameManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class ObstacleHitObserver : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField, Required] private PlayerCollisionComponent _playerCollision;
        [SerializeField, Required] private GameManager _gameManager;


        public void OnGameStart()
        {
            _playerCollision.OnObstacleHit += _gameManager.EndGame;
        }

        public void OnGameEnd()
        {
            _playerCollision.OnObstacleHit -= _gameManager.EndGame;
        }

        public void OnGamePause()
        {
            _playerCollision.OnObstacleHit -= _gameManager.EndGame;
        }

        public void OnGameResume()
        {
            _playerCollision.OnObstacleHit += _gameManager.EndGame;
        }
    }
}
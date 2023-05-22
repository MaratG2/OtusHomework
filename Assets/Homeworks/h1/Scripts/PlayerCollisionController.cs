using Homeworks.h1.GameManagement;
using UnityEngine;

namespace Homeworks.h1
{
    public class PlayerCollisionController : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField] private PlayerCollisionComponent _playerCollision;
        [SerializeField] private GameManager _gameManager;


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
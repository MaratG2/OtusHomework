using Homeworks.h1.GameManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class ObstaclesContainer : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField, Required] private GameManager _gameManager;
        private ObstacleBehaviour[] _obstacleBehaviours;

        private void Awake()
        {
            _obstacleBehaviours = GetComponentsInChildren<ObstacleBehaviour>();
        }

        public void OnGameStart()
        {
            foreach (var obstacle in _obstacleBehaviours)
                obstacle.OnPlayerHit += _gameManager.EndGame;
        }

        public void OnGameEnd()
        {
            foreach (var obstacle in _obstacleBehaviours)
                obstacle.OnPlayerHit -= _gameManager.EndGame;
        }
        
        public void OnGameResume()
        {
            foreach (var obstacle in _obstacleBehaviours)
                obstacle.OnPlayerHit += _gameManager.EndGame;
        }

        public void OnGamePause()
        {
            foreach (var obstacle in _obstacleBehaviours)
                obstacle.OnPlayerHit -= _gameManager.EndGame;
        }

    }
}
using System;
using Homeworks.h1.GameManager;
using Homeworks.h1.SO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class ObstaclesSpawner : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField, Required] private ObstaclesSpawnerDataSO _obstaclesSpawnerData;
        [SerializeField, Required] private ObstacleBehaviour _obstaclePrefab;
        [SerializeField, Required] private Transform _target;
        [SerializeField, Required] private Transform _obstaclesContainer;

        private float _timer = 0f;

        private void Awake()
        {
            this.enabled = false;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _obstaclesSpawnerData.TimeToSpawn)
            {
                _timer = 0f;
                SpawnObstacle();
            }
        }

        private void SpawnObstacle()
        {
            var newObstacle = Instantiate(_obstaclePrefab, GetSpawnPosition(), Quaternion.identity,
                _obstaclesContainer);
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 pos = _target.transform.position;
            pos += new Vector3(0f, 0f, _obstaclesSpawnerData.SpawnOffset);
            return pos;
        }

        public void OnGameStart()
        {
            this.enabled = true;
        }

        public void OnGameEnd()
        {
            this.enabled = false;
        }

        public void OnGamePause()
        {
            this.enabled = false;
        }

        public void OnGameResume()
        {
            this.enabled = true;
        }
    }
}
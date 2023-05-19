using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1.GameManager
{
    public enum GameState
    {
        OFF = 0,
        PLAY = 1,
        PAUSE = 2,
    }
    public class GameManager : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private GameState _state = GameState.OFF;
        private readonly List<IGameListener> _listeners = new ();

        public void AddListener(IGameListener listener)
        {
            if (listener == null)
                throw new ArgumentNullException();
            
            _listeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
                throw new ArgumentNullException();
            if (_listeners.Contains(listener) == false)
                throw new ArgumentOutOfRangeException();

            _listeners.Remove(listener);
        }
        [Button]
        public void StartGame()
        {
            if (_state != GameState.OFF)
                throw new Exception($"Invalid state change. Was: {_state}");
            
            foreach (var listener in _listeners)
                if (listener is IGameStartListener startListener)
                    startListener.OnGameStart();
            _state = GameState.PLAY;
        }
        [Button]
        public void EndGame()
        {
            if (_state != GameState.PLAY)
                throw new Exception($"Invalid state change. Was: {_state}");
            
            foreach (var listener in _listeners)
                if (listener is IGameEndListener endListener)
                    endListener.OnGameEnd();
            _state = GameState.OFF;
        }
        [Button]
        public void PauseGame()
        {
            if (_state != GameState.PLAY)
                throw new Exception($"Invalid state change. Was: {_state}");
            
            foreach (var listener in _listeners)
                if (listener is IGamePauseListener pauseListener)
                    pauseListener.OnGamePause();
            _state = GameState.PAUSE;
        }
        [Button]
        public void ResumeGame()
        {
            if (_state != GameState.PAUSE)
                throw new Exception($"Invalid state change. Was: {_state}");
            
            foreach (var listener in _listeners)
                if (listener is IGameResumeListener resumeListener)
                    resumeListener.OnGameResume();
            _state = GameState.PLAY;
        }
    }
}
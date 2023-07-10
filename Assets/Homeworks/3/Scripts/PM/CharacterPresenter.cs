using System;
using Homework3.Database;
using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;

namespace Homework3.PM
{
    public class CharacterPresenter : MonoBehaviour, ICharacterPresenter
    {
        [SerializeField] private Sprite _expBarCompleted, _expBarUncompleted;
        [SerializeField] private Sprite _lvlUpButtonActive, _lvlUpButtonInactive;
        public event Action OnLvlChanged;
        public event Action OnExpChanged;
        
        private PlayerLevelWrapper _playerLevelWrapper;
        private PlayerLevel _playerLevel => _playerLevelWrapper.PlayerLevel;

        [Inject]
        private void Construct(ISaveLoad[] wrappers)
        {
            foreach (var wrapper in wrappers)
                if (wrapper is PlayerLevelWrapper playerInfoWraper)
                    this._playerLevelWrapper = playerInfoWraper;
        }

        public void Start()
        {
            _playerLevel.OnExperienceChanged += ExpChanged;
            _playerLevel.OnLevelUp += LvlChanged;
        }

        public void Stop()
        {
            _playerLevel.OnExperienceChanged -= ExpChanged;
            _playerLevel.OnLevelUp -= LvlChanged;
        }
        
        public string GetLevel()
        {
            return $"Level: {_playerLevel.CurrentLevel}";
        }

        public bool CanLvlUp()
        {
            return GetProgressBarFill().Equals(1f);
        }

        public string GetExperience()
        {
            return $"XP: {_playerLevel.CurrentExperience} / {_playerLevel.RequiredExperience}";
        }

        public Sprite GetProgressBarSprite()
        {
            if (CanLvlUp())
                return _expBarCompleted;
            return _expBarUncompleted;
        }

        public Sprite GetLvlupButtonSprite()
        {
            if (CanLvlUp())
                return _lvlUpButtonActive;
            return _lvlUpButtonInactive;
        }

        public float GetProgressBarFill()
        {
            return _playerLevel.CurrentExperience / (float)_playerLevel.RequiredExperience;
        }

        private void ExpChanged(int newExp)
        {
            OnExpChanged?.Invoke();
        }
        
        private void LvlChanged()
        {
            OnLvlChanged?.Invoke();
        }
    }
}
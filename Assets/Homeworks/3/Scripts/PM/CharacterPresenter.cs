using System;
using Homework3.Database;
using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

namespace Homework3.PM
{
    public class CharacterPresenter : MonoBehaviour, ICharacterPresenter
    {
        [SerializeField] private Sprite _expBarCompleted, _expBarUncompleted;
        [SerializeField] private Sprite _lvlUpButtonActive, _lvlUpButtonInactive;
        public event Action OnLvlChanged;
        public event Action OnExpChanged;
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        private PlayerLevelWrapper _playerLevelWrapper;
        private CharacterInfoWrapper _characterInfoWrapper;
        private PlayerLevel _playerLevel => _playerLevelWrapper.PlayerLevel;
        private CharacterInfo _characterInfo => _characterInfoWrapper.CharacterInfo;

        [Inject]
        private void Construct(ISaveLoad[] wrappers)
        {
            foreach (var wrapper in wrappers)
            {
                if (wrapper is PlayerLevelWrapper playerInfoWraper)
                    this._playerLevelWrapper = playerInfoWraper;
                if (wrapper is CharacterInfoWrapper characterInfoWrapper)
                    this._characterInfoWrapper = characterInfoWrapper;
            }
        }

        public void Begin()
        {
            _playerLevel.OnExperienceChanged += ExpChanged;
            _playerLevel.OnLevelUp += LvlChanged;
            _characterInfo.OnStatAdded += OnStatAdded;
            _characterInfo.OnStatRemoved += OnStatRemoved;
        }

        public void Stop()
        {
            _playerLevel.OnExperienceChanged -= ExpChanged;
            _playerLevel.OnLevelUp -= LvlChanged;
            _characterInfo.OnStatAdded -= OnStatAdded;
            _characterInfo.OnStatRemoved -= OnStatRemoved;
        }
        
        public string GetLevel()
        {
            return $"Level: {_playerLevel.CurrentLevel}";
        }

        public bool CanLvlUp()
        {
            return _playerLevel.CanLevelUp();
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

        public CharacterStat[] GetAllStats()
        {
            return _characterInfo.GetStats();
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
        
        public void OnLvlupClicked()
        {
            if(CanLvlUp())
                _playerLevel.LevelUp();
        }
    }
}
using System;
using Lessons.Architecture.PM;
using UnityEngine;

namespace Homework3.PM
{
    public interface ICharacterPresenter
    {
        event Action OnLvlChanged;
        event Action OnExpChanged;
        event Action<CharacterStat> OnStatAdded;
        event Action<CharacterStat> OnStatRemoved;
        string GetLevel();
        bool CanLvlUp();
        string GetExperience();
        Sprite GetProgressBarSprite();
        Sprite GetLvlupButtonSprite();
        CharacterStat[] GetAllStats();
        float GetProgressBarFill();
        void OnLvlupClicked();
        void Begin();
        void Stop();
    }
}
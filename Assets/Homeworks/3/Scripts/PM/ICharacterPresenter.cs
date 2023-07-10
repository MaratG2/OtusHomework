using System;
using UnityEngine;

namespace Homework3.PM
{
    public interface ICharacterPresenter
    {
        event Action OnLvlChanged;
        event Action OnExpChanged;
        string GetLevel();
        bool CanLvlUp();
        string GetExperience();
        Sprite GetProgressBarSprite();
        Sprite GetLvlupButtonSprite();
        float GetProgressBarFill();
        void Start();
        void Stop();
    }
}
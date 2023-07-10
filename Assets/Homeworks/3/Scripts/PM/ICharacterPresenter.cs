using System;
using UnityEngine;

namespace Homework3.PM
{
    public interface ICharacterPresenter
    {
        event Action OnLvlChanged;
        event Action OnExpChanged;
        string GetLevel();
        string GetExperience();
        Sprite GetProgressBarSprite();
        float GetProgressBarFill();
        void Start();
        void Stop();
    }
}
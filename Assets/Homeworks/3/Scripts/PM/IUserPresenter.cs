using System;
using UnityEngine;

namespace Homework3.PM
{
    public interface IUserPresenter
    {
        event Action OnNameChanged;
        event Action OnDescChanged;
        event Action OnIconChanged;
        string GetName();
        string GetDescription();
        Sprite GetIcon();
        void Begin();
        void Stop();
    }
}
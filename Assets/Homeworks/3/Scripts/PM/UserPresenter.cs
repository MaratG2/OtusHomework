using System;
using Homework3.Database;
using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;

namespace Homework3.PM
{
    public class UserPresenter : MonoBehaviour, IUserPresenter
    {
        public event Action OnNameChanged;
        public event Action OnDescChanged;
        public event Action OnIconChanged;
        
        private UserInfoWrapper _userInfoWrapper;
        private UserInfo _userInfo => _userInfoWrapper.UserInfo; 

        [Inject]
        private void Construct(ISaveLoad[] wrappers)
        {
            foreach (var wrapper in wrappers)
                if (wrapper is UserInfoWrapper userInfoWraper)
                    this._userInfoWrapper = userInfoWraper;
        }
        
        public void Start()
        {
            _userInfo.OnNameChanged += NameChanged;
            _userInfo.OnDescriptionChanged += DescChanged;
            _userInfo.OnIconChanged += IconChanged;
        }
        
        public void Stop()
        {
            _userInfo.OnNameChanged -= NameChanged;
            _userInfo.OnDescriptionChanged -= DescChanged;
            _userInfo.OnIconChanged -= IconChanged;
        }

        private void NameChanged(string newName)
        {
            OnNameChanged?.Invoke();
        }
        private void DescChanged(string newDesc)
        {
            OnDescChanged?.Invoke();
        }
        private void IconChanged(Sprite newIcon)
        {
            OnIconChanged?.Invoke();
        }

        public string GetName()
        {
            return _userInfo.Name;
        }

        public string GetDescription()
        {
            return _userInfo.Description;
        }

        public Sprite GetIcon()
        {
            return _userInfo.Icon;
        }
    }
}
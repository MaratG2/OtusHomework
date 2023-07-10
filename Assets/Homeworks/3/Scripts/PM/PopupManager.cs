using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework3.PM
{
    public class PopupManager : MonoBehaviour
    {
        private UserPopup _userPopup;
        private CharacterPopup _characterPopup;
        private IUserPresenter _userPresenter;
        private ICharacterPresenter _characterPresenter;

        [Inject]
        private void Construct(UserPopup userPopup, CharacterPopup characterPopup, 
            IUserPresenter userPresenter, ICharacterPresenter characterPresenter)
        {
            this._userPopup = userPopup;
            this._characterPopup = characterPopup;
            this._userPresenter = userPresenter;
            this._characterPresenter = characterPresenter;
        }

        private void Start()
        {
            Show();
        }

        [Button]
        public void Show()
        {
            _userPopup.Show(_userPresenter);
            _characterPopup.Show(_characterPresenter);
        }

        [Button]
        public void Hide()
        {
            _userPopup.Hide();
            _characterPopup.Hide();
        }
    }
}
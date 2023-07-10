using UnityEngine;
using Zenject;

namespace Homework3.PM
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _popupGroup;
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

        public void Show()
        {
            _popupGroup.alpha = 1f;
            _popupGroup.interactable = true;
            _popupGroup.blocksRaycasts = true;
            _userPopup.Show(_userPresenter);
            _characterPopup.Show(_characterPresenter);
        }

        public void Hide()
        {
            _popupGroup.alpha = 0f;
            _popupGroup.interactable = false;
            _popupGroup.blocksRaycasts = false;
            _userPopup.Hide();
            _characterPopup.Hide();
        }
    }
}
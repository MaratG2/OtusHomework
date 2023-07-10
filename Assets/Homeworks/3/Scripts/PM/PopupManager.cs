using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework3.PM
{
    public class PopupManager : MonoBehaviour
    {
        private UserPopup _userPopup;
        private IUserPresenter _userPresenter;

        [Inject]
        private void Construct(UserPopup userPopup, IUserPresenter userPresenter)
        {
            this._userPopup = userPopup;
            this._userPresenter = userPresenter;
        }

        private void Start()
        {
            Show();
        }

        [Button]
        public void Show()
        {
            _userPopup.Show(_userPresenter);
        }

        [Button]
        public void Hide()
        {
            _userPopup.Hide();
        }
    }
}
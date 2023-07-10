using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework3.PM
{
    public class UserPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private Image _iconImage;
        
        private IUserPresenter _userPresenter;

        public void Show(IUserPresenter userPresenter)
        {
            this._userPresenter = userPresenter;
            
            _nameText.text = _userPresenter.GetName();
            _descText.text = _userPresenter.GetDescription();
            _iconImage.sprite = _userPresenter.GetIcon();

            _userPresenter.OnNameChanged += NameChanged;
            _userPresenter.OnDescChanged += DescChanged;
            _userPresenter.OnIconChanged += IconChanged;
            
            _userPresenter.Start();
        }

        public void Hide()
        {
            _userPresenter.OnNameChanged -= NameChanged;
            _userPresenter.OnDescChanged -= DescChanged;
            _userPresenter.OnIconChanged -= IconChanged;
            
            _userPresenter.Stop();
            this._userPresenter = null;
        }

        private void NameChanged()
        {
            _nameText.text = _userPresenter.GetName();
        }
        
        private void DescChanged()
        {
            _descText.text = _userPresenter.GetDescription();
        }
        
        private void IconChanged()
        {
            _iconImage.sprite = _userPresenter.GetIcon();
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework3.PM
{
    public class CharacterPopup : MonoBehaviour
    {
        //Stat factory?
        //Maybe stats popup
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Image _expBar;
        [SerializeField] private Button _lvlupButton;
        
        private ICharacterPresenter _characterPresenter;
        
        public void Show(ICharacterPresenter characterPresenter)
        {
            this._characterPresenter = characterPresenter;
            
            _levelText.text = _characterPresenter.GetLevel();
            _expText.text = _characterPresenter.GetExperience();
            _expBar.sprite = _characterPresenter.GetProgressBarSprite();
            _expBar.fillAmount = _characterPresenter.GetProgressBarFill();
            _lvlupButton.interactable = _characterPresenter.CanLvlUp();
            _lvlupButton.image.sprite = _characterPresenter.GetLvlupButtonSprite();
            _lvlupButton.onClick.AddListener(_characterPresenter.OnLvlupClicked);
            
            _characterPresenter.OnLvlChanged += LvlChanged;
            _characterPresenter.OnExpChanged += ExpChanged;
            
            _characterPresenter.Start();
        }

        public void Hide()
        {
            _characterPresenter.OnLvlChanged -= LvlChanged;
            _characterPresenter.OnExpChanged -= ExpChanged;
            _lvlupButton.onClick.RemoveListener(_characterPresenter.OnLvlupClicked);
            
            _characterPresenter.Stop();
            this._characterPresenter = null;
        }

        private void LvlChanged()
        {
            _levelText.text = _characterPresenter.GetLevel();
            ExpChanged();
        }
        
        private void ExpChanged()
        {
            _expText.text = _characterPresenter.GetExperience();
            _expBar.sprite = _characterPresenter.GetProgressBarSprite();
            _expBar.fillAmount = _characterPresenter.GetProgressBarFill();
            _lvlupButton.interactable = _characterPresenter.CanLvlUp();
            _lvlupButton.image.sprite = _characterPresenter.GetLvlupButtonSprite();
        }
    }
}
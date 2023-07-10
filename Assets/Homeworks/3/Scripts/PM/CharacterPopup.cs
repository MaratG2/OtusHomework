using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework3.PM
{
    public class CharacterPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Image _expBar;
        [SerializeField] private Button _lvlupButton;
        [SerializeField] private Transform _statsParent;
        [SerializeField] private CharacterStatObserver _characterStatPrefab;
        
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
            foreach (var stat in _characterPresenter.GetAllStats())
                StatAdded(stat);

            _characterPresenter.OnLvlChanged += LvlChanged;
            _characterPresenter.OnExpChanged += ExpChanged;
            _characterPresenter.OnStatAdded += StatAdded;
            _characterPresenter.OnStatRemoved += StatRemoved;
            
            _characterPresenter.Begin();
        }

        public void Hide()
        {
            _lvlupButton.onClick.RemoveListener(_characterPresenter.OnLvlupClicked);
            
            _characterPresenter.OnLvlChanged -= LvlChanged;
            _characterPresenter.OnExpChanged -= ExpChanged;
            _characterPresenter.OnStatAdded -= StatAdded;
            _characterPresenter.OnStatRemoved -= StatRemoved;
            foreach (var stat in _characterPresenter.GetAllStats())
                StatRemoved(stat);
            
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

        private void StatAdded(CharacterStat stat)
        {
            var newStat = Instantiate(_characterStatPrefab, Vector3.zero, Quaternion.identity, _statsParent);
            newStat.Init(stat);
            newStat.transform.localPosition = Vector3.zero;
        }
        
        private void StatRemoved(CharacterStat stat)
        {
            for (int i = 0; i < _statsParent.childCount; i++)
            {
                if (_statsParent.GetChild(i).GetComponent<CharacterStatObserver>().Stat == stat)
                {
                    Destroy(_statsParent.GetChild(i).gameObject);
                    return;
                }
            }
        }
    }
}
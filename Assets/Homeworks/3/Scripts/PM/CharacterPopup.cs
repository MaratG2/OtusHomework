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
        
        private CharacterPresenter _characterPresenter;
    }
}
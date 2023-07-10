using TMPro;
using UnityEngine;

namespace Homework3.PM
{
    public class CharacterStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statText;
  
        public void RefreshText(string text)
        {
            _statText.text = text;
        }
    }
}
using Lessons.Architecture.PM;
using UnityEngine;

namespace Homework3.PM
{
    public class CharacterStatObserver : MonoBehaviour
    {
        private CharacterStatView _statView;
        private CharacterStatModel _statModel;
        public CharacterStat Stat => _statModel.Stat;

        private void Awake()
        {
            _statView = GetComponent<CharacterStatView>();
            _statModel = GetComponent<CharacterStatModel>();
        }

        public void Init(CharacterStat stat)
        {
            _statModel.Init(stat);
            RefreshText(stat.Value);
            _statModel.Stat.OnValueChanged += RefreshText;
        }
        
        private void OnDestroy()
        {
            _statModel.Stat.OnValueChanged -= RefreshText;
        }

        private void RefreshText(int value)
        {
            string formattedText = $"{_statModel.Stat.Name}: {value}";
            _statView.RefreshText(formattedText);
        }
    }
}
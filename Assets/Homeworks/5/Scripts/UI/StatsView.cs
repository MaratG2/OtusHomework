using TMPro;
using UnityEngine;

namespace Homeworks5.UI
{
    public class StatsView : MonoBehaviour, IStatsView
    {
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _bulletsText;
        [SerializeField] private TMP_Text _killsText;

        public void SetHPText(string hpText)
        {
            _hpText.text = hpText;
        }

        public void SetBulletsText(string bulletsText)
        {
            _bulletsText.text = bulletsText;
        }

        public void SetKillsText(string killsText)
        {
            _killsText.text = killsText;
        }
    }
}
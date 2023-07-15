using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.UI
{
    public class StatsPresenter : MonoBehaviour
    {
        private IStatsView _statsView;
        private HeroModelStatsProvider _statsProvider;

        [Inject]
        private void Construct(IStatsView statsView, HeroModelStatsProvider statsProvider)
        {
            this._statsView = statsView;
            this._statsProvider = statsProvider;
            Init();
            InitDraw();
        }

        private void Init()
        {
            _statsProvider.HP.OnChanged += hp =>
            {
                _statsView.SetHPText($"HP: {hp}");
            };
            _statsProvider.CurrentBullets.OnChanged += bullets =>
            {
                _statsView.SetBulletsText($"Bullets: {bullets}/" +
                                          $"{_statsProvider.MaxBullets.Value}");
            };
            _statsProvider.Kills.OnChanged += kills =>
            {
                _statsView.SetKillsText($"Kills: {kills}");
            };
        }

        private void InitDraw()
        {
            _statsView.SetHPText($"HP: {_statsProvider.HP.Value}");
            _statsView.SetBulletsText($"Bullets: {_statsProvider.CurrentBullets.Value}/" +
                                      $"{_statsProvider.MaxBullets.Value}");
            _statsView.SetKillsText($"Kills: {_statsProvider.Kills.Value}");
        }
    }
}
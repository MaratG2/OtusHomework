using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.UI
{
    public class StatsPresenter : MonoBehaviour
    {
        private IStatsView _statsView;
        private HeroModel _heroModel;

        [Inject]
        private void Construct(IStatsView statsView, HeroModel heroModel)
        {
            this._statsView = statsView;
            this._heroModel = heroModel;
            Init();
            InitDraw();
        }

        private void Init()
        {
            _heroModel.core.life.health.OnChanged += hp =>
            {
                _statsView.SetHPText($"HP: {hp}");
            };
            _heroModel.core.shooter.currentBullets.OnChanged += bullets =>
            {
                _statsView.SetBulletsText($"Bullets: {bullets}/" +
                                          $"{_heroModel.core.shooter.maxBullets.Value}");
            };
            _heroModel.core.shooter.kills.OnChanged += kills =>
            {
                _statsView.SetKillsText($"Kills: {kills}");
            };
        }

        private void InitDraw()
        {
            _statsView.SetHPText($"HP: {_heroModel.core.life.health.Value}");
            _statsView.SetBulletsText($"Bullets: {_heroModel.core.shooter.currentBullets.Value}/" +
                                      $"{_heroModel.core.shooter.maxBullets.Value}");
            _statsView.SetKillsText($"Kills: {_heroModel.core.shooter.kills.Value}");
        }
    }
}
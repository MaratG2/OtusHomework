using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.UI
{
    public class StatsPresenter : MonoBehaviour
    {
        private IStatsView _statsView;
        private HeroEntity _heroEntity;
        private IHeroStatsComponent _heroStatsComponent;

        [Inject]
        private void Construct(IStatsView statsView, HeroEntity heroEntity)
        {
            this._statsView = statsView;
            this._heroEntity = heroEntity;
        }

        private void Start()
        {
            this._heroStatsComponent = _heroEntity.Get<IHeroStatsComponent>();
            Init();
            InitDraw();
        }

        private void Init()
        {
            _heroStatsComponent.HP().OnChanged += hp =>
            {
                _statsView.SetHPText($"HP: {hp}");
            };
            _heroStatsComponent.CurrentBullets().OnChanged += bullets =>
            {
                _statsView.SetBulletsText($"Bullets: {bullets}/" +
                                          $"{_heroStatsComponent.MaxBullets().Value}");
            };
            _heroStatsComponent.Kills().OnChanged += kills =>
            {
                _statsView.SetKillsText($"Kills: {kills}");
            };
        }

        private void InitDraw()
        {
            _statsView.SetHPText($"HP: {_heroStatsComponent.HP().Value}");
            _statsView.SetBulletsText($"Bullets: {_heroStatsComponent.CurrentBullets().Value}/" +
                                      $"{_heroStatsComponent.MaxBullets().Value}");
            _statsView.SetKillsText($"Kills: {_heroStatsComponent.Kills().Value}");
        }
    }
}
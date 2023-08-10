using Homeworks6.Hero;
using Homeworks6.Models;
using UnityEngine;
using Zenject;

namespace Homeworks6.UI
{
    public class StatsPresenter : MonoBehaviour
    {
        private IStatsView _statsView;
        private HeroEntity _heroEntity;
        private IHeroStatsComponent _heroStatsComponent;
        private KillsObserver _killsObserver;

        [Inject]
        private void Construct(IStatsView statsView, HeroEntity heroEntity, KillsObserver killsObserver)
        {
            _statsView = statsView;
            _heroEntity = heroEntity;
            _killsObserver = killsObserver;
        }

        private void Start()
        {
            _heroStatsComponent = _heroEntity.Get<IHeroStatsComponent>();
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
            _killsObserver.OnKillsChanged += () =>
            {
                _statsView.SetKillsText($"Kills: {_killsObserver.GetKills()}");
            };
        }

        private void InitDraw()
        {
            _statsView.SetHPText($"HP: {_heroStatsComponent.HP().Value}");
            _statsView.SetBulletsText($"Bullets: {_heroStatsComponent.CurrentBullets().Value}/" +
                                      $"{_heroStatsComponent.MaxBullets().Value}");
            _statsView.SetKillsText($"Kills: {_killsObserver.GetKills()}");
        }
    }
}
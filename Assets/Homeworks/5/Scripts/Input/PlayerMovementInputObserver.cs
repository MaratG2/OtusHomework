using Homeworks5.Components;
using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Input
{
    public class PlayerMovementInputObserver : MonoBehaviour
    {
        private PlayerMovementInput _playerMovementInput;
        private HeroEntity _heroEntity;

        [Inject]
        private void Construct(PlayerMovementInput input, HeroEntity heroEntity)
        {
            this._playerMovementInput = input;
            this._heroEntity = heroEntity;
        }
        
        private void OnEnable()
        {
            _playerMovementInput.onMove += Move;
        }
        
        private void OnDisable()
        {
            _playerMovementInput.onMove -= Move;
        }

        private void Move(Vector2 direciton)
        {
            _heroEntity.Get<IMoveComponent>().Move(direciton);
        }
    }
}
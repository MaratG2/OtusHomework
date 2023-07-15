using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Input
{
    public class PlayerMovementInputObserver : MonoBehaviour
    {
        private PlayerMovementInput _playerMovementInput;
        private HeroModel _heroModel;

        [Inject]
        private void Construct(PlayerMovementInput input, HeroModel heroModel)
        {
            this._playerMovementInput = input;
            this._heroModel = heroModel;
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
            _heroModel.core.mover.onMove?.Invoke(direciton);
        }
    }
}
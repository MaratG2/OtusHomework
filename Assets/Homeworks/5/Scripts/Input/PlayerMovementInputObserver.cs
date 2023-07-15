using Homeworks5.Hero;
using UnityEngine;

namespace Homeworks5.Input
{
    public class PlayerMovementInputObserver : MonoBehaviour
    {
        //TODO: DI
        [SerializeField] private PlayerMovementInput _playerMovementInput;
        [SerializeField] private HeroModel _heroModel;

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
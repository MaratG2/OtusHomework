using Homeworks5.Hero;
using UnityEngine;

namespace Homeworks5.Input
{
    public class PlayerInputObserver : MonoBehaviour
    {
        //TODO: DI
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private HeroModel _heroModel;

        private void OnEnable()
        {
            _playerInput.onMove += Move;
        }
        
        private void OnDisable()
        {
            _playerInput.onMove -= Move;
        }

        private void Move(Vector2 direciton)
        {
            _heroModel.core.mover.onMove?.Invoke(direciton);
        }
    }
}
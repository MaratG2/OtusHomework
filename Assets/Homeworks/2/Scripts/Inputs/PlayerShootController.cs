using UnityEngine;
using Zenject;

namespace ShootEmUp.Inputs
{
    public class PlayerShootController : MonoBehaviour
    {
        private PlayerShootInput _input;
        private CharacterController _character;
        
        [Inject]
        private void Construct(PlayerShootInput input, CharacterController character)
        {
            this._input = input;
            this._character = character;
        }
        
        private void OnEnable()
        {
            _input.OnShoot += PrepareAndShoot;
        }

        private void OnDisable()
        {
            _input.OnShoot -= PrepareAndShoot;
        }

        private void PrepareAndShoot()
        {
            _character._fireRequired = true;
        }
    }
}
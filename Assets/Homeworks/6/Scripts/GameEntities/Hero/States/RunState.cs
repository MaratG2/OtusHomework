using System;
using Atomic;
using Declarative;

namespace Homeworks6.Hero.States
{
    [Serializable]
    public class RunState : IState
    {
        private MoveSection _move;
        
        [Construct]
        public void Construct(MoveSection move)
        {
            _move = move;
        }
        
        void IState.Enter()
        {
            _move.IsEnabled.Value = true;
        }

        void IState.Exit()
        {
            _move.IsEnabled.Value = false;
        }
    }
}
using System.Collections.Generic;

namespace Atomic
{
    public class CompositeState : IState
    {
        private List<IState> _states = new List<IState>();
        
        void IState.Enter()
        {
            foreach (var state in _states)
                state.Enter();
        }

        void IState.Exit()
        {
            foreach (var state in _states)
                state.Exit();
        }

        protected void SetStates(params IState[] states)
        {
            _states = new List<IState>(states);
        }
    }
}
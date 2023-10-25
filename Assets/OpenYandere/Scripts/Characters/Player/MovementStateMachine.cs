using UnityEngine;
using System.Collections.Generic;
using OpenYandere.Characters.Player.States.Traits;

namespace OpenYandere.Characters.Player
{
    public enum MovementState
    {
        None,
        Standing,
        Walking,
        Running,
        Crouching
    }
    
    public class MovementStateMachine
    {
        private readonly Dictionary<MovementState, IState> _registeredStates = new Dictionary<MovementState, IState>();
        private IState _currentState;

        public void RegisterState(MovementState stateName, IState state)
        {
            _registeredStates.Add(stateName, state);
        }

        public void EnterState(MovementState stateName)
        {
            // If the state is not registered, return.
            if (!_registeredStates.ContainsKey(stateName)) return;
            
            // Update the current state and call the enter method.
            _currentState = _registeredStates[stateName];
            _currentState.Enter();
        }

        public void HandleInput(InputData input)
        {
            // Let the current state, handle the input.
            MovementState stateName = _currentState.HandleInput(input);
            
            // Switch states, if the state is not equal to none.
            if (stateName != MovementState.None) EnterState(stateName);
        }

        public void Update(float deltaTime)
        {
            // Let the current state, handle update.
            MovementState stateName = _currentState.HandleUpdate(deltaTime);
            
            // Switch states, if the state is not equal to none.
            if (stateName != MovementState.None) EnterState(stateName);
        }
    }
}
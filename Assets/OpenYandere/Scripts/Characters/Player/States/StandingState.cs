using OpenYandere.Managers;
using OpenYandere.Characters.Player.States.Traits;

namespace OpenYandere.Characters.Player.States
{
    internal class StandingState : IState
    {
        private PlayerManager _playerManager;
        
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter() { }

        public MovementState HandleInput(InputData input)
        {
            // If the player is moving but not running, switch to the walking state.
            if (input.IsMoving && !input.IsRunning) return MovementState.Walking;
            
            // The player MUST be running, if they are moving but not walking.
            // If the player is moving and running, switch to running state.
            return input.IsMoving ? MovementState.Running : MovementState.None;
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            // TODO: Stamina?
            
            return MovementState.None;
        }
    }
}
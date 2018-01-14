using OpenYandere.Managers;
using OpenYandere.Characters.Player.States.Traits;

namespace OpenYandere.Characters.Player.States
{
    public class RunningState : IState
    {
        private PlayerManager _playerManager;
        
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter()
        {
            var playerMovement = _playerManager.PlayerMovement;
            playerMovement.SetMovementSpeed(playerMovement.RunSpeed);
        }

        public MovementState HandleInput(InputData input)
        {
            // If the player is not running, but is moving switch to the walking state.
            if (!input.IsRunning && input.IsMoving) return MovementState.Walking;
            
            // If the player is not moving switch to the standing state.
            return !input.IsMoving ? MovementState.Standing : MovementState.None;
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            return MovementState.None;
        }
    }
}
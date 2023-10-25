using OpenYandere.Managers;
using OpenYandere.Characters.Player.States.Traits;

namespace OpenYandere.Characters.Player.States
{
    public class WalkingState : IState
    {
        private PlayerManager _playerManager;
        
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter()
        {
            var playerMovement = _playerManager.PlayerMovement;
            playerMovement.SetMovementSpeed(playerMovement.WalkSpeed);
        }

        public MovementState HandleInput(InputData input)
        {
            // If the player is moving and running, switch to running state.
            if (input.IsMoving && input.IsRunning) return MovementState.Running;
            
            // If the player is not moving, switch to standing state.
            return !input.IsMoving ? MovementState.Standing : MovementState.None;
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            return MovementState.None;
        }
    }
}
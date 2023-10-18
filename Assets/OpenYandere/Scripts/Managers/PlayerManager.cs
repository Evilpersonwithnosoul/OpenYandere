using UnityEngine;
using OpenYandere.Characters.Player;
using OpenYandere.Characters.Player.States;

namespace OpenYandere.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private readonly MovementStateMachine _movementStateMachine = new();
        
        [Header("References:")]
        public GameObject Player;
        public PlayerMovement PlayerMovement;
        
        private void Awake()
        {
            var standingState = new StandingState();
            var walkingState = new WalkingState();
            var runningState = new RunningState();

            standingState.Constructor(this);
            walkingState.Constructor(this);
            runningState.Constructor(this);

            _movementStateMachine.RegisterState(MovementState.Standing, standingState);
            _movementStateMachine.RegisterState(MovementState.Walking, walkingState);
            _movementStateMachine.RegisterState(MovementState.Running, runningState);
            _movementStateMachine.EnterState(MovementState.Standing);
            
            PlayerMovement.Constructor(_movementStateMachine);
        }
    }
}
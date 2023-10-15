using OpenYandere.Managers;

namespace OpenYandere.Characters.Player.States.Traits
{
    public interface IState
    {
        void Constructor(PlayerManager playerManager);
        void Enter();
        MovementState HandleInput(InputData input);
        MovementState HandleUpdate(float deltaTime);
    }
}
using OpenYandere.Characters.Player.States.Traits;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.Player.States
{
    public class CrouchingState : IState
    {
        private PlayerManager _playerManager;
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public MovementState HandleInput(InputData input)
        {
            throw new System.NotImplementedException();
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

    }

}

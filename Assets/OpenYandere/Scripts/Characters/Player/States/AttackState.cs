using OpenYandere.Characters.Player.States.Traits;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.Player.States
{
    internal class AttackState : IState
    {
        private PlayerManager _playerManager;
        
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter()
        {
            _playerManager.PlayerMovement.BlockMovement();
            // Aqui você pode adicionar a animação de ataque ou outras ações relevantes
        }

        public MovementState HandleInput(InputData input)
        {
            if (!input.IsAttacking) return MovementState.Standing;
            
            return MovementState.None;
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            return MovementState.None;
        }
    }
}

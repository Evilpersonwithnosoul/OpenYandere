using OpenYandere.Characters.Player.States.Traits;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.Player.States
{
    internal class DeadState : IState
    {
        private PlayerManager _playerManager;
        
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter()
        {
            _playerManager.PlayerMovement.BlockMovement();
            // Aqui você pode adicionar a animação de morte ou outras ações relevantes
        }

        public MovementState HandleInput(InputData input)
        {
            // Uma vez que o jogador está morto, não deve haver transição de estado com base na entrada
            return MovementState.None;
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            // Sem mudanças de estado durante a atualização, pois o jogador está morto
            return MovementState.None;
        }
    }
}

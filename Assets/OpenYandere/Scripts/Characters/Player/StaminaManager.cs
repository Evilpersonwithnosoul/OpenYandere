using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.Player
{
    public class StaminaManager
    {
        public float CurrentStamina { get; private set; }
        public float MaxStamina { get; private set; }
        protected float _staminaRecoveryRate, _staminaDrainRate;

        public StaminaManager(float maxStamina, float staminaRecoveryRate, float staminaDrainRate)
        {
            MaxStamina = maxStamina;
            CurrentStamina = MaxStamina;
            _staminaRecoveryRate = staminaRecoveryRate;
            _staminaDrainRate = staminaDrainRate;
        }

        public void DrainStamina(float deltaTime)
        {
            CurrentStamina = Mathf.Max(0, CurrentStamina - _staminaDrainRate * deltaTime);
        }

        public void RecoverStamina(float deltaTime)
        {
            CurrentStamina = Mathf.Min(MaxStamina, CurrentStamina + _staminaRecoveryRate * deltaTime);
        }

        public bool CanRun()
        {
            return CurrentStamina > 0;
        }
    }
}

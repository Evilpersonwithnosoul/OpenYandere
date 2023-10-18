using UnityEngine;
using OpenYandere.Managers.Traits;
using OpenYandere.Characters.Player;

namespace OpenYandere.Managers
{
    internal class GameManager : Singleton<GameManager>
    {
        [Header("Managers:")]
        public UIManager UIManager;
        public CameraManager CameraManager;
        public PlayerManager PlayerManager;
        public Player _player;
        public ObjectPoolManager ObjectPoolManager;
        public EquipmentManager equipmentManager;
        public void Resume()
        {
            
        }

        public void Pause()
        {
            
        }
    }
}
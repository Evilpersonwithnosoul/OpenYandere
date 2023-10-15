using UnityEngine;
using OpenYandere.Managers.Traits;

namespace OpenYandere.Managers
{
    internal class GameManager : Singleton<GameManager>
    {
        [Header("Managers:")]
        public UIManager UIManager;
        public CameraManager CameraManager;
        public PlayerManager PlayerManager;
        public ObjectPoolManager ObjectPoolManager;
        
        public void Resume()
        {
            
        }

        public void Pause()
        {
            
        }
    }
}
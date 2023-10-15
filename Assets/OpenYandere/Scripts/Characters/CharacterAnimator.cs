using UnityEngine;

namespace OpenYandere.Characters
{
    internal struct AnimatorData
    {
        public Vector3 MoveDirection;
        public bool IsRunning;
    }
    
    [RequireComponent(typeof(Animator))]
    internal class CharacterAnimator : MonoBehaviour
    {
        private AnimatorData _animatorData;
        
        [Header("References:")]
        [SerializeField] private Animator _animator;

        private void LateUpdate()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_animator.speed == 0f) return;
            
            _animator.SetFloat("Horizontal", _animatorData.MoveDirection.x);
            _animator.SetFloat("Vertical", _animatorData.MoveDirection.z);
            _animator.SetBool("Running", _animatorData.IsRunning);
        }
        
        public void UpdateData(AnimatorData animatorData)
        {
            _animatorData = animatorData;
        }
        
        public void Resume()
        {
            _animator.speed = 1f;
        }

        public void Pause()
        {
            _animator.speed = 0f;
        }
    }
}
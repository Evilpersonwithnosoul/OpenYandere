using OpenYandere.Characters.Interactions.Prefabs.NPC;
using UnityEngine;

namespace OpenYandere.Characters
{
    [System.Serializable, RequireComponent(typeof(CharacterAnimator), typeof(RagdollEnabler))]
    public abstract class Character : MonoBehaviour, IDamageable
    {
        protected Animator animator;
        public int Id;
        public string characterName;
        public int maxHealth = 100, health;
        [Range(-100, 100)] public int trustLevel;
        public Sprite faceSprite;
        public bool IsAlive = true,canTakeDamage=true;
        public enum GenderType { Male, Female }
        public GenderType Gender;
        public Transform headIKTarget;

        public RagdollEnabler ragdollEnabler;
        protected void Awake()
        {
            // Suponhamos que o personagem tenha um Animator.
            animator = GetComponent<Animator>();
            ragdollEnabler = GetComponent<RagdollEnabler>();
            health = maxHealth;
        }
        protected void Start()
        {
            if (health <= 0)
            {
                Die();
            }
        }

        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;
            Debug.Log($"{characterName} has {health} health remaining.");
            if (health <= 0)
            {
                Die();
            }
        }
        protected void Die()
        {
            IsAlive = false;           
            ragdollEnabler.EnableRagdoll();
            animator.SetTrigger("Die");

        }


    }
}


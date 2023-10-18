using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace OpenYandere.Characters.Interactions.Prefabs.NPC
{
    public class RagdollEnabler : MonoBehaviour
    {
        [SerializeField]
        private Animator Animator;
        [SerializeField]
        private Transform RagdollRoot;
        [SerializeField]
        private bool StartRagdoll = false;
        // Only public for Ragdoll Runtime GUI for explosive force
        public Rigidbody[] Rigidbodies;
        private CharacterJoint[] Joints;
        private Collider[] Colliders;
        protected Rig _rig;
        private void Awake()
        {
            Rigidbodies = RagdollRoot.GetComponentsInChildren<Rigidbody>();
            Joints = RagdollRoot.GetComponentsInChildren<CharacterJoint>();
            Colliders = RagdollRoot.GetComponentsInChildren<Collider>();
            _rig = GetComponentInChildren<Rig>();
            if (StartRagdoll)
            {
                EnableRagdoll();
            }
            else
            {
                EnableAnimator();
            }
        }

        public void EnableRagdoll()
        {
            _rig.weight = 0;
            Animator.enabled = false;
            foreach (CharacterJoint joint in Joints)
            {
                joint.enableCollision = true;
            }
            foreach (Collider collider in Colliders)
            {
                collider.enabled = true;
            }
            foreach (Rigidbody rigidbody in Rigidbodies)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.detectCollisions = true;
                rigidbody.useGravity = true;
            }
        }

        public void EnableAnimator()
        {
            _rig.weight = 1;
            Animator.enabled = true;
            foreach (CharacterJoint joint in Joints)
            {
                joint.enableCollision = false;
            }
            foreach (Collider collider in Colliders)
            {
                collider.enabled = false;
            }
            foreach (Rigidbody rigidbody in Rigidbodies)
            {
                rigidbody.detectCollisions = false;
                rigidbody.useGravity = false;
            }
        }
    }

}

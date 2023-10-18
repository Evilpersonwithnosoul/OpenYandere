using OpenYandere.Characters.Player;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

namespace OpenYandere.Characters.NPC
{

	[RequireComponent(typeof(NPCMovement))]
	public abstract class NPC : Character
	{
        public NPCMovement NPCMovement => _npcMovement;
        [SerializeField] protected NPCMovement _npcMovement;
        public GameObject player;
        public float detectionDistance = 10.0f, dangerDistance = 5.0f;
        public bool isPlayerDetected = false, isInDanger = false;
        public float fieldOfViewAngle = 120.0f;
        public LayerMask viewMask;
        public Routine dailyRoutine;
        private int currentActivityIndex = 0;

        private void Awake()
        {
            _npcMovement=GetComponent<NPCMovement>();

        }
        protected void Start()
        {
            ClockSystem.Instance.OnTimeChanged += CheckActivity;

            if (dailyRoutine.activities.Count > 0)
            {
                dailyRoutine.activities[0].OnActivityStart(this);
            }
        }
        void FixedUpdate()
        {
            DetectPlayer();

            if (isInDanger)
            {
                _npcMovement.FleeFromPlayer();
                //StartCoroutine(LookAtWeaponAndReact());
            }
        }


        private void CheckActivity()
        {
            if ((currentActivityIndex >= dailyRoutine.activities.Count) || dailyRoutine.activities.Count == 0) return;

            ActivityBase currentActivity = dailyRoutine.activities[currentActivityIndex];
            int currentTime = ClockSystem.Instance.GetTimeMilitary();

            if (currentTime >= currentActivity.startTimeMilitary && currentTime <= currentActivity.endTimeMilitary)
            {
                currentActivity.DoActivity(this);
            }
            else if (currentTime > currentActivity.endTimeMilitary)
            {
                currentActivity.OnActivityEnd(this);
                currentActivityIndex++;
                if (currentActivityIndex < dailyRoutine.activities.Count)
                {
                    dailyRoutine.activities[currentActivityIndex].OnActivityStart(this);
                    CheckActivity();
                }
            }
        }

        private void OnDestroy()
        {
            ClockSystem.Instance.OnTimeChanged -= CheckActivity;
        }

        void DetectPlayer()
        {
            Vector3 directionToPlayer = player.transform.position - this.transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            float angleBetweenNPCAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenNPCAndPlayer < fieldOfViewAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out RaycastHit hit, detectionDistance, viewMask))
                {
                    if (hit.collider.gameObject == player)
                    {
                        isPlayerDetected = true;
                        isInDanger = distanceToPlayer < dangerDistance && GameManager.Instance.equipmentManager.GetWeapon() != null;
                    }
                    else
                    {
                        isPlayerDetected = false;
                        isInDanger = false;
                    }
                }
            }
            else
            {
                isPlayerDetected = false;
                isInDanger = false;
            }
        }

        IEnumerator LookAtWeaponAndReact()
        {
            // Defina a posição do IK Target para a arma do jogador.
            Vector3 weaponPosition = EquipmentManager.Instance.GetWeaponSlot().position; // Supondo que você tenha um método que retorne a posição da arma.
            headIKTarget.position = weaponPosition;

            // Espere um momento para o NPC reconhecer.
            yield return new WaitForSeconds(1.5f);

            // Reação (por exemplo, recuar ou levantar as mãos).
            animator.SetTrigger("ReactToWeapon"); // Supondo que você tenha uma animação de reação.

            yield return new WaitForSeconds(1f); // Espere a reação terminar.

            // Inicie a fuga.
            _npcMovement.FleeFromPlayer();
        }

    }
}

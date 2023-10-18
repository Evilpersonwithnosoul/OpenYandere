using OpenYandere.Characters.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.NPC
{
    [System.Serializable]
    public class SitDownActivity : ActivityBase
    {
        public Vector3 chairLocation;

        public override void OnActivityStart(NPC person)
        {
            // O NPC se move para o local da cadeira.
            person.NPCMovement.NavigationAgent.SetDestination(chairLocation);
        }

        public override void DoActivity(NPC person)
        {
            // Assumindo que o NPC tenha chegado ao destino, eles se sentam.
            if (!person.NPCMovement.NavigationAgent.pathPending)
            {
                //person.NPCMovement.A.PlayAnimation("Sitting");
                Debug.Log("Sitting");
            }
        }

        public override void OnActivityEnd(NPC person)
        {
            // O NPC se levanta.
            //person.PlayAnimation("StandingUp");
            Debug.Log("Stand up");
        }
    }
}

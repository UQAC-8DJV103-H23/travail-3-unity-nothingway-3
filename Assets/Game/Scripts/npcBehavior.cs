using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcBehavior : MonoBehaviour
{
    public QuestManager questManager;

    private void OnTriggerEnter(Collider other)
    {
        //TODO : ADD SPEECH THINGGIES
        questManager.Fetched("Panda");
    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(other.transform.position);
    }
}

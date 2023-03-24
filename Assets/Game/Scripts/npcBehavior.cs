using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcBehavior : MonoBehaviour
{
    public QuestManager questManager;

    private void OnTriggerEnter(Collider other)
    {
        //TODO : ADD SPEECH THINGGIES

        if(other.CompareTag("Player"))
        {
            questManager.Fetched("Panda");

            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(other.transform.position);
    }
}

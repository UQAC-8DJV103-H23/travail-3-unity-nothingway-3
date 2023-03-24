using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcBehavior : MonoBehaviour
{
    public QuestManager questManager;

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(other.transform.position);
    }
}

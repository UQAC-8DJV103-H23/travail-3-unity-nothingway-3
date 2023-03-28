using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalOrb : MonoBehaviour
{
    private QuestManager questManager;

    private void Awake()
    {
        questManager = GameObject.FindGameObjectsWithTag("QuestManager")[0].GetComponent<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            questManager.Fetched("MagicalOrb");

            Destroy(gameObject);

        }

    }
}

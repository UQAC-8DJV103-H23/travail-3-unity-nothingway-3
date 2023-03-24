using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardEnemies : MonoBehaviour
{
    private QuestManager questManager;
    public Wall wall;

    private void Awake()
    {
        questManager = GameObject.FindGameObjectsWithTag("QuestManager")[0].GetComponent<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Projectile"))
        {
            questManager.Slay("Scarecrow");

            wall.ReduceScarecrowsToKill();

            Destroy(gameObject);

        }

    }

}

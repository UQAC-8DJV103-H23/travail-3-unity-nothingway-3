using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardEnemies : MonoBehaviour
{
    public QuestManager questManager;
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Projectile"))
        {
            questManager.Slay("Scarecrow");

            Destroy(gameObject);

        }

    }

}

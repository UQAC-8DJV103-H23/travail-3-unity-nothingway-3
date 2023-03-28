using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardEnemies : MonoBehaviour
{
    private QuestManager questManager;
    public Wall wall;

    [SerializeField] private AudioClip destroySound;


    private void Awake()
    {
        questManager = GameObject.FindGameObjectsWithTag("QuestManager")[0].GetComponent<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Projectile"))
        {
            questManager.Slay("Scarecrow");

            wall.ReduceScarecrowsToKill();

            AudioSource.PlayClipAtPoint(destroySound, gameObject.transform.position);

            Destroy(gameObject);

        }

    }

}

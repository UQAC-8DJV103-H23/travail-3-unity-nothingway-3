using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //TODO : ADD SPEECH THINGGIES
    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(other.transform.position);
    }
}

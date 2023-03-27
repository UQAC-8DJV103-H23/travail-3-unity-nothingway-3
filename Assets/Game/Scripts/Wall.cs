using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int scarecrowsToKill;

    public void ReduceScarecrowsToKill()
    {
        scarecrowsToKill--;

        CheckAmount();
    }

    private void CheckAmount()
    {
        if(scarecrowsToKill <= 0)
        {
            Destroy(gameObject);
        }
    }
}

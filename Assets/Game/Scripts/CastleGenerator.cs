using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGenerator : MonoBehaviour
{
    [SerializeField] ProceduralToolkit.Circle2 firstSpawnCircle;
    [SerializeField] ProceduralToolkit.Circle2 secondSpawnCircle;

    [SerializeField] GameObject redPrefab;
    [SerializeField] GameObject bluePrefab;

    bool placementCorrect = false;
    bool firstPlaced = false;
    bool isRedFirstCircle;

    // Start is called before the first frame update
    void Start()
    {
        PlaceRedCastle();
        PlaceBlueCastle();
    }

    void PlaceRedCastle()
    {
        int random = Random.Range(0, 1);

        Vector3 position;
        if (random == 1)
        {
            isRedFirstCircle = true;


            position.x = ProceduralToolkit.RandomE.PointInCircle2(firstSpawnCircle).x;
            position.z = ProceduralToolkit.RandomE.PointInCircle2(firstSpawnCircle).y;
            position.y = 500f;  //Change height to not be under the map for raycast
            position.y = GetGroundPosition(position);

            if (placementCorrect)
            {
                placementCorrect = false;
                Instantiate(redPrefab, position, Quaternion.identity);
                firstPlaced = true;
            }

        }
        else
        {
            isRedFirstCircle = false;

            position.x = ProceduralToolkit.RandomE.PointInCircle2(secondSpawnCircle).x;
            position.z = ProceduralToolkit.RandomE.PointInCircle2(secondSpawnCircle).y;
            position.y = 500f;  //Change height to not be under the map for raycast
            position.y = GetGroundPosition(position);

            if (placementCorrect)
            {
                placementCorrect = false;
                Instantiate(redPrefab, position, Quaternion.identity);
            }

        }
    }

    void PlaceBlueCastle()
    {
        Vector3 position;
        if (!isRedFirstCircle)
        {
            position.x = ProceduralToolkit.RandomE.PointInCircle2(firstSpawnCircle).x;
            position.z = ProceduralToolkit.RandomE.PointInCircle2(firstSpawnCircle).y;
            position.y = 500f;  //Change height to not be under the map for raycast
            position.y = GetGroundPosition(position);

            if (placementCorrect)
            {
                placementCorrect = false;
                Instantiate(bluePrefab, position, Quaternion.identity);
                firstPlaced = true;
            }

        }
        else
        {
            position.x = ProceduralToolkit.RandomE.PointInCircle2(secondSpawnCircle).x;
            position.z = ProceduralToolkit.RandomE.PointInCircle2(secondSpawnCircle).y;
            position.y = 500f;  //Change height to not be under the map for raycast
            position.y = GetGroundPosition(position);

            if (placementCorrect)
            {
                placementCorrect = false;
                Instantiate(bluePrefab, position, Quaternion.identity);
            }

        }
    }


    private float GetGroundPosition(Vector3 position)
    {
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(position, Vector3.down, out hit);
        if (raycastHit)
        {
            if (hit.collider.tag == "Ground")
            {
                placementCorrect = true;
                return hit.point.y;
            }
            else
            {
                placementCorrect = false;
                if (!firstPlaced)
                {
                    if (isRedFirstCircle)
                    {
                        PlaceRedCastle();
                    }
                    else
                    {
                        PlaceBlueCastle();
                    }
                }
                else
                {
                    if (!isRedFirstCircle)
                    {
                        PlaceRedCastle();
                    }
                    else
                    {
                        PlaceBlueCastle();
                    }
                }

            }
        }
        return -1f;
    }
}

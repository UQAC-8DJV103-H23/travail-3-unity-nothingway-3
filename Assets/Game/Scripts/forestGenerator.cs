using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestGenerator : MonoBehaviour
{
    [SerializeField] Rect spawnRect;
    [SerializeField] GameObject[] assetToInstanciate;
    [SerializeField] int nbAssetToSpawn;
    [SerializeField] GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        GenerateForest();
    }

    // Update is called once per frame
    private void GenerateForest()
    {
        Vector3 position;
        for (int i = 0; i < nbAssetToSpawn; i++)
        {
            int indexToInstantiate = Random.Range(0, assetToInstanciate.Length);

            position.x = ProceduralToolkit.RandomE.PointInRect(spawnRect).x;
            position.z = ProceduralToolkit.RandomE.PointInRect(spawnRect).y;
            position.y = GetGroundPosition(new Vector3(position.x, 100f, position.z));

            if (position.y == -1f)
            {
                position.x = ProceduralToolkit.RandomE.PointInRect(spawnRect).x;
                position.z = ProceduralToolkit.RandomE.PointInRect(spawnRect).y;
                position.y = GetGroundPosition(new Vector3(position.x, 100f, position.z));
            }
            else
            {
                Instantiate(assetToInstanciate[indexToInstantiate], position, assetToInstanciate[indexToInstantiate].transform.rotation, parent.transform);

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
                return hit.point.y;
            else
                return -1f;
        }
        return -1f;
    }
}

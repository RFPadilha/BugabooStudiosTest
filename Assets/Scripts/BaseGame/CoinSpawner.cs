using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;

    public float xMin, xMax;
    public float zMin, zMax;

    Vector3 gizmoPos;
    public LayerMask layerMask;
    Vector3 spawnPos;

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }
    IEnumerator SpawnCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.1f,.8f));
            float randomAmount = Random.Range(1f, 3f);
            for (int i = 0; i < randomAmount; i++)
            {
                Vector3 chosenPos = RandomSpawnPos();
                if (Helper.SafeSpawnPoint(chosenPos, layerMask, collectiblePrefabs[0].GetComponent<Collectible>().pickUpRadius))
                Instantiate(collectiblePrefabs[0], chosenPos, transform.rotation);
            }
        }
    }

    Vector3 RandomSpawnPos()
    {
        float randomX = Random.Range(xMin, xMax);
        float randomZ = Random.Range(zMin, zMax);

        return new Vector3(randomX, .5f, randomZ);
    }
}

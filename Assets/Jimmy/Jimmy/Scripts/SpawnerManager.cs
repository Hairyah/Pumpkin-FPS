using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] GameObject witchPrefab;
    [SerializeField] GameObject caretakerPrefab;

    [SerializeField] int nbrOfEnnemyBySpawn;
    [SerializeField] int delayToSpawn;

    bool canSpawn;

    System.Random rnd = new System.Random();

    void Awake()
    {
        canSpawn = true;
    }

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        canSpawn = false;
        for (int i = 0; i < nbrOfEnnemyBySpawn; ++i)
        {
            int randomType = rnd.Next(0, 99);
            if (randomType < 25)
            {
                GameObject newEnnemyByGameObject = InstantiateEnnemy(ghostPrefab, transform.gameObject);
            }
            else if (randomType >= 25 && randomType < 50)
            {
                GameObject newEnnemyByGameObject = InstantiateEnnemy(witchPrefab, transform.gameObject);
            }
            else
            {
                GameObject newEnnemyByGameObject = InstantiateEnnemy(caretakerPrefab, transform.gameObject);
            }
        }
        yield return new WaitForSeconds(delayToSpawn);
        canSpawn = true;
    }

    public GameObject InstantiateEnnemy(GameObject prefab, GameObject spawner)
    {
        System.Random rnd = new System.Random();

        //Get the world position of the specified spawner
        Vector3 spawnerPosition = spawner.transform.position;

        //Get two random float within a range of radius
        double minRadius = 0;
        double maxRadius = 0;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Cryptes":
                minRadius = 1;
                maxRadius = 5;
                break;

            case "Cimetiere":
                minRadius = 10;
                maxRadius = 14;
                break;
        }
        
        double range = maxRadius - minRadius;
        float randX = (float)((rnd.NextDouble() * range) + minRadius);
        float randz = (float)((rnd.NextDouble() * range) + minRadius);

        //Get a random float between 0 and 1, if round to 1 spawningPosition = spawnerPosition + radius, if round to 0 spawningPosition = spawnerPosition - radius
        //The instance of the gameObject is set as child of the spwaner : in spwanerScript, 
        if ((int)Mathf.Round((float)rnd.NextDouble()) == 1)
        {
            if ((int)Mathf.Round((float)rnd.NextDouble()) == 1)
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x + randX), spawnerPosition.y + 1, (spawnerPosition.z + randz)), Quaternion.identity, spawner.transform);
                return character;
            }
            else
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x + randX), spawnerPosition.y + 1, (spawnerPosition.z - randz)), Quaternion.identity, spawner.transform);
                return character;
            }
        }
        else
        {
            if ((int)Mathf.Round((float)rnd.NextDouble()) == 1)
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x - randX), spawnerPosition.y + 1, (spawnerPosition.z + randz)), Quaternion.identity, spawner.transform);
                return character;
            }
            else
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x - randX), spawnerPosition.y + 1, (spawnerPosition.z - randz)), Quaternion.identity, spawner.transform);
                return character;
            }
        }
    }
}

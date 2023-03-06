using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using System;
using Assets.Scripts;
>>>>>>> Stashed changes

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] GameObject witchPrefab;
    [SerializeField] GameObject caretakerPrefab;

    public List<Ennemy> ennemyListByClass;
    public List<GameObject> ennemyListByGameObject;

    [SerializeField] EnnemyData ghostData;
    [SerializeField] EnnemyData witchData;
    [SerializeField] EnnemyData caretakerData;

    [SerializeField] int nbrOfEnnemyBySpawn;
    public int nbrOfSpawns;
    public int nbrOfWavesSpawned;
    bool canSpawn;

    System.Random rnd = new System.Random();

    void Awake()
    {
        ennemyListByClass = new List<Ennemy>();
        ennemyListByGameObject = new List<GameObject>();
        nbrOfWavesSpawned = 0;
        canSpawn = true;
    }

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    private void SpawnInit()
    {
        for (int i = 0; i < nbrOfEnnemyBySpawn; ++i)
        {
            int randomType = rnd.Next(0, 99);
            if (randomType < 25)
            {
                Ennemy newEnnemyByClass = new Ennemy("Ghost");
                ennemyListByClass.Add(newEnnemyByClass);
                GameObject newEnnemyByGameObject = newEnnemyByClass.InstantiateEnnemy(ghostPrefab, transform.gameObject);
                newEnnemyByGameObject.name = (ennemyListByClass.Count - 1).ToString();
                newEnnemyByGameObject.transform.GetChild(0).gameObject.name = (ennemyListByClass.Count - 1).ToString();
                ennemyListByGameObject.Add(newEnnemyByGameObject);
            }
            else if (randomType >= 25 && randomType < 50)
            {
                Ennemy newEnnemyByClass = new Ennemy("Witch");
                ennemyListByClass.Add(newEnnemyByClass);
                GameObject newEnnemyByGameObject = newEnnemyByClass.InstantiateEnnemy(witchPrefab, transform.gameObject);
                newEnnemyByGameObject.name = (ennemyListByClass.Count - 1).ToString();
                newEnnemyByGameObject.transform.GetChild(0).gameObject.name = (ennemyListByClass.Count - 1).ToString();
                ennemyListByGameObject.Add(newEnnemyByGameObject);
            }
            else
            {
                Ennemy newEnnemyByClass = new Ennemy("Caretaker");
                ennemyListByClass.Add(newEnnemyByClass);
                GameObject newEnnemyByGameObject = newEnnemyByClass.InstantiateEnnemy(caretakerPrefab, transform.gameObject);
                newEnnemyByGameObject.name = (ennemyListByClass.Count - 1).ToString();
                Debug.Log("nameBefor" + newEnnemyByGameObject.transform.GetChild(0).gameObject.name);
                newEnnemyByGameObject.transform.GetChild(0).gameObject.name = (ennemyListByClass.Count - 1).ToString();
                Debug.Log("nameAfter" + newEnnemyByGameObject.transform.GetChild(0).gameObject.name);
                ennemyListByGameObject.Add(newEnnemyByGameObject);
            }
        }
        ++nbrOfWavesSpawned;
    }

    IEnumerator Spawn()
    {
        canSpawn = false;
        for (int i = 0; i < nbrOfEnnemyBySpawn; ++i)
        {
            //Debug.Log(ghostData.health);
            int randomType = rnd.Next(0, 99);
            if (randomType < 25)
            {
                GameObject newEnnemyByGameObject = InstantiateEnnemy(ghostPrefab, transform.gameObject);
                //newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize(Resources.Load<EnnemyData>("Assets/Jimmy/Jimmy/Scripts/GhostData.asset"));
                newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize(ghostData);
            }
            else if (randomType >= 25 && randomType < 50)
            {
                GameObject newEnnemyByGameObject = InstantiateEnnemy(witchPrefab, transform.gameObject);
                //newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize(Resources.Load<EnnemyData>("Assets/Jimmy/Jimmy/Scripts/WitchData.asset"));
                newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize(witchData);
            }
            else
            {
<<<<<<< Updated upstream
                Ennemy newEnnemyByClass = new Ennemy("Caretaker");
                ennemyListByClass.Add(newEnnemyByClass);
                GameObject newEnnemyByGameObject = newEnnemyByClass.InstantiateEnnemy(caretakerPrefab, transform.gameObject);
                newEnnemyByGameObject.name = (ennemyListByClass.Count - 1).ToString();
                newEnnemyByGameObject.transform.GetChild(0).gameObject.name = (ennemyListByClass.Count - 1).ToString();
                ennemyListByGameObject.Add(newEnnemyByGameObject);
=======
                GameObject newEnnemyByGameObject = InstantiateEnnemy(caretakerPrefab, transform.gameObject);
                //newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize(Resources.Load<EnnemyData>("Assets/Jimmy/Jimmy/Scripts/CaretakerData.asset"));
                newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize(caretakerData);
>>>>>>> Stashed changes
            }
        }
        ++nbrOfWavesSpawned;
        yield return new WaitForSeconds(30);
        canSpawn = true;
    }
<<<<<<< Updated upstream
=======

    public int GetIndex(string name)
    {
        int index = Convert.ToInt32(name);
        return index;
    }

    public GameObject InstantiateEnnemy(GameObject prefab, GameObject spawner)
    {
        System.Random rnd = new System.Random();

        //Get the world position of the specified spawner
        Vector3 spawnerPosition = spawner.transform.position;

        //Get two random float within a range of radius
        double minRadius = 1;
        double maxRadius = 5;
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
>>>>>>> Stashed changes
}

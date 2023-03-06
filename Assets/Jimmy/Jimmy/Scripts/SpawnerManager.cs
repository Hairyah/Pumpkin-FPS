using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] GameObject witchPrefab;
    [SerializeField] GameObject caretakerPrefab;

    public List<Ennemy> ennemyListByClass;
    public List<GameObject> ennemyListByGameObject;

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

    IEnumerator Spawn()
    {
        canSpawn = false;
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
                newEnnemyByGameObject.transform.GetChild(0).gameObject.name = (ennemyListByClass.Count - 1).ToString();
                //newEnnemyByGameObject.GetComponent<EnnemyManager>().Initialize()
                ennemyListByGameObject.Add(newEnnemyByGameObject);
            }
        }
        ++nbrOfWavesSpawned;
        yield return new WaitForSeconds(30);
        canSpawn = true;
    }

    public int GetIndex(string name)
    {
        int index = Convert.ToInt32(name);
        return index;
    }
}

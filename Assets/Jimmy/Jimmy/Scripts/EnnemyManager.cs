using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnnemyManager : MonoBehaviour
{
    [SerializeField] SpawnerManager spawner;
    private Ennemy ennemyReferenceInClassList;
    GameObject target;
    NavMeshAgent navMeshAgent;
    float distanceFromTarget;
    bool hasAttack;

    [SerializeField] GameObject ghostAttackPrefab;
    [SerializeField] GameObject witchAttackPrefab;
    [SerializeField] GameObject caretakerAttackPrefab;

    bool isInitialize;

    void Start()
    {
        spawner = transform.parent.gameObject.GetComponent<SpawnerManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        isInitialize = false;
        hasAttack = false;
        StartCoroutine(Init());
    }

    void Update()
    {
        if (isInitialize)
        {
            navMeshAgent.SetDestination(target.transform.position);
            distanceFromTarget = GetDistanceFromTarget(transform.position, target.transform.position);
            if (distanceFromTarget <= ennemyReferenceInClassList.rangeToAttack && !hasAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        hasAttack = true;
        Vector3 direction = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
        Vector3 positionToInstance = Vector3.MoveTowards(new Vector3 (transform.position.x, 0, transform.position.z), direction * int.MaxValue, 0.1f);
        positionToInstance.y = 1f;
        //Vector3 positionToInstance = transform.position + Vector3.forward;
        switch (ennemyReferenceInClassList.type)
        {
            case "Ghost":
                Instantiate(ghostAttackPrefab, positionToInstance, Quaternion.identity);
                break;

            case "Witch":
                Instantiate(witchAttackPrefab, positionToInstance, Quaternion.identity);
                break;

            case "Caretaker":
                Instantiate(caretakerAttackPrefab, positionToInstance, Quaternion.identity);
                break;
        }

        yield return new WaitForSeconds(new System.Random().Next(2, 6));
        hasAttack = false;
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.1f);
        ennemyReferenceInClassList = spawner.ennemyListByClass[GetIndex()];
        target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(target);
        isInitialize = true;
    }

    private int GetIndex()
    {
        int index = Convert.ToInt32(transform.gameObject.name);
        return index;
    }

    private float GetDistanceFromTarget(Vector3 ennemy, Vector3 target)
    {
        return Vector3.Distance(ennemy, target);
    }
}

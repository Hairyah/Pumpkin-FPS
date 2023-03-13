using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Assets.Scripts;

public class EnnemyManager : MonoBehaviour
{
    [SerializeField] SpawnerManager spawner;
    GameObject target;
    NavMeshAgent navMeshAgent;
    float distanceFromTarget;
    bool hasAttack;

    [SerializeField] EnnemyData ennemyData;

    [SerializeField] GameObject ghostAttackPrefab;
    [SerializeField] GameObject witchAttackPrefab;
    [SerializeField] GameObject caretakerAttackPrefab;

    public int health;

    bool isInitialize;

    void Start()
    {
        health = ennemyData.health;
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
            navMeshAgent.speed = ennemyData.moveSpeed;
            distanceFromTarget = GetDistanceFromTarget(transform.position, target.transform.position);
            if (distanceFromTarget <= ennemyData.rangeToAttack && !hasAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        hasAttack = true;
        Vector3 direction = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
        Vector3 positionToInstance = Vector3.MoveTowards(transform.position, direction * int.MaxValue, 0.2f);
        positionToInstance.y += 1;
        switch (ennemyData.type)
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

        yield return new WaitForSeconds(new System.Random().Next(ennemyData.delayToAttack, ennemyData.delayToAttack + 3));
        hasAttack = false;
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.1f);
        target = GameObject.FindGameObjectWithTag("Player");
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

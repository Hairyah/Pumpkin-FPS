using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}

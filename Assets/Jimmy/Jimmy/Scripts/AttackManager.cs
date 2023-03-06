using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    GameObject player;
    Vector3 target;
    Vector3 direction;
    [SerializeField] float moveSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
        direction = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
        target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        moveSpeed = 0.15f;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction * int.MaxValue, moveSpeed);

        if (transform.position == target)
        {
            Debug.Log("TARGET REACHED");
            Destroy(transform.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Projectile" && other.gameObject.tag != "Ennemy")
        {
            Destroy(transform.gameObject);
        }
    }
}
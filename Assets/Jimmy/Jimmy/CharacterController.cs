using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameObject spawnerPrefab;
    //private GameObject spawner;

    public Rigidbody playerRigidbody;
    public Transform cameraTransform;

    public float speed = 10f;
    public float mouseSensitivity = 1f;
    public float dashStrength = 10000f;

    public bool canDash;
    private float speedMultiplier;

    private void Start()
    {
        playerRigidbody = transform.GetComponent<Rigidbody>();
        cameraTransform = transform.GetChild(0);
        // Bloquer le curseur de la souris en jeu
        canDash = true;
        speedMultiplier = 1f;
        //spawner = Instantiate(spawnerPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        // Déplacements
        Vector3 velocity = Input.GetAxis("Horizontal")
            * cameraTransform.right * speed // new Vector3(10f, 0f, 0f)
            + Input.GetAxis("Vertical")
            * cameraTransform.forward * speed;
        velocity.y = 0f;
        velocity *= speedMultiplier;
        playerRigidbody.velocity = velocity;

        // On ajoute le déplacement de caméra
        Vector3 rotationCameraEuler
            = cameraTransform.localRotation.eulerAngles
            + Input.GetAxis("Mouse X") * Vector3.up * mouseSensitivity
            - Input.GetAxis("Mouse Y") * Vector3.right * mouseSensitivity;

        // On convertit en quaternion et on applique à la caméra
        cameraTransform.localRotation
            = Quaternion.Euler(rotationCameraEuler);

        // Dash
        if (Input.GetButtonDown("Jump") && canDash)
        {
            playerRigidbody.AddForce(
                cameraTransform.forward
                * dashStrength);
            canDash = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(transform.gameObject.name + " collide with => " + collision.transform.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        speedMultiplier += 0.2f;
    }

    private int GetIndex()
    {
        int index = Convert.ToInt32(transform.gameObject.name);
        return index;
    }
}
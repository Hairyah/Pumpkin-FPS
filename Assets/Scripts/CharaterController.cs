using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform cameraTransform;
 

    [Header ("Functional Options")]
    public bool grounded = false;


    [Header ("Move&Speed Parameters")]
    public float speed = 10f;
    private float speedMultiplier;

    [Header("Dash Parameters")]
    public float dashNonControleTime;
    public float dashMultiplier;
    public float dashResetTime;
    private float lastDash = 0f;

    [Header ("Mouse Parameter")]
    public float mouseSensitivity = 1f;

    [Header ("Jump Parameter")]
    [SerializeField]
    private float distanceToJump = 1.05f;
    [SerializeField]
    private float jumpForce = 10f;

    private float GetHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            return hit.distance;
        return float.MaxValue;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        speedMultiplier = 1f;
    }

    private void Update()
    {
        if (lastDash + dashNonControleTime < Time.time)
        {
            Vector3 velocity = Input.GetAxis("Horizontal")
            * cameraTransform.right * speed // new Vector3(10f, 0f, 0f)
            + Input.GetAxis("Vertical")
            * cameraTransform.forward * speed;
            velocity *= speedMultiplier;
            velocity.y = playerRigidbody.velocity.y;
            playerRigidbody.velocity = velocity;

            velocity.y = 0f;

            if (Input.GetKeyDown(KeyCode.LeftShift) && lastDash + dashResetTime < Time.time)
            {
                velocity *= dashMultiplier;
                playerRigidbody.velocity = velocity;
                lastDash = Time.time;
            }
        }

        Vector3 rotationCameraEuler
            = cameraTransform.localRotation.eulerAngles
            + Input.GetAxis("Mouse X") * Vector3.up * mouseSensitivity
            - Input.GetAxis("Mouse Y") * Vector3.right * mouseSensitivity;

        cameraTransform.localRotation
            = Quaternion.Euler(rotationCameraEuler);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dist = GetHeight();
            if (dist < distanceToJump)
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
        }
    }

    /*private void Update()
    {
        Vector3 velocity = Input.GetAxis("Horizontal") * cameraTransform.right * speed // new Vector3(10f, 0f, 0f)
            + Input.GetAxis("Vertical") * cameraTransform.forward * speed;

        velocity *= speedMultiplier;
        velocity.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = velocity;

        velocity.y = 0f;
        if (Input.GetKeyDown(KeyCode.LeftShift) && FindObjectOfType<Conductor>().TestRythme())
        {
            Debug.Log(FindObjectOfType<Conductor>().TestRythme());

            velocity *= dashMultiplier;
            playerRigidbody.velocity = velocity;
            lastDash = Time.time;
        }

        *//*if (Input.GetKeyDown(KeyCode.LeftShift) && FindObjectOfType<Conductor>().TestRythme() && lastDash + dashResetTime < Time.time) //
        {
            velocity *= dashMultiplier;
            playerRigidbody.velocity = velocity;
            lastDash = Time.time;
        }*//*

        Vector3 rotationCameraEuler= cameraTransform.localRotation.eulerAngles+ Input.GetAxis("Mouse X") * Vector3.up * mouseSensitivity
            - Input.GetAxis("Mouse Y") * Vector3.right * mouseSensitivity;

        cameraTransform.localRotation = Quaternion.Euler(rotationCameraEuler);
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dist = GetHeight();
            if (dist < distanceToJump)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
            }
        }  
    }*/
}


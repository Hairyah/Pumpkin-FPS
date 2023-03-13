using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Stats Joueur")]
    public float pdvJoueur = 100f;
    public Text affPdv;

    LevelManager levelManager;

    private float GetHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            return hit.distance;
        return float.MaxValue;
    }

    private void Start()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        speedMultiplier = 1f;
        affPdv = GameObject.Find("PV").GetComponent<Text>();
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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Item") 
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Heal");

            pdvJoueur += 25;
            if (pdvJoueur> 100){
                pdvJoueur = 100;
            }
            affPdv.text = pdvJoueur.ToString() + " HP";
        }

        if (other.gameObject.tag == "Projectile")
        {
            //FindObjectOfType<AudioManager>().Play("Damage");

            pdvJoueur -= 25;
            affPdv.text = pdvJoueur.ToString() + " HP";
            if (pdvJoueur <= 0)
            {
                levelManager.hasLose = true;
            }
        }
    }
}


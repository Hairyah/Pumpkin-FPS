using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform cameraTransform;

    public float speed = 10f;
    public float mouseSensitivity = 1f;
    public float dashStrength = 10000f;

    public bool canDash;
    private float speedMultiplier;

    [Header("Jump Parameter")]
    [SerializeField]
    private float distanceToJump = 1.05f;
    [SerializeField]
    private float jumpForce = 10f;

    private void Start()
    {
        canDash = true;
        speedMultiplier = 1f;
        
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
        if (Input.GetButtonDown("Dash") && canDash)
        {
            playerRigidbody.AddForce(
                cameraTransform.forward
                * dashStrength);
            canDash = false;
        }

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            float dist = GetHeight();
            if (dist < distanceToJump)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
            }
        }
    }

    private float GetHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            return hit.distance;
        return float.MaxValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // collision.contacts[0].normal
        // Vector3.up
        if (Vector3.Dot(collision.contacts[0].normal,
            Vector3.up) > 0.9f)
            canDash = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        speedMultiplier += 0.2f;
    }
}


using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("General Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 50f;
    public float impactForce = 30f;
    private float nextTimeToFire = 0f;

    public Camera fpsCam;
    public Transform cameraTransform;
    public ParticleSystem muzzleFlash;

    [Header("Stats Smooth")]
    public float intensity;
    public float smooth;
    private Quaternion origin_rotation;

    public Animator animator;
    public GameObject impactEffect;

    public int nbActualBullet = 8;
    public Text affBullet;

    [Header("Shotgun")]
    public bool shotgun = false;
    public int bulletPerShot = 6;
    public float inaccuracyDistance = 5f;

    SpawnerManager spawnerManager;

    private void Start()
    {
        affBullet = GameObject.Find("Bullets").GetComponent<Text>();
        spawnerManager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerManager>();
        origin_rotation = transform.localRotation;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && FindObjectOfType<Conductor>().TestRythme() && Time.time >= nextTimeToFire && nbActualBullet>0) //TIR !
        {
            FindObjectOfType<AudioManager>().Play("Gun");
            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();
        }else if (Input.GetButtonDown("Fire1") && !FindObjectOfType<Conductor>().TestRythme() && nbActualBullet > 0) //Loupe le timing
        {
            FindObjectOfType<AudioManager>().Play("MissTiming");
        }else if (Input.GetButtonDown("Fire1")) //Plus de balle
        {
            FindObjectOfType<AudioManager>().Play("Cling");
        }

        if (Input.GetButtonDown("Reload") && FindObjectOfType<Conductor>().TestRythme()) //Recharger en rythme
        {
            FindObjectOfType<AudioManager>().Play("Reload");
            nbActualBullet = 8;
            affBullet.text = nbActualBullet.ToString() + "/8 bullets";
            animator.SetTrigger("Reload");
        }

        UpdateSway();
    }

    void Shoot()
    {
        animator.SetTrigger("Fire");
        muzzleFlash.Play();
        nbActualBullet--;
        affBullet.text = nbActualBullet.ToString()+"/8 bullets";

        Quaternion recul = Quaternion.AngleAxis(-intensity*2, Vector3.right);
        cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, recul, Time.deltaTime * smooth);

        Vector3 eulerRotation = cameraTransform.localRotation.eulerAngles;
        cameraTransform.localRotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);

        if (shotgun)
        {
            for(int i = 0; i < bulletPerShot; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, GetShootingDirection(), out hit, range/4))
                {
                    //Debug.Log(hit.transform.name);

                    Target target = hit.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                    }

                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                //Debug.Log(hit.transform.name);

                /*Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }*/


                if (hit.transform.gameObject != null)
                {
                    if (hit.transform.gameObject.tag == "Ennemy")
                    {
                        int index = spawnerManager.GetIndex(hit.transform.gameObject.name);
                        spawnerManager.ennemyListByClass[index].health -= 50;
                        if (spawnerManager.ennemyListByClass[index].health <= 0)
                        {
                            Debug.Log("ENTER");
                            Destroy(hit.transform.gameObject);
                        }
                    }
                }

                /*if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }*/

                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    private void UpdateSway()
    {
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y");

        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * t_x_adj * t_y_adj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
    }

    Vector3 GetShootingDirection()
    {
        Vector3 targetPos = fpsCam.transform.position + fpsCam.transform.forward;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance),
            targetPos.y + Random.Range(-inaccuracyDistance, inaccuracyDistance),
            targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance)
            );

        Vector3 direction = targetPos - fpsCam.transform.position;
        return direction.normalized;
    }
}

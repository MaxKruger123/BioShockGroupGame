using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform spawnPoint;
    public float distance = 15f;

    public GameObject muzzle;
    public GameObject impact;
    public float fireRate = 10f;

    private float nextTimeToFire = 0f;

    Camera cam;

    public int maxAmmo = 4;
    private int currentAmmo;
    public float reloadTime = 20f;
    private bool isReloading = false;

    public Animator animator;

    public PlayerMovement playerMovement;
    private Transform playerTransform;

    float old_pos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        currentAmmo = maxAmmo;
        playerMovement = GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>();
        playerTransform = playerMovement.GetComponent<Transform>();
        old_pos = playerTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
            return;

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            animator.SetBool("Reloading", true);

            return;

        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            animator.SetBool("Shot", true);
            Shoot();
        }

        if (old_pos > playerTransform.transform.position.z)
        {
            animator.SetBool("Walking", true);
        } else if (old_pos < playerTransform.transform.position.z)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        old_pos = playerTransform.transform.position.z;
    }

    //Method to shoot
    private void Shoot()
    {
        RaycastHit hit;
        RaycastHit hit_1;
        RaycastHit hit_2;
        RaycastHit hit_3;

        GameObject muzzleInstance = Instantiate(muzzle, spawnPoint.position, spawnPoint.localRotation);
        muzzleInstance.transform.parent = spawnPoint;

       

        currentAmmo--;

        //Bullet that goes forward
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            GameObject gunImpact = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(gunImpact, 3);
           
        }

        //Bullet that goes forward
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-0.2f, 0f, 0f), out hit_1, distance))
        {
            GameObject gunImpact1 = Instantiate(impact, hit_1.point, Quaternion.LookRotation(hit_1.normal));
            
            Destroy(gunImpact1, 3);
            //Apply damage if needed
        }

        //Bullet that goes up
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, 0.1f, 0f), out hit_2, distance))
        {
            GameObject gunImpact2 = Instantiate(impact, hit_2.point, Quaternion.LookRotation(hit_2.normal));
            
            Destroy(gunImpact2, 3);
            //Apply damage if needed
        }


        //Bullet that goes down
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, -0.1f, 0f), out hit_3, distance))
        {
            GameObject gunImpact3 = Instantiate(impact, hit_3.point, Quaternion.LookRotation(hit_3.normal));
            
            Destroy(gunImpact3, 3);
            //Apply damage if needed
        }

        StartCoroutine(Wait());
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        if (currentAmmo == 0)
        {
            yield return new WaitForSeconds(5);
        } else if(currentAmmo == 1)
        {
            yield return new WaitForSeconds(4);
        }
        else if (currentAmmo == 2)
        {
            yield return new WaitForSeconds(3);
        }
        else if (currentAmmo == 3)
        {
            yield return new WaitForSeconds(2);
        }


        currentAmmo = maxAmmo;
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("Shot", false);


    }
}

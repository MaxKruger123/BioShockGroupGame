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
    public bool shotGun;

    public AudioSource audioSource;
    public AudioClip shootSound;

    public AudioSource audioSource1;
    public AudioClip reloadSound1;

    public AudioSource audioSource2;
    public AudioClip reloadSound2;

    public AudioSource audioSource3;
    public AudioClip reloadSound3;

    public AudioSource audioSource4;
    public AudioClip reloadSound4;

    public AudioSource audioSource5;
    public AudioClip walkSound;

    public bool bull;



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

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && !shotGun)
        {
            StartCoroutine(Reload());
            

            return;

        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !isReloading)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            animator.SetBool("Shot", true);
            shotGun = true;
            audioSource.PlayOneShot(shootSound);
            Shoot();
        }

        if (old_pos > playerTransform.transform.position.z)
        {
            if (bull)
            {
                audioSource5.Play();
            }
            bull = false;
            animator.SetBool("Walking", true);
        } else if (old_pos < playerTransform.transform.position.z)
        {
            if (bull)
            {
                audioSource5.Play();
            }
            bull = false;
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
            audioSource5.Stop();
            bull = true;
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
            animator.SetBool("ReloadFour", true);
            audioSource4.PlayOneShot(reloadSound4);
            yield return new WaitForSeconds(5);
        } else if(currentAmmo == 1)
        {
            animator.SetBool("ReloadThree", true);
            audioSource3.PlayOneShot(reloadSound3);
            yield return new WaitForSeconds(1.4f);
        }
        else if (currentAmmo == 2)
        {
            animator.SetBool("ReloadTwice", true);
            audioSource2.PlayOneShot(reloadSound2);
            yield return new WaitForSeconds(1f);
        }
        else if (currentAmmo == 3)
        {
            animator.SetBool("Reloading", true);
            audioSource1.PlayOneShot(reloadSound1);
            yield return new WaitForSeconds(0.4f);
        }


        currentAmmo = maxAmmo;
        isReloading = false;
        animator.SetBool("Reloading", false);
        animator.SetBool("ReloadTwice", false);
        animator.SetBool("ReloadThree", false);
        animator.SetBool("ReloadFour", false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        shotGun = false;
        animator.SetBool("Shot", false);


    }
}

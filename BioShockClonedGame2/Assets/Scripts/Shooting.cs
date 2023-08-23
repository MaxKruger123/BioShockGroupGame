using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform spawnPoint;
    public float distance = 15f;

    public GameObject muzzle;
    public GameObject impact;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
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

        //Bullet that goes forward
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

            //Apply damage if needed
        }

        //Bullet that goes forward
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-0.2f, 0f, 0f), out hit_1, distance))
        {
            Instantiate(impact, hit_1.point, Quaternion.LookRotation(hit_1.normal));

            //Apply damage if needed
        }

        //Bullet that goes up
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, 0.1f, 0f), out hit_2, distance))
        {
            Instantiate(impact, hit_2.point, Quaternion.LookRotation(hit_2.normal));

            //Apply damage if needed
        }


        //Bullet that goes down
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, -0.1f, 0f), out hit_3, distance))
        {
            Instantiate(impact, hit_3.point, Quaternion.LookRotation(hit_3.normal));

            //Apply damage if needed
        }
    }
}

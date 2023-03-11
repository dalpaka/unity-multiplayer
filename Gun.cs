using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.1f;
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public Transform firePoint;
    public LineRenderer laserLine;
    public AudioSource gunAudio;

    private float timeBtwShots = 0f;

    private void Start()
    {
        laserLine.enabled = false;
    }

    private void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        transform.localRotation = Quaternion.identity;

    }

    private void Shoot()
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, firePoint.position);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            // Apply damage to target if it has a health script
            Health targetHealth = hit.transform.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(10);
            }
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(1, fpsCam.transform.position + (fpsCam.transform.forward * range));
        }

        timeBtwShots = fireRate;

        Invoke("DisableLaser", 0.1f);

        gunAudio.Play();
    }


    private void DisableLaser()
    {
        laserLine.enabled = false;
    }
}




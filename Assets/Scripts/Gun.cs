using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public int ammoMax = 10;
    public int ammoCount;
    [Range(0,500)]
    public int ammoOnPerson;
    public float reloadTime;
    public float timer;

    public bool isAutomatic;
    public bool isReloading = false;

    public Camera playerCam;
    public ParticleSystem muzzleFlash;
    public GameObject HitEffect;
    public PlayerLevelUp playerLevelUp;
    [SerializeField] AudioSource shootSFX;
    [SerializeField] AudioSource enemySFX;

    Animator animator;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        if (ammoMax <= ammoOnPerson)
        {
            ammoCount = ammoMax;
        }
        else ammoCount = ammoOnPerson;
        

        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.R) || ammoCount <= 0)
        {
            isReloading = true;

        }
        if(ammoCount == ammoMax || ammoCount == ammoOnPerson && ammoCount > 0 )
        {
            isReloading = false;
        }

        if(isReloading && ammoOnPerson > 0)
        {
            Reloading();            
        }

        else
        {
            if (ammoCount > 0)
            {
                if (isAutomatic == true)
                {
                    if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        Shoot();
                    }
                }

                if (isAutomatic == false)
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        Shoot();
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            AmmoPickup();
        }

            
   
    }

    void OnEnable()
    {
        damage *= playerLevelUp.damageBoost;
        range *= playerLevelUp.rangeBoost;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        ammoCount--;
        ammoOnPerson--;

        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            shootSFX.Play();

            EnemyAi enemyAI = hit.transform.GetComponent<EnemyAi>();

            if(enemyAI != null)
            {
                enemyAI.TakeDamage(damage);
                enemySFX.Play();
                Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            
        }
    }

    void Reloading()
    {
        animator.SetBool("isReloading", true);
        timer += Time.deltaTime;
        if(timer >= reloadTime)
        {

            if (ammoMax <= ammoOnPerson)
            {
                ammoCount = ammoMax;
            }
            else ammoCount = ammoOnPerson;
        
            animator.SetBool("isReloading", false);
            timer = 0;
        }

        isReloading = false;
    }

    public void AmmoPickup()
    {

        RaycastHit hit;
            if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, 5))
        {
            AmmoPack ammoPack = hit.transform.GetComponent<AmmoPack>();

            if(ammoPack != null)
            {
                ammoPack.PickedUp();
            }

            ammoOnPerson += ammoMax / 2;

        }
        
    }
}

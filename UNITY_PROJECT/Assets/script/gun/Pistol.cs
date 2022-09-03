using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    #region variables
    public static Pistol instance;

    RaycastHit hit;
    //used to damage enemy
    [SerializeField]
    float DamageEnemy = 35f;

    [SerializeField]
    float headshoot = 100f;
    
    public bool isReloading;

    public GameObject head;

    public Text curentAmmo_text;

    public Text carriedAmmo_text;
    //Weapon effects
    //muzzleflash
    public ParticleSystem muzzleflash;
    
    //Eject Bullet casing
    public ParticleSystem bulletcasing;
    
    //blood effect
    public GameObject bloodEffect;
    
    //Gun Audio 
    public AudioSource gunAS;
    public AudioClip metalshootAC;
    //Bullet Holes
    public GameObject metalBulletHole;
    
    public AudioClip gunSound;

    public AudioClip dryefireac;

    [SerializeField]
    Transform ShootPoint;

    //Rate od Fire
    [SerializeField]
    float RateOfFire;
    float nextFire = 0;
    
    [SerializeField]
    float weaponRange;
    Animator animator;

    [Header("Iron Sights")]
    public bool ironSightOn = false;
    public GameObject crosshair;
    Camera maincamera;
    int fovNormal = 60;
    int fovIronSight = 30;
    float smoothZoom = 3f;

    [Header("layer Affected")]
    public LayerMask Layer;
    #endregion

    void Start()
    {
        PistolSoundAndMuzzleFlash();
        EjectCasing();
        UpdateAmmoUI();
        gunAS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        maincamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ironSightOn = true;
            crosshair.SetActive(false);
            animator.SetBool("ironsighton", true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            ironSightOn = false;
            crosshair.SetActive(true);
            animator.SetBool("ironsighton", false);
        }
        if(ironSightOn == true)
        {
            maincamera.fieldOfView = Mathf.Lerp(maincamera.fieldOfView, fovIronSight, smoothZoom * Time.deltaTime);
        }
        else if (ironSightOn == false)
        {
            maincamera.fieldOfView = Mathf.Lerp(maincamera.fieldOfView, fovNormal, smoothZoom * Time.deltaTime);
        }
        if (Input.GetButton("Fire1") && GlobalVariables.pistolcurrentAmmo > 0 && !isReloading && !ironSightOn)
        {
            shoot();
        }
        else if (Input.GetButton("Fire1") && GlobalVariables.pistolcurrentAmmo > 0 && !isReloading && ironSightOn)
        {
            shootironsight();
        }
        else if (Input.GetButton("Fire1") && GlobalVariables.pistolcurrentAmmo <= 0)
        {
            DryFire();
        }
        else if (Input.GetKeyDown(KeyCode.R) && GlobalVariables.pistolcurrentAmmo <= GlobalVariables.pistolmaxAmmo)
        {
            Reload();
        }
    }

    private void Awake()
    {
            instance = this;
    }

    void shoot()
    {
        if (Time.time>nextFire)
        {

            nextFire = Time.time + RateOfFire;

            animator.SetTrigger("Shoot");

            GlobalVariables.pistolcurrentAmmo--;
            
            Shootray();
            
            UpdateAmmoUI();
        }
    }

    void shootironsight()
    {
        if (Time.time > nextFire)
        {

            nextFire = Time.time + RateOfFire;

            animator.SetTrigger("ironsightshoot");

            GlobalVariables.pistolcurrentAmmo--;

            Shootray();

            UpdateAmmoUI();
        }
    }

    void Shootray()
    {
        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, weaponRange, Layer))
        {
            if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                enemyHealth.DeductHealth(DamageEnemy);
                Instantiate(bloodEffect, hit.point, transform.rotation);
                Debug.Log("Hit Enemy");
            }
            else if (hit.transform.tag == "headenemy")
            {
                EnemyHealth enemyHealth = hit.transform.GetComponentInParent<EnemyHealth>();
                enemyHealth.DeductHealth(headshoot);
                Instantiate(bloodEffect, hit.point, transform.rotation);
                head.SetActive(false);
                Debug.Log("Hit Enemyhead");
            }
            else if (hit.transform.tag == "metal")
            {
                gunAS.PlayOneShot(metalshootAC);
                Instantiate(metalBulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Debug.Log("Hit Enemyhead");
            }
            else
            {
                Debug.Log(hit.transform.name);
            }
        }
    }

    void DryFire()
    {
        if (!isReloading)
        {
            if (Time.time > nextFire)
            {
                nextFire = 0f;
                nextFire = Time.time + RateOfFire;

                //Add dry fire anim
                gunAS.PlayOneShot(dryefireac);

                animator.SetTrigger("dryfire");

            }
        }
    }

    void Reload()
    {
        if (!isReloading)
        {
            animator.SetTrigger("Reload");
            if (GlobalVariables.pistolcurrentcarriedAmmo <= 0) return;
            StartCoroutine(ReloadCountdown(2f));
        }
    }

    public void UpdateAmmoUI()
    {
        curentAmmo_text.text = GlobalVariables.pistolcurrentAmmo.ToString();
        carriedAmmo_text.text = GlobalVariables.pistolcurrentcarriedAmmo.ToString();
    }

    IEnumerator ReloadCountdown(float timer)
    {
        while(timer > 0f)
        {
            isReloading = true;
            timer-=Time.deltaTime;
            yield return null;
        }
        if(timer <= 0f)
        {
            isReloading=false;
            int bulletsNeeadedToFillMag = GlobalVariables.pistolmaxAmmo - GlobalVariables.pistolcurrentAmmo;
            int bulletsToDeduct = (GlobalVariables.pistolcurrentcarriedAmmo >= bulletsNeeadedToFillMag) ? bulletsNeeadedToFillMag: GlobalVariables.pistolcurrentcarriedAmmo;
            GlobalVariables.pistolcurrentcarriedAmmo -= bulletsToDeduct;
            GlobalVariables.pistolcurrentAmmo += bulletsToDeduct;
            UpdateAmmoUI();
        }
    }

    IEnumerator PistolSoundAndMuzzleFlash()
    {
        muzzleflash.Play();
        gunAS.PlayOneShot(gunSound);
        yield return new WaitForEndOfFrame();
        muzzleflash.Stop();
    }

    IEnumerator EjectCasing()
    {
        bulletcasing.Play();
        yield return new WaitForEndOfFrame();
        bulletcasing.Stop();
    }
}
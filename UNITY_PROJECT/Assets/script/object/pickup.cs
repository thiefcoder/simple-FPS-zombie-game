using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickup : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    float pickupDistance = 5f;
    Camera mainCam;
    public Text pickupText;
    public LayerMask layer;
    string Pickupinfo;
    [Header("Audio")]
    public AudioClip pickupAmmoAC;
    public AudioClip pickupHealthAC;
    public AudioClip pickupKeyAC;
    AudioSource pickupAS;
    string pickuptag;

    private void Start()
    {
        pickupAS = GetComponent<AudioSource>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        ray = mainCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if (Physics.Raycast(ray, out hit, pickupDistance, layer))
        {
            pickupText.enabled = true;
            pickupText.text = hit.transform.name.ToString();
            pickuptag = hit.transform.tag;
            CheckItem();
        }
        else
        {
            pickupText.enabled = false;
        }
    }
    
    void pickupPistolAmmo()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(hit.transform.gameObject);
            GlobalVariables.pistolcurrentcarriedAmmo = GlobalVariables.pistoltotalAmmo;
            Pistol.instance.UpdateAmmoUI();
            pickupAS.PlayOneShot(pickupAmmoAC);
            pickupText.enabled = false;
        }
    }

    void pickupHealth()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            healthpack healthPackScript = hit.transform.GetComponentInChildren<healthpack>();
            float healthAmmount = healthPackScript.healthAmmount;
            if (PlayerHealth.singleton.currentHealth + healthAmmount > PlayerHealth.singleton.MaxHealth)
            {
                PlayerHealth.singleton.currentHealth = PlayerHealth.singleton.MaxHealth;
                PlayerHealth.singleton.updateHealthUI();
            }
            else
            {
                PlayerHealth.singleton.AddHealth(healthAmmount);
            }
            Destroy(hit.transform.gameObject);
            pickupAS.PlayOneShot(pickupHealthAC);
            pickupText.enabled = false;
        }
    }

    void pickkeys()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickupAS.PlayOneShot(pickupKeyAC);
            Destroy(hit.transform.gameObject);
        }
    }

    void CheckItem()
    {
        switch (pickuptag)
        {
            case "pistolAmmo":
                if (GlobalVariables.pistolcurrentcarriedAmmo < GlobalVariables.pistoltotalAmmo)
                {
                    pickupPistolAmmo();
                }
                else
                {
                    Pickupinfo = "Pistol Ammuntunition Full";
                    pickupText.text = Pickupinfo;
                }
                break;
            case "healthpack":
                if (PlayerHealth.singleton.currentHealth < PlayerHealth.singleton.MaxHealth)
                {
                    pickupHealth();
                }
                else
                {
                    Pickupinfo = "Health Full";
                    pickupText.text = Pickupinfo;
                }
                break ;
            case "keys":
                pickkeys();
                break ;
            default:
                break;
        }
    }
}

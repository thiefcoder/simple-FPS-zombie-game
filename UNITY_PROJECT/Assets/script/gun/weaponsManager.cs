using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponsManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] weapons;
    private void Start()
    {
        UnequipWeapons();
        EquipPistol();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipPistol();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipAkm();
        }
    }

    void UnequipWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    void EquipPistol()
    {
        UnequipWeapons();
        weapons[0].SetActive(true);
        Pistol.instance.UpdateAmmoUI();
    }

    void EquipAkm()
    {
        UnequipWeapons();
        weapons[1].SetActive(true);
        AKM.instance.UpdateAmmoUI();
    }
}

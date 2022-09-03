using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    //KEY
    public static bool hasBlueKey = false;
    public static bool hasRedKey = false;
    public static bool hasBlackKey = false;

    //AMMO
    //pistol
    public static int pistolcurrentAmmo = 12;
    public static int pistolmaxAmmo = 12;
    public static int pistolcurrentcarriedAmmo = 30;
    public static int pistoltotalAmmo = 60;

    //AKM
    public static int AKMcurrentAmmo = 30;
    public static int AKMmaxAmmo = 30;
    public static int AKMcurrentcarriedAmmo = 90;
    public static int AKMtotalAmmo = 150;
}

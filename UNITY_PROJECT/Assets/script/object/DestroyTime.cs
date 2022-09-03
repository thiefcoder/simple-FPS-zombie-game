using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public int destroytime = 5;

    private void Awake()
    {
        Destroy(gameObject, destroytime);
    }
}

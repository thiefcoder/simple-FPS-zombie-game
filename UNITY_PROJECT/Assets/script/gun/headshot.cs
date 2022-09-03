using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headshot : MonoBehaviour
{
    public GameObject head;
    public GameObject headshotEffect;
    private void OnDisable()
    {
        head.SetActive(false);
        headshotEffect.SetActive(true);
    }
}

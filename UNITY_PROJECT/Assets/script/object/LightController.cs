using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Light TheLight;

    [SerializeField]
    float minTimeBeforeLightFlickers;
    [SerializeField]
    float MaxTimebeforLightFlickers;

    void Start()
    {
        TheLight = GetComponent<Light>();
        StartCoroutine("MakeLightFlicker");
    }
    IEnumerator MakeLightFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBeforeLightFlickers, MaxTimebeforLightFlickers));
            TheLight.enabled = !TheLight.enabled;
        }
    }
}

using System.Collections;
using UnityEngine;

public class LightControl : MonoBehaviour
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
    IEnumerable MakeLightFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBeforeLightFlickers, MaxTimebeforLightFlickers));
            TheLight.enabled = !TheLight.enabled;
        }
    }

}

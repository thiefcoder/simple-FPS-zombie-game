using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorray : MonoBehaviour
{
    RaycastHit hit;
    public Transform RayShootPoint;
    float rayDistance = 3f;
    public LayerMask layer;
    float tick = 1f;
    void Update()
    {
        tick -= Time.time;
        if (tick <= 0f)
        {
            tick = 1f;
            Debug.DrawRay(RayShootPoint.position, RayShootPoint.forward, Color.red);
            if (Physics.Raycast(RayShootPoint.position, RayShootPoint.forward, out hit,rayDistance,layer))
            {

                Doorscript doortriggerscripts = hit.transform.GetComponent<Doorscript>();
                if (!doortriggerscripts.isOpen && !doortriggerscripts.isLocked)
                {
                    doortriggerscripts.opendoor();
                }
            }
        }
    }
}

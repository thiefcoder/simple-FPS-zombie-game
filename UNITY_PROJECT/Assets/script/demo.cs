using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    float speed = 200;
    // Update is called once per frame
    void Update()
    {
      transform.Rotate(0, 0, speed*Time.deltaTime);
    }
}
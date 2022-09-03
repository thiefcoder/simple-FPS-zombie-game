using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kays : MonoBehaviour
{
    public enum KeyType { Blue, Black, Red}
    public KeyType keyType;

    private void OnDestroy()
    {
        switch (keyType)
        {
            case KeyType.Blue:
                GlobalVariables.hasBlueKey = true;
                break;
            case KeyType.Black:
                GlobalVariables.hasBlackKey = true;
                break;
            case KeyType.Red:
                GlobalVariables.hasBlackKey = true;
                break;
            default:
                break;
        }
    }
}

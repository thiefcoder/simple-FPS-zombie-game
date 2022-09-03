using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doorscript : MonoBehaviour
{
    Animator animator;
    AudioSource doorAS;
    public AudioClip openAC, closeAC,lockdoor;
    public bool isOpen;
    public bool isLocked;
    [SerializeField]
    Text doorText;
    string doorString;
    public enum LockType { Blue, Black, Red}
    public LockType lockType;

    private void Start()
    {
        doorText.enabled = false;
        doorAS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isLocked && !isOpen)
            {
                doorString = "Open Door";
                UpdateDoorUI();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    opendoor();
                }
            }
            else if(isOpen)
            {
                doorString = "Close Door";
                UpdateDoorUI();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    CloseDoor();
                }
            }
            else if (isLocked && !isOpen)
            {
                doorString = "Door is LoCked";
                UpdateDoorUI();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    doorAS.PlayOneShot(lockdoor);
                    CheckLockType();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            doorText.enabled = false;
        }
    }

    public void opendoor()
    {
        isOpen = true;
        doorText.enabled=false;
        animator.SetTrigger("open");
        doorAS.PlayOneShot(openAC);
    }

    void CloseDoor()
    {
        isOpen = false;
        animator.SetTrigger("close");
        doorAS.PlayOneShot(closeAC);
    }

    void UpdateDoorUI()
    {
        doorText.enabled = true;
        doorText.text = doorString.ToString();
    }

    void CheckLockType()
    {
        switch (lockType)
        {
            case LockType.Blue:
                if (GlobalVariables.hasBlueKey)
                {
                    isLocked = false;
                    opendoor();
                }
                break;
            case LockType.Black:
                if (GlobalVariables.hasBlackKey)
                {
                    isLocked = false;
                    opendoor();
                }
                break;
            case LockType.Red:
                if (GlobalVariables.hasRedKey)
                {
                    isLocked = false;
                    opendoor();
                }
                break;
            default:
                break;
        }
    }
}

using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioSource shell;

    void Start()
    {

        shell = GetComponent<AudioSource>();
        shell.Play();
    }
}

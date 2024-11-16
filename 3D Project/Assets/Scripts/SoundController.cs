using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            audioSource.Play();
        }    
        if(Input.GetKeyDown(KeyCode.E)){
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}

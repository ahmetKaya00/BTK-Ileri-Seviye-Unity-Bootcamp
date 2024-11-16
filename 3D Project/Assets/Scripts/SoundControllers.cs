using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllers : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public Transform kamera;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        kamera = Camera.main.transform;
    }

    void Update()
    {
        float mesafe = Vector3.Distance(transform.position, kamera.position);
        float maxMesafe  = 10f;
        float normalizedMesafe = Mathf.Clamp01(mesafe / maxMesafe);
        audioSource.volume = 1 - normalizedMesafe;
    }
}

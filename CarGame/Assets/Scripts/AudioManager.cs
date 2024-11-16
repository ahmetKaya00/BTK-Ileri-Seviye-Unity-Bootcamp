using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource bgMusicSource;
    public AudioSource[] sfxSource;

    private float bgMusicVolume = 1.0f;
    private float sfxSourceVolume = 1.0f;
    private float bgMusicTime = 0f;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += onSceneLoaded;
        }
        else{
            Destroy(gameObject);
            return;
        }
        LoadVolumeSettings();
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode){
        AssignAudioSource();

        if(bgMusicSource != null){
            bgMusicSource.time = bgMusicTime;
            if(!bgMusicSource.isPlaying){
                bgMusicSource.Play();
            }
        }
        ApplyVolumeSettings();
    }

    public void AssignAudioSource(){
        bgMusicSource = GameObject.Find("bg")?.GetComponent<AudioSource>();

        if(bgMusicSource != null){
            bgMusicSource.loop = true;
        }

        AudioSource[] allAudioSource = FindObjectsOfType<AudioSource>();
        sfxSource = System.Array.FindAll(allAudioSource, source => source != bgMusicSource);
    }


    private void LoadVolumeSettings(){
        bgMusicVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1.0f);
        sfxSourceVolume = PlayerPrefs.GetFloat("SFXVolume",1.0f);
    }

    private void ApplyVolumeSettings(){
        if(bgMusicSource != null){
            bgMusicSource.volume = bgMusicVolume;
        }

        foreach(var sfx in sfxSource){
            if(sfx != null){
                sfx.volume = sfxSourceVolume;
            }
        }
    }

    public void SetBGMusicVolume(float volume){
        bgMusicVolume = volume;
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        ApplyVolumeSettings();
    }
    public void SetSFXVolume(float volume){
        sfxSourceVolume = volume;
        PlayerPrefs.SetFloat ("SFXVolume", sfxSourceVolume);
        ApplyVolumeSettings();
    }

    private void OnApplicationQuit() {
        PlayerPrefs.SetFloat("bgMusicVolume", bgMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume",sfxSourceVolume);
        PlayerPrefs.Save();
    }


}

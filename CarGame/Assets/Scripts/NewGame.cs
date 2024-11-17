using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void GamePlay(){
        float bgMusicVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1.0f);
        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume",1.0f);

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume",SFXVolume);

        PlayerPrefs.Save();
        SceneManager.LoadScene("Selector");
    }
}

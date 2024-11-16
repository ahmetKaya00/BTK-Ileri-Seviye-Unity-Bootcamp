using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [SerializeField] float waitTime = 4f;
    void Start()
    {
        StartCoroutine(WaitAndMainMenu());
    }

    private IEnumerator WaitAndMainMenu(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainMenu");
    }
}
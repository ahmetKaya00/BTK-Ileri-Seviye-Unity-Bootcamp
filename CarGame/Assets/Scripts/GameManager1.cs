using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform spawnPoint;

    public GameObject selectedCar;

    private void Start(){
        string selectedCarPrefabName = PlayerPrefs.GetString("SelectedCarPrefab");

        GameObject carPrefab = Resources.Load<GameObject>(selectedCarPrefabName);

        if(carPrefab != null){
            selectedCar = Instantiate(carPrefab, spawnPoint.position,spawnPoint.rotation);
            virtualCamera.Follow = selectedCar.transform;
            virtualCamera.LookAt = selectedCar.transform;
        }
    }
}

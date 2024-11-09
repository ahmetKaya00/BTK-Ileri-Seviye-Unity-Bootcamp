using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllers : MonoBehaviour
{
    [SerializeField] private List<Camera> cameras; 
    void Start()
    {
        if(cameras == null || cameras.Count == 0){
            Debug.Log("cameras is not empty");
            return;
        } 
        ActiveCamera(0);
    }

    void Update()
    {
        for(int i = 0; i < cameras.Count; i++){
            if(Input.GetKeyDown(KeyCode.Alpha1 + i)){
                ActiveCamera(i);
            }
        }
    }

    void ActiveCamera(int camerasIndex){
        if(camerasIndex < 0 || cameras.Count == 0){
            Debug.Log("Invalid camera");
            return;
        }
        for(int i = 0; i < cameras.Count; i++) {
            cameras[i].gameObject.SetActive(i==camerasIndex);
        }
    }
}

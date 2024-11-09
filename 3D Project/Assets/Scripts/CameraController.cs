using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Camera camera1, camera2,camera3;

    void Start(){
        ActiveCamera(camera1);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            ActiveCamera(camera1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            ActiveCamera(camera2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            ActiveCamera(camera3);
        }
    }

    void ActiveCamera(Camera camera){
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);

        camera.gameObject.SetActive(true);

    }
}

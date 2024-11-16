using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
     
public class ToogleFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public GameObject player1, player2;

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            if(virtualCamera.Follow == player1.transform){
                virtualCamera.Follow = player2.transform;
            }
            else if(virtualCamera.Follow == player2.transform){
                virtualCamera.Follow = player1.transform;
            }
        }
    }
}

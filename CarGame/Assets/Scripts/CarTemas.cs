using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTemas : MonoBehaviour
{
    public bool hasFinished = false;
    public float finishTime;
    public Rigidbody rb;

    public void Start(){
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("FinishedLine") && !hasFinished){
            hasFinished = true;
            finishTime = Time.timeSinceLevelLoad;
            RaceManager.instance.CarFinished(this);
        }
    }
}

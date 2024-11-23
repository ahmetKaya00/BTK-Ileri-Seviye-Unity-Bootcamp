using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public Transform[] wayPoints;
    public Transform[] whells;
    public WheelCollider[] wheelColliders;

    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;

    public float lookHeadDistance = 5f;

    private int currentWayInd = 0;
    private Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate(){
        Drive();
        CheckWayPoints();
        UpdateWhellPos();
    }

    public void Drive(){
        Vector3 relativeVector = transform.InverseTransformPoint(wayPoints[currentWayInd].position);

        float newSteer = Mathf.Clamp((relativeVector.x / relativeVector.magnitude) * maxSteeringAngle, -maxSteeringAngle,maxSteeringAngle);

        for(int i = 0; i < wheelColliders.Length; i++){
            if(wheelColliders[i].transform.localPosition.z > 0){
                wheelColliders[i].steerAngle = newSteer;
            }
        }
        foreach(WheelCollider wheel in wheelColliders){
            wheel.motorTorque = maxMotorTorque;
        }
    }

    public void CheckWayPoints(){
        float distance = Vector3.Distance(transform.position, wayPoints[currentWayInd].position);
        if(distance < lookHeadDistance){
            currentWayInd = (currentWayInd + 1) % wayPoints.Length;
        }
    }

    public void UpdateWhellPos(){
        for(int i = 0;i<wheelColliders.Length;i++){
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);

            whells[i].position = pos;
            whells[i].rotation = quat;
        }
    }
}

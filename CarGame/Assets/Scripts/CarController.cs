using System.Collections;
using System.Collections.Generic;
using SimpleInputNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    public Transform frontLeftWhell, frontRightWhell;
    public Transform rearLeftWhell, rearRightWhell;

    public WheelCollider frontLeftWhellCollider, frontRightWhellCollider;
    public WheelCollider rearLeftWhellCollider, rearRightWhellCollider;

    public float maxSteerAngle = 45f;
    public float motorForce =  1500f;
    public float brakeForce = 3000f;
    public float decelarationForce = 100f;
    public float reverseForce = 1000f;
    public float stopThreshold = 1f;
    public float flipThresholdAngle = 45f;

    private float currentSteerAngle;
    private float currentAcceleration;
    private float currentBrakeForce;
    private bool isReversing = false;
    private bool isCheckingFlip = false;
    private float flipCheckTime = 4f;

    public SteeringWheel steeringWheel;

    private Rigidbody carRigidbody;
    private Renderer carRenderer;

    private void Start(){
        carRigidbody = GetComponent<Rigidbody>();
        carRenderer = GetComponent<Renderer>();

        FindSteeringWhell();
        FindButtonAndAssignEventTrigger();

        carRigidbody.centerOfMass = new Vector3(0, -0.5f,0);

    }

    private void Update(){
        if(steeringWheel != null){
            currentSteerAngle = maxSteerAngle * steeringWheel.Value;
        }
        ApplyMovement();
        UpdateWhellPoses();
        CheckFlip();
    }

    public void CheckFlip(){
        if(Vector3.Angle(Vector3.up, transform.up)>flipThresholdAngle){
            if(!isCheckingFlip){
                isCheckingFlip = true;
                Invoke("CheckIfStillFlipped",flipCheckTime);
            }
        }else{
            isCheckingFlip = false;
            CancelInvoke("CheckIfStillFlipped");
        }
    }
    public void CheckIfStillFlipped(){
        if(isCheckingFlip){
            StartRespawnProcces();
        }
    }
    public void StartRespawnProcces(){
        carRenderer.enabled = false;
        Invoke("Respawn",2f);
    }
    private void Respawn(){
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        transform.rotation = Quaternion.identity;
        carRigidbody.velocity = Vector3.zero;
        carRigidbody.angularVelocity = Vector3.zero;

        carRenderer.enabled = true;
        isCheckingFlip = false;
    }
    private void ApplyMovement(){
        frontLeftWhellCollider.steerAngle = currentSteerAngle;
        frontRightWhellCollider.steerAngle = currentSteerAngle;

        if(currentAcceleration == 0f && currentBrakeForce == 0f){
            rearLeftWhellCollider.brakeTorque = decelarationForce;
            rearRightWhellCollider.brakeTorque = decelarationForce;
        }
        else{
            rearLeftWhellCollider.brakeTorque = currentBrakeForce;
            rearRightWhellCollider.brakeTorque = currentBrakeForce;
        }
        rearLeftWhellCollider.motorTorque = currentAcceleration;
        rearRightWhellCollider.motorTorque = currentAcceleration;
    }

    private void FindSteeringWhell() {
        GameObject steeringWhellObj = GameObject.Find("SteeringWheel");
        if(steeringWhellObj != null){
            steeringWheel = steeringWhellObj.GetComponent<SteeringWheel>();
        }
    }
    private void FindButtonAndAssignEventTrigger(){
        GameObject gasButton = GameObject.Find("Gas");
        if(gasButton != null){
            AddEventTriggers(gasButton, GasOn,BrakeOn);
        }
        GameObject reverseButton = GameObject.Find("Reverse");
        if(gasButton != null){
            AddEventTriggers(reverseButton, ReverseOn,BrakeOn);
        }
        GameObject brakeButton = GameObject.Find("Brake");
        if(gasButton != null){
            AddEventTriggerToPointerDown(brakeButton,BrakeOn);
        }
    }
    private void AddEventTriggers(GameObject buttonObj, UnityEngine.Events.UnityAction pointerDownAction, UnityEngine.Events.UnityAction pointerUpAction) {
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
        if(trigger == null){
            trigger = buttonObj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry{
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((eventData) => {pointerDownAction();});
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry{
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((eventData) => {pointerUpAction();});
        trigger.triggers.Add(pointerUpEntry);
    }
    private void AddEventTriggerToPointerDown(GameObject buttonObj, UnityEngine.Events.UnityAction pointerDownAction){
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
        if(trigger == null){
            trigger = buttonObj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry{
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((eventData) => {pointerDownAction();});
        trigger.triggers.Add(pointerDownEntry);
    }

    public void GasOn(){
        if(isReversing){
            currentAcceleration = 0;
            currentBrakeForce = brakeForce;
            if(carRigidbody.velocity.magnitude < stopThreshold){
                isReversing = false;
                currentBrakeForce = 0f;
                currentAcceleration += motorForce;
            }
        }else{
                currentBrakeForce = 0f;
                currentAcceleration = motorForce;
        }
    }
    
    public void BrakeOn(){
        currentAcceleration = 0f;
        currentBrakeForce = brakeForce;
    }
    public void ReverseOn(){
        if(!isReversing){
            currentAcceleration = 0;
            currentBrakeForce = brakeForce;
            if(carRigidbody.velocity.magnitude < stopThreshold){
                isReversing = true;
                currentBrakeForce = 0f;
                currentAcceleration = -reverseForce;
            }
        }
    }
    private void UpdateWhellPoses(){
        UpdateWhellPose(frontLeftWhellCollider, frontLeftWhell);
        UpdateWhellPose(frontRightWhellCollider, frontRightWhell);
        UpdateWhellPose(rearLeftWhellCollider, rearLeftWhell);
        UpdateWhellPose(rearRightWhellCollider, rearRightWhell);
    }
    private void UpdateWhellPose(WheelCollider collider, Transform transform){
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}

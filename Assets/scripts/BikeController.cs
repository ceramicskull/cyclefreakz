using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;

public class BikeController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public GameObject hand;
    public GameObject joyStick;
    public AudioSource motorSound;
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    private float motor;
    private float steering;
    private float initHandForward;
    private float initHandSteer;
    private bool start;
    private bool driving;
    public Vector3 com;
    private Rigidbody rb;
    public void Start(){
        SteamVR.Initialize(true);
        start = false;
        motor = 0;
        steering = 0;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }
    public void FixedUpdate()
    {
        if(driving && !start){
            start = true;
            initHandForward = hand.transform.localPosition.z;
            initHandSteer = hand.transform.localPosition.x;

        }
        else if (!driving){
            start = false;
            motor = 0;
            joyStick.SendMessage("neutral");
            joyStick.SendMessage("straight");
            steering = 0;
        }
        else if(start){
        Debug.Log("driving...");
        if(hand.transform.localPosition.z - initHandForward > 0.05f){
        motor = maxMotorTorque;
        motorSound.pitch += 0.001f;
        if(motorSound.pitch >= 2.0f){
            motorSound.pitch = 2.0f;
        }
        joyStick.SendMessage("forward");
        }
        else if(hand.transform.localPosition.z - initHandForward < -0.10f){
        motor = -maxMotorTorque;
        motorSound.pitch += 0.001f;
        if(motorSound.pitch >= 2.0f){
            motorSound.pitch = 2.0f;
        }
        joyStick.SendMessage("backward");

        }
        else{
        motor = 0;
        motorSound.pitch = 1.0f;
        joyStick.SendMessage("neutral");
        }
        
        if(hand.transform.localPosition.x - initHandSteer > 0.15f){
        steering = maxSteeringAngle;
        joyStick.SendMessage("left");
        }
        else if(hand.transform.localPosition.x - initHandSteer < -0.15f){
        steering = -maxSteeringAngle;
        joyStick.SendMessage("right");
        }
        else{
        steering = 0;
        joyStick.SendMessage("straight");
        }
        }
        SendMessage("SendTorque", motor);
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.wheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.wheel.motorTorque = motor;

            }
        }
    }
    void Driving(){
            driving = true;
            Debug.Log("driving true!");
    }
    void NotDriving(){
            driving = false;
            Debug.Log("driving false!");
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider wheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

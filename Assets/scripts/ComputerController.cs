using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;

public class ComputerController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public GameObject hand;
    public GameObject ball;
    private GameObject whereToAim;
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public GameObject goal;
    private float motor;
    private bool collide;
    private float steering;
    private Vector3 ogPos;
    private float initHandForward;
    private float initHandSteer;
    private bool driving;
    private float count;
    private int bulletCount;
    public AudioSource bulletSound;
    public GameObject[] endZones;
    private float reverseTimer;
    private float bulletTimer;
    private float bulletClock;
    private int hitCount;
    public bool allowedtoShoot;
    public bool reverseTimerTrue;
    public bool isRed;
    //positions 1, 2 (2 representing offense or defense) or center (1)
    public int position;

    public GameObject[] bullets;
    public GameObject[] enemies;
    System.Random rnd;
    public void Start(){
        rnd = new System.Random();
        SteamVR.Initialize(true);
        motor = 0;
        steering = 0;
        count = 0.0f;
        bulletCount = 0;
        reverseTimer = 0.0f;
        bulletTimer = 0.0f;
        bulletClock = rnd.Next(1,10);
        whereToAim = new GameObject();
        allowedtoShoot = false;
        reverseTimerTrue = false;
    }
    public void FixedUpdate()
    {
        
        Transform tempComp = transform;
        float ballX = ball.transform.position.x;
        float ballZ = ball.transform.position.z;
        float slope = (goal.transform.position.z - ballZ)/(goal.transform.position.x - ballX);
        float bPos = -slope*ballX + ballZ;
        float posX;
        float posZ;
        posZ = ballZ+2.5f;
        posX = (posZ - bPos)/slope;
        
        whereToAim.transform.position = new Vector3(posX, 0.0f, posZ);
        if(position == 1){
        tempComp.LookAt(whereToAim.transform);
        }
        if(position == 2){
        if(reverseTimer == 0.0f){
            int wheretoDrive = rnd.Next(0,4);
            if(wheretoDrive <= 2){
                tempComp.LookAt(endZones[wheretoDrive].transform);
            }
            else{
                tempComp.LookAt(whereToAim.transform);
            }
        }
        }
        if(reverseTimer == 0.0f){
            motor = maxMotorTorque;
            ogPos = transform.position;
    
        }
        
        else if(reverseTimer >= 2 && reverseTimer <= 3 & reverseTimerTrue){
            if((transform.position.x - ogPos.x < 0.3f && transform.position.x - ogPos.x > -0.3f)){
                motor = -maxMotorTorque;
            }
        }
        
        
        

        if(bulletTimer >= bulletClock && bulletTimer <= bulletClock + 1.0f  && allowedtoShoot) {   
            GameObject target = enemies[rnd.Next(0, enemies.Length - 1)];
            if(target.name == "ball"){
                bullets[bulletCount].SetActive(true);
		        bullets[bulletCount].transform.position = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
			    bullets[bulletCount].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bullets[bulletCount].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                bullets[bulletCount].transform.LookAt(whereToAim.transform);
			    bullets[bulletCount].GetComponent<Rigidbody>().AddForce(bullets[bulletCount].transform.forward * 75, ForceMode.Impulse);
                bulletSound.Play();
                bulletCount ++;
            }
            else{
                bullets[bulletCount].SetActive(true);
		        bullets[bulletCount].transform.position = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
			    bullets[bulletCount].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bullets[bulletCount].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                bullets[bulletCount].transform.LookAt(target.transform);
			    bullets[bulletCount].GetComponent<Rigidbody>().AddForce(bullets[bulletCount].transform.forward * 75, ForceMode.Impulse);
                bulletSound.Play();
                bulletCount ++;
            }
            if(bulletCount >= bullets.Length){
                allowedtoShoot = false;
            } 
        }
        reverseTimer += Time.deltaTime;
        bulletTimer += Time.deltaTime;
        if(reverseTimer > 7){
            reverseTimer = 0.0f;
        }
        if (bulletTimer > 10.0f){
            bulletTimer = 0.0f;
            if(bulletCount >= bullets.Length){
                allowedtoShoot = true;
                bulletCount = 0;
            }
        }
              
        transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y +(tempComp.localEulerAngles.y - transform.localEulerAngles.y)/8, transform.localEulerAngles.z);
        
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

    
}

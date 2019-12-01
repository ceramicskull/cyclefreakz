using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RespawnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 orgRotate;
    private Vector3 orgPos;
    private Vector3 curPos;
    private int count;
    private float torque;
    public SteamVR_Fade fade;
    public bool isRed;
    public bool isPlayerOne;
    public GameObject ball;
    public GameObject player;
    public GameObject HealthBar;
    public GameObject explosion;
    public AudioSource explosionSound;
    public AudioSource endgame;
    public float timer;
    private int hitCount;
    void Start()
    {
        orgRotate = transform.eulerAngles;
        orgPos = transform.position;
        hitCount = 0;
        explosion.SetActive(false);
        timer = 0.0f;

    }

    void Update(){
        
        if((transform.eulerAngles.x >= 90 && transform.eulerAngles.x <= 270) || 
        (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270) || (torque >= 100.0f && transform.position == curPos && count == 5)){
            Respawn();
        }

        if(count == 10){
        curPos = transform.position;
        count = 0;
        }
        count ++;
        timer += Time.deltaTime;
        if(timer > 4.0f){
            explosion.SetActive(false);
        }

    }
    void SendTorque(float t){
        torque = t;
    }
    void Respawn(){
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        timer = 0.0f;
        explosion.GetComponent<ParticleSystem>().Play();
        if(!endgame.isPlaying){
            explosionSound.Play();
        }
        if(isPlayerOne){
        SteamVR_Fade.Start(Color.black, 0);
        }
        transform.eulerAngles = orgRotate;
        transform.position = orgPos;
        transform.LookAt(ball.transform);
        if(player != null)
            player.transform.eulerAngles = transform.eulerAngles;
        transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y,transform.localEulerAngles.z);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if(player != null){
            player.gameObject.SendMessage("Respawn");
        }
        HealthBar.transform.localScale = new Vector3(0.8f,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
		SteamVR_Fade.Start(Color.clear, 1);
        
    }
    void Respawn2(){
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        timer = 0.0f;
        explosion.GetComponent<ParticleSystem>().Play();
        if(isPlayerOne){
        SteamVR_Fade.Start(Color.black, 0);
        }
        transform.eulerAngles = orgRotate;
        transform.position = orgPos;
        transform.LookAt(ball.transform);
        if(player != null)
            player.transform.eulerAngles = transform.eulerAngles;
        transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y,transform.localEulerAngles.z);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if(player != null){
            player.gameObject.SendMessage("Respawn");
        }
        HealthBar.transform.localScale = new Vector3(0.8f,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
		SteamVR_Fade.Start(Color.clear, 1);
        
    }
    private void Hit(){
        hitCount ++;
        
           
        HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x * 0.25f,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);

        if(hitCount > 4){
            Respawn();
            hitCount = 0;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(isRed){
            if( other.gameObject.CompareTag("redprojectile")){
            Hit();
            Debug.Log("hit enemy");
            other.gameObject.SetActive(false);
           }
       }
       else{
           if(other.gameObject.CompareTag("greenprojectile")){
            Hit();
            Debug.Log("hit enemy");
            other.gameObject.SetActive(false);
           }
       }
    }

}

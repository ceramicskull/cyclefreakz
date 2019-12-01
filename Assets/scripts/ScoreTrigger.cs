using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTrigger : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    public GameObject[] players = new GameObject[6];
    public AudioSource redgoal;
    public AudioSource greengoal;
    public Text counter1;
    public Text counter11;
    public Text counter2;
    public Text counter22;
    public Text timer;
    public Text timer2;
    private int score1;
    private int score2;
    public float explosionTimer;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
        startPos = transform.position;
        startRot = transform.rotation;
        counter1.text = score1.ToString();
        counter2.text = score2.ToString();
        counter11.text = counter1.text;
        counter22.text = counter2.text;
        explosion.SetActive(false);
        explosionTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < - 10 || transform.position.y > 100 || transform.position.z < - 100 || transform.position.z > 100 ||
        transform.position.x < - 100 || transform.position.x > 100){
            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        explosionTimer += Time.deltaTime;
        if(explosionTimer > 4.0f){
            explosion.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // team 1 is red team 2 is green
        if(other.CompareTag("goal team 1")){
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            explosion.GetComponent<ParticleSystem>().Play();
            explosionTimer = 0.0f;
            score1++;
            greengoal.Play();
            counter1.text = score1.ToString();
            counter11.text = score1.ToString();
            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            for(int i = 0; i <= 5; i++){
            players[i].gameObject.SendMessage("Respawn2");
            }
            
            
        }
        if (other.CompareTag("goal team 2"))
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            explosion.GetComponent<ParticleSystem>().Play();
            explosionTimer = 0.0f;
            score2 ++;
            redgoal.Play();
            counter2.text = score2.ToString();
            counter22.text = score2.ToString();
            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            for(int i = 0; i <= 5; i++){
            players[i].gameObject.SendMessage("Respawn2");
            }
        }
    }
    private void GameOver(){
            if(score2 > score1){
                timer.text = "Green Team Wins!";
                timer2.text = "Green Team Wins!";
            }
            if(score1 > score2){
                timer.text = "Red Team Wins!";
                timer2.text = "Red Team Wins!";
            }
            if(score1 == score2){
                timer.text = "Tie Game!";
                timer2.text = "Tie Game!";
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startController : MonoBehaviour {
	public AudioSource loop;
	public AudioSource endgame;
	// Use this for initialization
	public GameObject[] computers;
	public GameObject player;
	public GameObject hand;
	public GameObject hand2;
	public GameObject ball;
	public int gameTime;
	public Text timer;
	public Text timer2;
	private int time;
	private float second;
	private bool gameOver;
	private int countdownTimer;
	public AudioSource countdown;
	void Awake() {
     QualitySettings.vSyncCount = 1;
     Application.targetFrameRate = 60;
	 }
	void Start () {
		countdownTimer = 11;
		second = 0.0f;
		time = 20;
		GetComponent<AudioSource>().Play();
		for(int i = 0; i < computers.Length; i++){
			computers[i].GetComponent<Rigidbody>().isKinematic = true;
			computers[i].GetComponent<ComputerController>().enabled = false;
		}
		player.GetComponent<Rigidbody>().isKinematic = true;
		ball.GetComponent<Rigidbody>().isKinematic = true;
		gameOver = false;

	}
	
	// Update is called once per frame
	void Update () {
		
		if(!GetComponent<AudioSource>().isPlaying && !loop.isPlaying && !endgame.isPlaying){
				for(int i = 0; i < computers.Length; i++){
				computers[i].GetComponent<Rigidbody>().isKinematic = false;
				computers[i].GetComponent<ComputerController>().allowedtoShoot = true;
				computers[i].GetComponent<ComputerController>().reverseTimerTrue = true;
				computers[i].GetComponent<ComputerController>().enabled = true;
				}
				player.GetComponent<Rigidbody>().isKinematic = false;
				hand.GetComponent<Shooter>().allowedtoShoot = true;
				hand2.GetComponent<Joystick>().enabled = true;
				ball.GetComponent<Rigidbody>().isKinematic = false;
				loop.Play();
				time = gameTime;
		}
		int remainder;
		int quotient = System.Math.DivRem(time, 60, out remainder);
		Debug.Log(time);
		if(remainder < 10 && !gameOver){
		timer.text = quotient.ToString() + ":0" + remainder.ToString();
		timer2.text =  quotient.ToString() + ":0" + remainder.ToString();
		}
		else if (!gameOver){
		timer.text = quotient.ToString() + ":" + remainder.ToString();
		timer2.text =  quotient.ToString() + ":" + remainder.ToString();
		}
		
		if(time < countdownTimer && time >= 0 && loop.isPlaying){
			countdown.Play();
			countdownTimer --;
		}
		if(time <= 0 && loop.isPlaying){
			for(int i = 0; i < computers.Length; i++){
				computers[i].GetComponent<Rigidbody>().isKinematic = true;
				computers[i].GetComponent<ComputerController>().enabled = false;
				}
				player.GetComponent<Rigidbody>().isKinematic = true;
				ball.GetComponent<Rigidbody>().isKinematic = true;
				ball.gameObject.SendMessage("GameOver");
				hand.GetComponent<Shooter>().allowedtoShoot = false;
				gameOver = true;
				loop.Stop();
				endgame.Play();
		}
		
		if(second >= 0.89f){
			time --;
			second = 0.0f;
		}
		else{
		second += Time.deltaTime;
		}
	}
}

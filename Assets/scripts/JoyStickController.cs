using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void forward(){
		transform.localEulerAngles = new Vector3(25.0f,transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
	void backward(){
		transform.localEulerAngles = new Vector3(-25.0f,transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
	void neutral(){
		transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
	void straight(){
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y, 0.0f);
	}
	void left(){
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y, -25.0f);
	}
	void right(){
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y, 25.0f);
	}
}

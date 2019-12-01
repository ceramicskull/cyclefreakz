using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizoncontroller : MonoBehaviour {

	// Use this for initialization
	public GameObject car;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.eulerAngles = new Vector3(0.0f, car.transform.eulerAngles.y , 0.0f);
	}
	void Respawn(){
		this.transform.eulerAngles = new Vector3(0.0f, car.transform.eulerAngles.y , 0.0f);
	}
}

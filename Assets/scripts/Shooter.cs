using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
public class Shooter : MonoBehaviour {

		public SteamVR_Action_Boolean stickAction;

        public Hand hand;
		private bool shooting;
		int count;
		public GameObject[] bullets;
		public GameObject gun;
		public AudioSource bulletSound;
		public bool allowedtoShoot;
        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (stickAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No stick Action assigned");
                return;
            }

            stickAction.AddOnChangeListener(OnstickActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (stickAction != null)
                stickAction.RemoveOnChangeListener(OnstickActionChange, hand.handType);
        }

        private void OnstickActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                shooting = true;
				hand.TriggerHapticPulse(3000);
                
            }
			else{
				shooting = false;
			}
        }
		void Start(){
			count = 0;
			allowedtoShoot = false;
		}
		void Update(){
			if(shooting && allowedtoShoot){
				bulletSound.Play();
				bullets[count].SetActive(true);
				bullets[count].transform.position = gun.transform.position;
				bullets[count].GetComponent<Rigidbody>().velocity = Vector3.zero;
				bullets[count].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				bullets[count].GetComponent<Rigidbody>().AddForce(gun.transform.forward * 75, ForceMode.Impulse);
        // Does the ray intersect any objects excluding the player layer
				shooting = false;
			}
			count ++;
			
			if(count > bullets.Length){
				count = 0;
			}
			

		}
}

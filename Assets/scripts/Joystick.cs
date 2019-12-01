using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
public class Joystick : MonoBehaviour {

		public SteamVR_Action_Boolean stickAction;

        public Hand hand;
		public GameObject motorcycle;
        
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
                hand.TriggerHapticPulse(3000);
                motorcycle.SendMessage("Driving");
                
            }
			else{
				motorcycle.SendMessage("NotDriving");
			}
        }
}

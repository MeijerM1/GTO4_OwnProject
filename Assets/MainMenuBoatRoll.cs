using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBoatRoll : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		float rollAngle = Mathf.Sin(Time.time) * 5; 
		float pitchAngle = Mathf.Sin(Time.time) * 2; 
			
		
		transform.rotation = Quaternion.AngleAxis(rollAngle, Vector3.back);
		
	}
}

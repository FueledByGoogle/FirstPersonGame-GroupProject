using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivator : MonoBehaviour {

	public UnityEngine.UI.Text pressR; //The button prompt text. If it is a child of the camera, it should show up and follow with the camera
	
	public UnityEngine.UI.Text reading1;

	public Collider fieldOfReference; 
	
	private Rigidbody playerBody;
	
	bool finishedReading;


	// Use this for initialization
	void Start () {
		playerBody = GetComponent<Rigidbody> ();
		finishedReading = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.R) && pressR.enabled == true) { 
			pressR.enabled = false;
			playerBody.isKinematic = true;
			fieldOfReference.isTrigger = false;
			finishedReading = false;
		}
		
		if(finishedReading == false){
			reading1.enabled = true;
			if (Input.GetKey (KeyCode.T)) {
			finishedReading = true;
			}
		}
		
		if(finishedReading == true){
			playerBody.isKinematic = false;
			fieldOfReference.isTrigger = true;
			reading1.enabled = false;
		}
		
	}
	
	
	void OnTriggerEnter(Collider coll){
		
		//The following if statement enables the button prompt once in the trigger range of an NPC
		if (coll.gameObject.CompareTag("NPC")){
			pressR.enabled = true;
			//coll.isTrigger = false;
		}
		//Required: TriggerActivator.cs on mainCharacter
		//coll or collider (sphere collider perferably) is what the player enters
		//If col has the tag NPC, button prompt appears
		
	}	
	
	
	//void OnTriggerStay(Collider coll){
		
		//The following if statement enables the button prompt once in the trigger of an NPC
		//if (coll.gameObject.CompareTag("NPC")){
		//	pressR.enabled = true;
		//}
		//Required: TriggerActivator.cs on mainCharacter
		//coll or collider (sphere collider perferably) is where the player is in (or inside triggering object)
		//If col has the tag NPC, button prompt appears
	//}
	
	void OnTriggerExit(Collider coll){
		
		//The following if statement DISABLES the button prompt once leaving the trigger of an NPC
		if (coll.gameObject.CompareTag("NPC")){
			pressR.enabled = false;
		}
		//Required: TriggerActivator.cs on mainCharacter
		//coll or collider (sphere collider perferably) is where the player leaves
		//If col has the tag NPC, button prompt disappears
		
	}

}

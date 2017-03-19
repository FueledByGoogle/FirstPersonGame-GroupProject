using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScript : MonoBehaviour {


	public UnityEngine.UI.Text pressR;
	
	public UnityEngine.UI.Text reading1;
	public UnityEngine.UI.Text reading2;
	public UnityEngine.UI.Text reading3;
	
	public Rigidbody characterRigidBody;
	
	bool isReadingThis;
	Vector3 holdPlace;
	int numberOfTextBoxes;

	// Use this for initialization
	void Start () {
		isReadingThis = false;
		holdPlace = characterRigidBody.transform.position;
		numberOfTextBoxes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isReadingThis == true){
			characterRigidBody.transform.position = holdPlace;
			if(numberOfTextBoxes == 0){
				reading1.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyUp (KeyCode.R) && numberOfTextBoxes == 1){
				reading1.enabled = false;
				reading2.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyUp (KeyCode.R) && numberOfTextBoxes == 2){
				reading2.enabled = false;
				reading3.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyUp (KeyCode.R) && numberOfTextBoxes == 3){
				reading3.enabled = false;
				isReadingThis = false;
				numberOfTextBoxes = 0;
			}
		}
		
		if (Input.GetKeyUp (KeyCode.R) && pressR.enabled == true) { 
			pressR.enabled = false;
			isReadingThis = true;
			holdPlace = characterRigidBody.transform.position;
		}
		
		
	}
	
	
	void OnTriggerEnter(Collider coll){
		
		//The following if statement enables the button prompt once in the trigger range of an NPC
		if (coll.gameObject.CompareTag("Player")){
			pressR.enabled = true;
			//coll.isTrigger = false;
		}
		//Required: TriggerActivator.cs on mainCharacter
		//coll or collider (sphere collider perferably) is what the player enters
		//If col has the tag NPC, button prompt appears
		
	}
	
	void OnTriggerExit(Collider coll){
		
		//The following if statement DISABLES the button prompt once leaving the trigger of an NPC
		if (coll.gameObject.CompareTag("Player")){
			pressR.enabled = false;
		}
		//Required: TriggerActivator.cs on mainCharacter
		//coll or collider (sphere collider perferably) is where the player leaves
		//If col has the tag NPC, button prompt disappears
		
	}
}

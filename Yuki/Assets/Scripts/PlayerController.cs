using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public Character character;
	public Camera ThirdPerson;
	public Camera FirstPerson;
	public Animation Animations;
	public GameObject PlayerGroupLocator;			//We will need to attach the grouplocater to this, so we can disable it when using the bow
	public const float strafeSpeed = 2f;

	public Bow bow;
	public GameObject Archery;						//Used to hide archery related elements when not using the bow
	public GameObject fakeArrow;					//Fake arrow only displayed when player clicks Fire and bow begins to draw
	public bool usingBow;
	public bool arrowDrawn;
	public SkinnedMeshRenderer playerMeshRenderer;	//Used to disable render of player so camera doesn't see player when using bow

	void Start () {
		character = GetComponent<Character> ();
		Animations = GetComponent<Animation> ();

		//Bow related
		ThirdPerson.enabled = true;
		FirstPerson.enabled = false;
		usingBow = false;
		Archery.SetActive (false);

		Cursor.lockState = CursorLockMode.Locked;	//Remove cursor from player view
	}

	void Update () {
		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	void FixedUpdate () {
		PlayerControl ();

	}

	private void PlayerControl (){
		
		if (usingBow == true) {
			playerMeshRenderer.enabled = false;		//render of player has to be disabled in order to see the bow and have no playerbody showing through
			PlayerGroupLocator.SetActive (false);
		} else {
			PlayerGroupLocator.SetActive (true);
			playerMeshRenderer.enabled = true;
		}

		if (usingBow == false) {					

			Archery.SetActive (false);

			if (Input.GetMouseButton (1)) {
				
				ThirdPerson.enabled = false;		//switching between cameras
				FirstPerson.enabled = true;
				//usingBow = true;

			} else {
				
				if (ThirdPerson.enabled != true) {	//This check probably doesn't matter but skips setting FirstPerson camera to enabled each time
					ThirdPerson.enabled = true;
					FirstPerson.enabled = false;
				}

			}

		} else if (usingBow == true) {				
			Bow ();
		}
			
		if (Input.GetMouseButton (0) && usingBow == false) {
			/*	TODO: Sword is only trigger when player left clicking
			 */
			Animations.Play ("Attack");
		}

		Movement ();
	}


	void Bow() {

		playerMeshRenderer.enabled = false;		//Don't render playerbody when using bow, so we can make bow closer to the camera view without seeing it in first person
		Archery.SetActive (true);

		if (arrowDrawn == false && bow.stretching == false) {	//This prevents an arrow from appearing on initial active of bow
			fakeArrow.SetActive (false);
		}

		//Go back to using melee
		if (Input.GetKeyDown("1")) {
			ThirdPerson.enabled = true;
			FirstPerson.enabled = false;
			usingBow = false;
		}

		//Shooting
		if (Input.GetMouseButton (0)) {			
			//When arrow is not drawn and player wants to draw arrow, display the fakeArrow
			if (arrowDrawn == false && bow.releasing == false) {
				fakeArrow.SetActive (false);
				arrowDrawn = true;
			}
			//Draw until bow becomes ready
			if (bow.bowReady == false) {
				fakeArrow.SetActive (true);
				bow.Stretch ();
			}
			//Bow ready to fire
			if (bow.stretching == true && bow.bowReady == true && arrowDrawn == true) {
				bow.Fire ();
				fakeArrow.SetActive (false);
				bow.Release ();
				arrowDrawn = false;
			}
		}	
	}

	void Movement () {
		
		character.Jump ();
		/*TODO: Implement strafing where moving forward and sideways 
		 * will be normalized so walking sideways and forwards at the same time
		 * isn't faster.
		 */

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {
			Strafe ();
			Animations.Play ("Walk");
		} else if (Input.GetKey (KeyCode.W)) {			//Forward Movement

//			if (Input.GetKey (KeyCode.LeftShift) && character.isGrounded == true && usingBow == false && attacking == false) {
			if (Input.GetKey (KeyCode.LeftShift) && usingBow == false) {
				Run ();
				character.Move (1f, true);
			} else {
				Walk ();
				character.Move (1f, false);
			}

		} else if (Input.GetKey (KeyCode.S)) {	//Backwards Movement

//			if (Input.GetKey (KeyCode.LeftShift) && character.isGrounded == true && usingBow == false && attacking == false) {
			if (Input.GetKey (KeyCode.LeftShift)  && usingBow == false) {
				Run ();
				character.Move (-1f, true);
			} else {
				Walk ();
				character.Move (-1f, false);
			}

		} 
		/* TODO: Only wait when finshed attack animation, otherwise animation is messed up
		 */
		else if (Animations.IsPlaying("Attack") == false) {
			Animations.Play ("Wait");
		}
	}
		
	void Strafe() {
		float vertical = Input.GetAxis ("Vertical") * strafeSpeed * Time.deltaTime;
		float horizontal = Input.GetAxis ("Horizontal") * strafeSpeed * Time.deltaTime;
		transform.Translate (horizontal, 0, vertical);
	}

	void Run (){
		Animations ["Walk"].speed = 2.0f;
		if (Animations.IsPlaying("Attack") == false) {
			Animations.Play ("Walk");
		}
	}
		
	void Walk (){
		Animations ["Walk"].speed = 1.0f;
		if (Animations.IsPlaying("Attack") == false) {
			Animations.Play ("Walk");
		}
	}

}

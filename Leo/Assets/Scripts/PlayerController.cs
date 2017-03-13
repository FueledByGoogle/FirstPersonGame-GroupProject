using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	Character character;
	public Camera ThirdPerson;
	public Camera FirstPerson;
	public Animation Animations;
	public GameObject PlayerGroupLocator;		//We will need to attach the grouplocater to this, so we can disable it when using the bow
	public float strafeSpeed = 2f;
	private bool attacking;

	public Bow bow;
	public GameObject Archery;			//Used to hide archery related elements when not using the bow
	public GameObject fakeArrow;		//Fake arrow only displayed when player clicks Fire and bow begins to draw
	public bool usingBow;
	public bool arrowDrawn;
	public SkinnedMeshRenderer playerMeshRenderer;

	//Jumping
	public Transform groundCheckTransform;
	private bool isGrounded;
	public float jumpSpeed = 100f;



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
		isGrounded = IsGrounded ();
	}

	private void PlayerControl (){
		
		if (usingBow == true) {
			playerMeshRenderer.enabled = false;	//render of player has to be disabled in order to see the bow and have no playerbody showing through
			PlayerGroupLocator.SetActive (false);
		} else {
			PlayerGroupLocator.SetActive (true);
			playerMeshRenderer.enabled = true;
		}

		if (usingBow == false) {					

			Archery.SetActive (false);

			if (Input.GetMouseButton (1)) {
				
				ThirdPerson.enabled = false;	//switching between cameras
				FirstPerson.enabled = true;
				usingBow = true;

			} else {
				
				if (ThirdPerson.enabled != true) {//This check probably doesn't matter but skips setting FirstPerson camera to enabled each time
					ThirdPerson.enabled = true;
					FirstPerson.enabled = false;
				}

			}

		} else if (usingBow == true) {				
			Bow ();
		}
			
		if (Input.GetMouseButton (0)) {
			Animations.Play ("Attack");
			attacking = true;
		} else {
			attacking = false;
		}

		Movement ();
	}


	void Bow() {

		//Allow strafing when using the bow
		float vertical = Input.GetAxis ("Vertical") * strafeSpeed * Time.deltaTime;
		float horizontal = Input.GetAxis ("Horizontal") * strafeSpeed * Time.deltaTime;
		character.transform.Translate (horizontal, 0, vertical);

		playerMeshRenderer.enabled = false;		//Don't render playerbody when using bow, so we can make bow closer to the camera view without seeing it in first person
		Archery.SetActive (true);

		if (arrowDrawn == false && bow.stretching == false) {	//This prevents an arrow from appearing on initial active of bow
			fakeArrow.SetActive (false);
		}

//		//if we are using the bow press "S" to exit shooting mode
//		if (Input.GetKey(KeyCode.S)) {
//			ThirdPerson.enabled = true;
//			FirstPerson.enabled = false;
//			usingBow = false;
//		}

		//Go back to using melee
		if (Input.GetKeyDown("1")) {
			ThirdPerson.enabled = true;
			FirstPerson.enabled = false;
			usingBow = false;
		}

		if (Input.GetMouseButton (0)) {			//Shooting
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
		} else if (attacking == false) {
			Animations.Play ("Wait");
		}	
	}

	void Movement () {
		Jump ();

		if (Input.GetKey (KeyCode.W)) {			//Forward Movement

			if (Input.GetKey (KeyCode.LeftShift) && isGrounded == true && usingBow == false) {
				Run ();
				character.Move (1f, true);
			} else {
				Walk ();
				character.Move (1f, false);
			}

		} else if (Input.GetKey (KeyCode.S)) {	//Backwards Movement

			if (Input.GetKey (KeyCode.LeftShift) && isGrounded == true && usingBow == false) {
				Run ();
				character.Move (-1f, true);
			} else {
				Walk ();
				character.Move (-1f, false);
			}

		}
	}

	void Jump () {
		if (Input.GetKey(KeyCode.Space) && isGrounded){
			character.characterRigidBody.AddForce (new Vector3 (0, jumpSpeed, 0));
		}
	}

	void Run (){
		Animations ["Walk"].speed = 2.0f;
		if (attacking == false) {
			Animations.Play ("Walk");
		}
	}

	void Walk (){
		Animations ["Walk"].speed = 1.0f;
		if (attacking == false) {
			Animations.Play ("Walk");
		}
	}

	bool IsGrounded () {
		/*We shoot a ray down with length 0.1f from the between the two feet at
		*the groundCheckTransform to see if it hits anything.
		*NOTE: make sure groundCheckTransform is close to the ground, otherwise ray will fall short of the ground
		*/
		isGrounded = Physics.Raycast (groundCheckTransform.position, Vector3.down, 0.1f);
		return isGrounded;
	}
}

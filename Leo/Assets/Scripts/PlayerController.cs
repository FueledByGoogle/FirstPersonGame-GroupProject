using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	Character character;
	public Camera ThirdPerson;
	public Camera FirstPerson;
	public Animation Animations;

	public Bow bow;
	public bool usingBow;
	public bool arrowDrawn;
	Rigidbody arrowInstance;
	public GameObject Archery;			//Used to hide archery related elements when not using the bow


	public GameObject fakeArrow;
	/*	Fake arrow only displayed when player clicks Fire and bow begins to draw
	 */


	//Jumping
	public Transform groundCheckTransform;
	private bool isGrounded;
	public float jumpSpeed = 100f;



	void Start () {
		character = GetComponent<Character> ();
		Animations = GetComponent<Animation> ();
		ThirdPerson.enabled = true;
		FirstPerson.enabled = false;
		usingBow = false;
		Archery.SetActive (false);

	}

	void Update () {
		
	}

	void FixedUpdate () {
		Movement ();
		isGrounded = IsGrounded ();
	}

	private void Movement (){
		
		if (Input.GetKey (KeyCode.D)) {				//Character rotation
			character.Rotate (true);
		} else if (Input.GetKey (KeyCode.A)) {
			character.Rotate (false);
		}

		if (usingBow == false) {					//Melee

			Archery.SetActive (false);

			if (Input.GetMouseButton (1)) {
				
				ThirdPerson.enabled = false;
				FirstPerson.enabled = true;
				usingBow = true;

			} else {
				
				if (ThirdPerson.enabled != true) {//This check probably doesn't matter but skips setting FirstPerson camera to enabled each time
					ThirdPerson.enabled = true;
					FirstPerson.enabled = false;
				}

			}

		} else if (usingBow == true) {				//Ranged

			Archery.SetActive (true);

			if (arrowDrawn == false && bow.stretching == false) {	//This prevents an arrow from appearing on initial active of bow
				fakeArrow.SetActive (false);
			}

			//if we are using the bow press right button again to exit shooting mode
			if (Input.GetKey(KeyCode.S)) {
				ThirdPerson.enabled = true;
				FirstPerson.enabled = false;
				usingBow = false;
			}

			if (Input.GetMouseButton (0)) {			//Shooting

				//Instainated arrow at point of fire
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

		if (usingBow == false) {

			if (Input.GetMouseButton (0)) {				//You are limited to walking while attacking

				Animations.Play ("Attack");

				if (Input.GetKey (KeyCode.W)) {
					character.Move (1f, false);
				} else if (Input.GetKey (KeyCode.S)) {
					character.Move (-1f, false);
				}

			} else {

				Jump ();

				if (Input.GetKey (KeyCode.W)) {			//Forward Movement

					if (Input.GetKey (KeyCode.LeftShift) && isGrounded == true) {	//Can only run on ground
						Run ();
						character.Move (1f, true);
					} else {
						Walk ();
						character.Move (1f, false);
					}

				} else if (Input.GetKey (KeyCode.S)) {	//Backwards Movement

					if (Input.GetKey (KeyCode.LeftShift) && isGrounded == true) {
						Run ();
						character.Move (-1f, true);
					} else {
						Walk ();
						character.Move (-1f, false);
					}

				}else {
					Animations.Play ("Wait");
				}
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
		Animations.Play ("Walk");
	}

	void Walk (){
		Animations ["Walk"].speed = 1.0f;
		Animations.Play ("Walk");
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

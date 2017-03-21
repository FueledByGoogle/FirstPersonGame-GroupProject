using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public Camera playerCamera;
	public Character character;
	private Animator animator;
	public float moveSpeed = 1f;
	public bool movementDisabled;

	public Bow bow;
	public GameObject archery;						//Used to hide archery related elements when not using the bow
	public GameObject fakeArrow;					//Fake arrow only displayed when player clicks Fire and bow begins to draw
	public GameObject rHand;
	public GameObject lHand;
	public bool usingBow;
	public bool arrowDrawn;
	bool bowZoomed;
	float bowZoomValue = 45f;


	public AudioSource shieldUpAudio;

	/*TODO: When different swords are implemented this won't work because
	 * we're pointing to a specific word here, so instead we'll be pointing to the
	 * instantiated sword later on
	 */
	public Sword sword;


	private bool shieldUP;

	void Start () {
		character = GetComponent<Character> ();
		animator = GetComponent<Animator> ();
		movementDisabled = false;
		//Bow related
		usingBow = false;
		archery.SetActive (false);
		bowZoomed = false;
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

		if (Input.GetKeyDown ("2")) {
			usingBow = true;
		}else if (Input.GetKeyDown ("1")) {
			usingBow = false;
			//Reset arrow position
			bow.stretching = false;
			bow.bowReady= false;
			bow.factor = 0f;
			fakeArrow.SetActive (false);
		}
		//Movement Disabled when shield is in use
		if (shieldUP == false && movementDisabled == false) {
			Movement ();
		}

		if (movementDisabled)
			animator.SetBool ("Walking", false);

		Bow ();
		if (usingBow == false) {
			SwordAttack ();
			Defense();
		}

	}
		
	void Bow() {

		if (usingBow == false) {

			archery.SetActive (false);

			//Graphics
			rHand.SetActive (true);
			lHand.SetActive (true);


		} else if (usingBow == true) {
			
			archery.SetActive (true);

			//Graphics
			rHand.SetActive (false);
			lHand.SetActive (false);
			if (arrowDrawn == false && bow.stretching == false) {	//This prevents an arrow from appearing on initial active of bow
				fakeArrow.SetActive (false);
			}

			//Shooting
			if (Input.GetMouseButton (0)) {

				bowZoomed = true;
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
				//revert back to default F0W
				if (bow.releasing == true) {
					playerCamera.fieldOfView = 60f;
					bowZoomed = false;
				}
			}
			//only zoom when arrow is drawn and stretching
			if (playerCamera.fieldOfView != bowZoomValue && bowZoomed)
				playerCamera.fieldOfView = Mathf.Lerp (playerCamera.fieldOfView, bowZoomValue, 0.05f);
	


		}
	}

	void Defense() {
		if (Input.GetKey (KeyCode.Q)) {
			
			animator.SetBool ("Shield_Up", true);
			if (shieldUP != true) {	//prevents audio clip from being played multiple times.
				shieldUpAudio.Play ();
			}
			shieldUP = true;

		} else
			shieldUP = false;
	}

	void Movement () {
		
		character.Jump ();
		/*TODO: Implement strafing where moving forward and sideways 
		 * will be normalized so walking sideways and forwards at the same time
		 *TODO: Ability to sprint for a short amount of time
		*/
		if ((Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A) || 
			 Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) && character.isGrounded)
			animator.SetBool ("Walking", true);

		if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A) ||
		    Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || !character.isGrounded)
			animator.SetBool ("Walking", false);


		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {
			
			Strafe ();

		} else if (Input.GetKey (KeyCode.W)) {			//Forward Movement
			
			if (Input.GetKey (KeyCode.LeftShift) && usingBow == false) {
				character.Move (1f, true);
			} else {
				character.Move (1f, false);
			}

		} else if (Input.GetKey (KeyCode.S)) {			//Backwards Movement
			
			if (Input.GetKey (KeyCode.LeftShift) && usingBow == false) {
				character.Move (-1f, true);
			} else {
				character.Move (-1f, false);
			}

		} else {
			animator.SetBool("Walking", false);
		}

	}
		
	void Strafe() {
		float vertical = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;
		float horizontal = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		transform.Translate (horizontal, 0, vertical);
	}

	void SwordAttack () {
		//MouseButtonDown b/c MouseButton refreshes too quickly and multple clicks
		//will be registered in a single frame
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Player_Sword_Attack")) {
			if (Input.GetMouseButtonDown (0) && !animator.GetCurrentAnimatorStateInfo (0).IsName ("Player_Sword_Attack")) {
				animator.SetTrigger ("Normal_Attack");
			}
		} 
	
	}
}

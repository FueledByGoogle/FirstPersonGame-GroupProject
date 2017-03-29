using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveCamera : MonoBehaviour {

	Vector2 currCameraPos;
	Vector2 smoothing;
	public float moveSpeed = 3.0f;
	public float smoothFactor = 2.0f;

	public GameObject character;
	public GameObject hands;
	public GameObject bow;

	void Update () {
		Vector2 mouseVectors = new Vector2 (Input.GetAxisRaw ("Mouse X") * moveSpeed, Input.GetAxisRaw ("Mouse Y") * moveSpeed);

		smoothing.x = Mathf.Lerp (smoothing.x, mouseVectors.x, 1f / smoothFactor);
		smoothing.y = Mathf.Lerp (smoothing.y, mouseVectors.y, 1f / smoothFactor);

		currCameraPos += smoothing;
		currCameraPos.y = Mathf.Clamp (currCameraPos.y, -30f, 30f);

		transform.localRotation = Quaternion.AngleAxis (-currCameraPos.y, Vector3.right);
		character.transform.rotation = Quaternion.AngleAxis (currCameraPos.x, Vector3.up);
		hands.transform.localRotation = Quaternion.AngleAxis (-currCameraPos.y, Vector3.right);
	}
}

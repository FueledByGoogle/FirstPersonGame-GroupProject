using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveCamera : MonoBehaviour {

	Vector2 mouseLook;

	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	public GameObject character;
	public GameObject bow;

	void Update () {
		Vector2 mouseVectors = new Vector2 (Input.GetAxisRaw ("Mouse X") * sensitivity, Input.GetAxisRaw ("Mouse Y") * sensitivity);

		mouseLook += mouseVectors;
		mouseLook.y = Mathf.Clamp (mouseLook.y, -20f, 45f);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		character.transform.rotation = Quaternion.AngleAxis (mouseLook.x, Vector3.up);
		bow.transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
	}
}

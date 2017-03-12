using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveCamera : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;

	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	public GameObject character;

	void Start () {
		character = transform.parent.gameObject;
	}

	void Update () {
		Vector2 md = new Vector2 (Input.GetAxisRaw ("Mouse X") * sensitivity, Input.GetAxisRaw ("Mouse Y") * sensitivity);

//		md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
//		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
//		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
//		mouseLook += smoothV;
		mouseLook += md;
		mouseLook.y = Mathf.Clamp (mouseLook.y, -20f, 45f);



		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);
	}


}

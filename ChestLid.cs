using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLid : MonoBehaviour {

	public GameObject lidObject;
	public UnityEngine.UI.Text pressR;
	
	bool isOpen;


	// Use this for initialization
	void Start () {
		isOpen = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyUp (KeyCode.R) && isOpen == false) { 
			lidObject.transform.Rotate(Vector3.right * -15);
			isOpen = true;
		}		
		
	}
}

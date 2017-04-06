using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPostScript : MonoBehaviour {
	
	public GameObject enterText;


	// Use this for initialization
	void Start ()
	{
	}

	void Update ()
	{


	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (true);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (false);
		}
	}

}

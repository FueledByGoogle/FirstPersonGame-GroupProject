using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        movement();
	}

    public void Move(float i)
    {
        transform.position += i * transform.forward * Time.deltaTime * 5;
    }

    void movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(-1);
        }

    }
}

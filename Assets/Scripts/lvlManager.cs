using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlManager : MonoBehaviour {

    roomNode start;
    public lvlManager lvlMan;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (lvlMan == null)
        {
            lvlMan = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (lvlMan != this)
        {
            Destroy(gameObject);
        }
    }
}

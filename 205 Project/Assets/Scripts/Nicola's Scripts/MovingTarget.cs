using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArcheryTarget : MonoBehaviour {


	public float startRangex = -2.0f;
	public float endRangex = 2.0f;
	public float startRangey = -2.0f;
	public float endRangey = 2.0f;
	public float startRangez = -2.0f;
	public float endRangez = 2.0f;
	
	public float lifeTime = 2.0f;
	
	float startTime;
	bool passBy;
	
	float randx;
	float randy;
	float randz;

	// Use this for initialization
	void Start () {
		
		Random.InitState(75);
		randx =  Random.Range (startRangex, endRangex);
		randy =  Random.Range (startRangey, endRangey);
		randz =  Random.Range (startRangez, endRangez);
		startTime = Time.time;
		passBy = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(passBy == true){
			if(Time.time < startTime + lifeTime){
				gameObject.transform.position += transform.right * Time.deltaTime * (randx);
				gameObject.transform.position += transform.up * Time.deltaTime * (randy);
				gameObject.transform.position += transform.forward * Time.deltaTime * (randz);
				
			}
			else{
				passBy = false;
				startTime = Time.time;
			}
		}
		else{
			if(Time.time < startTime + lifeTime){
				gameObject.transform.position += transform.right * Time.deltaTime * (randx * -1.0f);
				gameObject.transform.position += transform.up * Time.deltaTime * (randy * -1.0f);
				gameObject.transform.position += transform.forward * Time.deltaTime * (randz * -1.0f);
				
			}
			else{
				randx =  Random.Range (startRangex, endRangex);
				randy =  Random.Range (startRangey, endRangey);
				randz =  Random.Range (startRangez, endRangez);
				startTime = Time.time;
				passBy = true;
			}
		}
	}
}

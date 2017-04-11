using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour {

	public BridgeTrigger bridgeTrigger;
	private bool bridgeLowered = false;

	void Update () {
		if (bridgeTrigger.TargetHit && !bridgeLowered) {
			transform.rotation = Quaternion.Euler (0f, 270f, Mathf.Lerp (transform.rotation.eulerAngles.z, 360f, Time.deltaTime * 2f));
			if (transform.rotation.eulerAngles.z >= 359.98f) {
				bridgeLowered = true;
			}

		}
	}
}

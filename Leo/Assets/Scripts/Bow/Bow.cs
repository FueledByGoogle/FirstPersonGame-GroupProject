using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float factor;

    Vector3 firstPosition;
    Vector3 lastPosition;

    public bool stretching;
    public bool releasing;
	public bool bowReady;

    public const float stretchSpeed = 1f;
    public const float releaseSpeed = 5f;

	public Rigidbody arrow;
	public float arrowVelocity = 20f;
	public Transform arrowSpawnLocation;

    void Start () {
        firstPosition = transform.localPosition;
        lastPosition = transform.localPosition + Vector3.up * 0.45f;
		bowReady = false;
    }

	void Update () {

        if (stretching) {
            factor += stretchSpeed * Time.deltaTime;

            if (factor > 1.0f) {
                factor = 1.0f;
				bowReady = true;
            }
        }

		if (releasing) {
            factor -= releaseSpeed* Time.deltaTime;

            if (factor < 0.0f) {
                factor = 0.0f;
				bowReady = false;
            }
        }
        transform.localPosition = Vector3.Lerp(firstPosition, lastPosition, factor);
    }

    public void Stretch () {
        stretching = true;
        releasing = false;
    }

    public void Release () {
        releasing = true;
        stretching = false;
    }

	public Rigidbody DrawArrow (){
		Rigidbody arrowFired = Instantiate (arrow, arrowSpawnLocation.position, arrowSpawnLocation.rotation) as Rigidbody;
		return arrowFired;
	}

	public Vector3 BowPosition () {
		return arrowSpawnLocation.position;
	}

	//TODO: this won't be needed after orientation of bow is fixed
	public Quaternion BowRotation () {
		return arrowSpawnLocation.rotation;
	}

	public void Fire() {
		Rigidbody arrowFired = Instantiate (arrow, arrowSpawnLocation.position, arrowSpawnLocation.rotation) as Rigidbody;
		arrowFired.velocity = arrowVelocity * arrowSpawnLocation.forward * -1;
	}
}

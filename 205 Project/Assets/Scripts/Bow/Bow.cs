using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float factor;

    public Vector3 firstPosition;
    public Vector3 finalPosition;

    public bool stretching;
    public bool releasing;
	public bool bowReady;

    public const float stretchSpeed = 1f;
    public const float releaseSpeed = 5f;


	public float arrowVelocity = 20f;
	public Rigidbody arrow;
	public Transform arrowSpawnLocation;

	public AudioSource bowAudio;
	public bool bowAudioPlayed;

    void Start () {
        firstPosition = transform.localPosition;
        finalPosition = transform.localPosition + Vector3.up * 0.45f;
		bowReady = false;
		bowAudioPlayed = false;
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
        transform.localPosition = Vector3.Lerp(firstPosition, finalPosition, factor);
    }

    public void Stretch () {
		stretching = true;
		releasing = false;

		if (!bowAudio.isPlaying) {
			bowAudio.Play ();
		}
		
    }

    public void Release () {
        releasing = true;
        stretching = false;

//		if (!bowAudio.isPlaying) {
//			bowAudio.Stop ();
//			bowAudioPlayed = false;
//		}
    }

//	public Rigidbody DrawArrow (){
//		Rigidbody arrowFired = Instantiate (arrow, arrowSpawnLocation.position, arrowSpawnLocation.rotation) as Rigidbody;
//		return arrowFired;
//	}

	public Vector3 BowPosition () {
		return arrowSpawnLocation.position;
	}

	public void Fire() {
			bowAudio.Stop ();

		Rigidbody arrowFired = Instantiate (arrow, arrowSpawnLocation.position, arrowSpawnLocation.rotation) as Rigidbody;
		arrowFired.velocity = arrowVelocity * arrowSpawnLocation.forward * -1;
	}
}

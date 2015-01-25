using UnityEngine;
using System.Collections;

public class PlayCrashingSound : MonoBehaviour {

	public AudioSource audio;
	private static bool hasPlayedSound = false;

	// Play a sound if it hasn't been played already
	void OnCollisionEnter(Collision c) {
		if (PlayCrashingSound.hasPlayedSound == false && !audio.isPlaying) {
			audio.Play ();
		}
	}

}

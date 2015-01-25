using UnityEngine;
using System.Collections;

public class TrumpetPropEvent : EventPropEvent {

	public AudioSource soundEffectsBag;
	public int effectivenessThreshold;
	private AudioSource[] sounds;

	// Use this for initialization
	void Start () {
		sounds = soundEffectsBag.GetComponents<AudioSource> ();
		held = false;
		eventFinished = true;
		eventsPerformed = 0;
	}

	public void Update () {
		if ( eventFinished == false && held == true ) {
			if ( !sounds[7].isPlaying ) {
				// You finished! Make audience happy
				if ( eventsPerformed < effectivenessThreshold ) {
					audience.Laugh ();
				}
				eventFinished = true;
			}
		}
	}

	public override void TriggerEvent() {
		held = true;
		if ( eventFinished ) {
			eventFinished = false;
			eventsPerformed++;
			if ( eventsPerformed > effectivenessThreshold ) {
				// Not effective any more, don't play the trumpet sound!
				audience.happinessMeter -= 0.05f;
				audience.Aww (); // The audience is displeased
				eventFinished = true;
				Debug.Log ("trumpet no longer effective");
			}
			else {
				if ( audience.IsBooing() ) {
					audience.StopBooing();
				}
				sounds[7].Play ();
				audience.happinessMeter += 0.2f;
				Debug.Log ("Played trumpet " + eventsPerformed );
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudienceHappiness : MonoBehaviour {

	public float happinessMeter; // Between 0.0 and 1.0 #HashtagLerp4Ever
	public float audienceInterval; // How often the audience cheers/boos
	public float happinessLossOT; // How quickly the audience gets bored of you
	public Slider happySlider;
	public Collider audienceCollider; // Useful to handle audience reaction to thrown items
	public GameObject soundEffectsBag; // SFX bag, we meet again!
	public StartSequence start;

	private AudioSource[] sounds;
	private float lastTime;
	private float lastMood;

	// Use this for initialization
	void Start () {
		lastTime = Time.time;
		happinessMeter = lastMood = 0.75f;
		happySlider.value = happinessMeter * 100;
		sounds = soundEffectsBag.GetComponents<AudioSource> (); // see the gameobject in scene to find sound definitions
	}

	// Called every (audienceInterval) seconds, computes the change in
	// the audience's mood from the last time, and then plays a positive
	// or negative sound accordingly
	void CheerOrBoo() {
		if (lastMood > happinessMeter) {
			// If the audience's happy level went down, play a boo!
			if (!sounds[4].isPlaying)
				sounds[4].Play ();
			lastMood = happinessMeter;
			lastTime = Time.time;
		} else {
			// Otherwise, play a yay!
			if (!sounds[3].isPlaying)
				sounds[3].Play ();
			lastMood = happinessMeter;
			lastTime = Time.time;
		}
	}
	
	// Update is called once per frame
	void Update () {
		happySlider.value = happinessMeter * 100;

		// If the game has started, and the intro is over, passively decrease the audience's happiness
		if (start.eventState == StartSequence.IntroSceneState.DONE) {
			happinessMeter -= happinessLossOT;
			happySlider.enabled = true; // Make sure the slider is visible
		}
		else if ( start.eventState == StartSequence.IntroSceneState.OPENING_CURTAINS ||
		          start.eventState == StartSequence.IntroSceneState.SWITCHING_ON_SPOTLIGHTS ) {
			happySlider.enabled = true;
			lastTime = Time.time;
		}

//		// DEBUG ONLY
//		if (Input.GetKeyDown (KeyCode.H)) {
//			happinessMeter = Mathf.Min( happinessMeter + 0.15f, 1.0f );
//		}

		if ( Time.time - lastTime >= audienceInterval 
		    && start.eventState == StartSequence.IntroSceneState.DONE ) {
			// It is time to decide how you've fared with the audience
			CheerOrBoo();
			lastTime = Time.time;
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudienceHappiness : MonoBehaviour {

	public float happinessMeter; // Between 0.0 and 1.0 #HashtagLerp4Ever
	public float happyDelta;
	public float audienceInterval; // How often the audience cheers/boos
	public float happinessLossOT; // How quickly the audience gets bored of you
	public Slider happySlider;
	public Collider audienceCollider; // Useful to handle audience reaction to thrown items
	public GameObject soundEffectsBag; // SFX bag, we meet again!
	public GameObject gameOverScreen;
	public CharacterController player;
	public StartSequence start;

	/**
	 * AudioSource order on SoundEffectsBag:
	 * 
	 * 0 - zap
	 * 1 - spotlight-flicker
	 * 2 - throat clearing
	 * 3 - cheering
	 * 4 - booing
	 * 5 - laughing
	 * 6 - awww
	 * 7 - trumpet noise
	 */

	private AudioSource[] sounds;
	private float lastTime;
	private float lastMood;

	// Use this for initialization
	void Start () {
		gameOverScreen.renderer.enabled = false;
		lastTime = Time.time;
		happinessMeter = lastMood = 0.75f;
		happySlider.value = Mathf.FloorToInt( happinessMeter * 100 );
		happyDelta = lastMood - happinessMeter;
		sounds = soundEffectsBag.GetComponents<AudioSource> (); // see the gameobject in scene to find sound definitions
	}

	// Called every (audienceInterval) seconds, computes the change in
	// the audience's mood from the last time, and then plays a positive
	// or negative sound accordingly
	private void CheerOrBoo() {
		if (lastMood > happinessMeter) {
			// If the audience's happy level went down, play a boo!
			if (!IsMakingSound())
				Boo ();
			lastMood = happinessMeter;
			lastTime = Time.time;
		} else {
			// Otherwise, play a yay!
			if (!IsMakingSound())
				Cheer ();
			lastMood = happinessMeter;
			lastTime = Time.time;
		}
	}

	// Returns true if audience is cheering/booing/crying/laughing etc.
	public bool IsMakingSound() {
		return (
			sounds[3].isPlaying || sounds[4].isPlaying || sounds[5].isPlaying || sounds[6].isPlaying
		);
	}

	public bool IsBooing() {
		return sounds [4].isPlaying;
	}

	public void StopBooing() {
		sounds [4].Stop ();
	}

	// Stops all the audience sounds immediately (i.e. we just caught a ball or did
	// a fancy trick or something.)
	public void StopAllSounds() {
		foreach (AudioSource s in sounds) {
			s.Stop ();
		}
	}

	public void Cheer() {
		sounds[3].Play ();
	}

	public void Boo() {
		sounds[4].Play ();
	}

	// A positive response to an action
	public void Laugh() {
		sounds[5].Play ();
	}

	public void Aww() {
		sounds[6].Play ();
	}

	// Update is called once per frame
	void Update () {
		happySlider.value = Mathf.FloorToInt( happinessMeter * 100 );

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

		if ( Time.time - lastTime >= audienceInterval 
		    && start.eventState == StartSequence.IntroSceneState.DONE ) {
			// It is time to decide how you've fared with the audience
			CheerOrBoo();
			lastTime = Time.time;
		}

		if ( happinessMeter <= 0 ) {
			player.enabled = false;
			gameOverScreen.renderer.enabled = true;
		}
	}
}

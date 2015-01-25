using UnityEngine;
using System.Collections;

public class StartSequence : MonoBehaviour
{
	public Collider curtainLeft, curtainRight;
	public float moveAmount, 
				 curtainOpenDuration, // How long it takes for the curtains to open
	             spotLightPause;      // How much time between each spotlight turning on
	public GameObject topLeftSptLight, botLeftSptLight, topRightSptLight, botRightSptLight, middleSptLight;
	public AudioSource spotLightOnSound, curtainOpeningSound;
	public GameObject titleScreenImageUI, startTextUI; 	
	public CharacterController player;

	// Makes life easier for scripted events
	public enum IntroSceneState {
		SHOWING_TITLE_SCREEN,
		OPENING_CURTAINS,
		SWITCHING_ON_SPOTLIGHTS,
		DONE
	};

	private Vector3 curtainLeftStart, curtainRightStart;
	private float startTime;
	private int lightsOn = 0;
	public IntroSceneState eventState;

	void Start ()
	{
		// Make sure we aren't autoplaying anything
		spotLightOnSound.playOnAwake = curtainOpeningSound.playOnAwake = false;

		curtainLeftStart = curtainLeft.transform.position;
		curtainRightStart = curtainRight.transform.position;
		eventState = IntroSceneState.SHOWING_TITLE_SCREEN;

		// Turn off the front of stage lights first
		topLeftSptLight.light.enabled = false;
		botLeftSptLight.light.enabled = false;
		topRightSptLight.light.enabled = false;
		botRightSptLight.light.enabled = false;
		middleSptLight.light.enabled = false;
		lightsOn = 0;
	}

	void Update ()
	{

		if (lightsOn >= 5) {
			// All spotlights are on, then the intro is over
			eventState = IntroSceneState.DONE;
		}

		switch (eventState) {
		case IntroSceneState.SHOWING_TITLE_SCREEN:
			// The title screen should still be visible
			if (Input.GetKeyDown("space"))
			{
				// Enable the character controller
				player.enabled = true;

				// Delete the title screen UI
				Destroy(titleScreenImageUI);
				Destroy(startTextUI);

				// Open the curtains
				startTime = Time.time;
				eventState = IntroSceneState.OPENING_CURTAINS;
				OpenCurtains();
				Debug.Log("The curtains have opened!");
			}
			else {
				player.enabled = false;
			}
			break;
		case IntroSceneState.OPENING_CURTAINS:
			MoveCurtain (true, curtainLeft, curtainLeftStart);
			MoveCurtain (false, curtainRight, curtainRightStart);
			break;
		case IntroSceneState.SWITCHING_ON_SPOTLIGHTS:
			if ( spotLightOnSound.isPlaying ) break; // Wait until the sfx is off
			switch (lightsOn){
				// This switch-case determines the order in which the spotlights are turned on
			case 0:
				topLeftSptLight.light.enabled = true;
				spotLightOnSound.Play();
				lightsOn++;
				break;
			case 1:
				topRightSptLight.light.enabled = true;
				spotLightOnSound.Play ();
				lightsOn++;
				break;
			case 2:
				botLeftSptLight.light.enabled = true;
				spotLightOnSound.Play();
				lightsOn++;
				break;
			case 3:
				botRightSptLight.light.enabled = true;
				spotLightOnSound.Play();
				lightsOn++;
				break;
			case 4:
				middleSptLight.light.enabled = true;
				spotLightOnSound.Play();
				lightsOn++;
				break;
			default:
				break;
			}
			break;
		}

	}

	private void OpenCurtains() {				
		curtainOpeningSound.Play ();
		MoveCurtain (true, curtainLeft, curtainLeftStart);
		MoveCurtain (false, curtainRight, curtainRightStart);
	}

	private void MoveCurtain (bool moveLeft, Collider curtain, Vector3 curtainStart)
	{
		Vector3 curtainEnd;
		float timePercent = Mathf.Clamp((Time.time - startTime) / curtainOpenDuration, 0, 1);

		Debug.Log("Curtains Opened: " + timePercent + "%" );
		if (moveLeft)
		{
			curtainEnd = curtainStart + new Vector3 (-moveAmount, 0, 0);
		}
		else
		{
			curtainEnd = curtainStart + new Vector3 (moveAmount, 0, 0);
		}
		curtain.transform.position = Vector3.Lerp(curtainStart, curtainEnd, timePercent);

		// Stop calling this function when the curtains have fully opened
		if (timePercent >= 1.0) {
			eventState = IntroSceneState.SWITCHING_ON_SPOTLIGHTS;
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeginSinging : MonoBehaviour
{
	public GameObject player;
	public GameObject[] rafters;
	public GameObject sparkEmitter;
	public GameObject[] lights;
	public GameObject[] permanentFlickerLights;
	public GameObject deadLight;
	public GameObject micStand;
	public Text interactText;
	public float flickerTime, waitForRafters, rafterSpinAmount;
	public AudioSource soundEffectsBag;
	public StartSequence introHandle;
	public WinCheck winCheck;
	public enum SceneState {
		notStarted, singing, flickering, raftersFell
	};
	public AudienceHappiness crowd;
	public SceneState sceneState;

	private float startTime;
	private bool colliding, started, permanentFlicker;
	private AudioSource[] sounds;
	private bool humPlayed;
	private Vector3 anchor;

	void Start ()
	{
		sceneState = SceneState.notStarted;
		// Grab all of the Audio source components from the sound effects bag
		sounds = soundEffectsBag.GetComponents<AudioSource> ();
		humPlayed = false;
	}
	
	void Update ()
	{
		if (colliding)
		{
			if (Input.GetKeyDown(KeyCode.F) && introHandle.eventState == StartSequence.IntroSceneState.DONE)
			{
				sceneState = SceneState.singing;
				started = true;
				startTime = Time.time;
				anchor = player.transform.position;
				interactText.text = "";
				if (crowd != null) {
					crowd.happinessMeter += 0.075f;
				}
				sounds[2].Play ();
			}

			if ( introHandle.eventState == StartSequence.IntroSceneState.SWITCHING_ON_SPOTLIGHTS ||
			     introHandle.eventState == StartSequence.IntroSceneState.OPENING_CURTAINS ) {
				interactText.text = "Wait for your cue!";
			}
		}

		if (sceneState == SceneState.singing)
		{
			// Hold player
			player.transform.position = anchor;

			if (Time.time - startTime >= flickerTime)
			{
				startTime = Time.time;
				sceneState = SceneState.flickering;
				permanentFlicker = true;

				deadLight.light.enabled = false;
				sparkEmitter.particleEmitter.Emit();
				sounds[0].Play ();
			}
		}

		if (sceneState == SceneState.flickering)
		{
			// Hold player
			player.transform.position = anchor;

			micStand.rigidbody.isKinematic = false;
			micStand.rigidbody.useGravity = true;
			micStand.rigidbody.AddTorque (new Vector3(0, 5, 10));

			// Flicker lights
			flickerLights(lights, 50);
			
			if (Time.time - startTime >= waitForRafters)
			{
				startTime = Time.time;
				sceneState = SceneState.raftersFell;

				// Enable all lights
				for (int i = 0; i < lights.Length; i++)
				{
					lights[i].light.enabled = true;
				}

				// Make rafters fall
				for (int i = 0; i < rafters.Length; i++)
				{
					rafters[i].rigidbody.isKinematic = false;
					rafters[i].rigidbody.useGravity = true;
					float x = Random.Range(-rafterSpinAmount, rafterSpinAmount);
					float y = Random.Range(-rafterSpinAmount, rafterSpinAmount);
					float z = Random.Range(-rafterSpinAmount, rafterSpinAmount);
					rafters[i].rigidbody.AddTorque(x, y, z);
					rafters[i].rigidbody.AddForce(x, y, z);
					Debug.Log(x + " " + y + " " + z);
				}

				winCheck.startTimer();
			}
		}

		if (permanentFlicker)
		{
			flickerLights(permanentFlickerLights, 97);
		}

		if (sceneState == SceneState.raftersFell)
		{
			tag = "Prop"; // Once the rafters have fallen, the mic stand becomes a prop
			foreach (CapsuleCollider c in GetComponents<CapsuleCollider>()) {
				if ( c.isTrigger ) {
					c.enabled = false;
				}
			}
		}
	}

	void OnGUI ()
	{
		if (colliding && !started && introHandle.eventState == StartSequence.IntroSceneState.DONE) {
			interactText.text = "'F' to clear throat!";
		} else if (colliding && introHandle.eventState == StartSequence.IntroSceneState.DONE && tag.Equals ("Prop")) {
			interactText.text = "'F' to use!";
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Player")
		{
			Debug.Log("ouch");
			colliding = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Player")
		{
			Debug.Log("ouch");
			colliding = false;
			interactText.text = "";
		}
	}

	private void flickerLights (GameObject[] lights, int chanceOfFlicker)
	{
		// Flicker lights
		for (int i = 0; i < lights.Length; i++)
		{
			if (Random.Range(0, 100) >= chanceOfFlicker)
			{
				lights[i].light.enabled = false;
			}
			else
			{
				lights[i].light.enabled = true;
				// Limit playing the hum sound to once
				if (!sounds[1].isPlaying && !humPlayed) {
					sounds[1].Play();
					humPlayed = true;
				}
			}
		}
	}
}

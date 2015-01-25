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
	public float flickerTime, waitForRafters;

	private float startTime;
	private enum SceneState {
		notStarted, singing, flickering, raftersFell
	};
	private SceneState sceneState;
	private bool colliding, started, permanentFlicker;
	private Vector3 anchor;

	void Start ()
	{
		sceneState = SceneState.notStarted;
	}
	
	void Update ()
	{
		if (colliding)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				sceneState = SceneState.singing;
				started = true;
				startTime = Time.time;
				anchor = player.transform.position;
				interactText.text = "";
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

				for (int i = 0; i < rafters.Length; i++)
				{
					rafters[i].rigidbody.isKinematic = false;
					rafters[i].rigidbody.useGravity = true;
				}
			}
		}

		if (permanentFlicker)
		{
			flickerLights(permanentFlickerLights, 95);
		}

		if (sceneState == SceneState.raftersFell)
		{

		}
	}

	void OnGUI ()
	{
		if (colliding && !started)
		{
			interactText.text = "'F' to sing!";
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
			}
		}
	}
}

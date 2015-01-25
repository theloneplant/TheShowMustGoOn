using UnityEngine;
using System.Collections;

public class BeginSinging : MonoBehaviour
{
	public GameObject player;
	public GameObject[] rafters;
	public GameObject sparkEmitter;
	public GameObject[] lights;
	public GameObject deadLight;
	public float flickerTime, waitForRafters;

	private float startTime;
	private bool colliding, singing, flickering, raftersFell;
	private Vector3 anchor;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (colliding)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				singing = true;
				startTime = Time.time;
				anchor = player.transform.position;
			}
		}

		if (singing)
		{
			// Hold player
			player.transform.position = anchor;

			if (Time.time - startTime >= flickerTime)
			{
				startTime = Time.time;
				flickering = true;
				singing = false;

				deadLight.light.enabled = false;
				sparkEmitter.particleEmitter.Emit();
			}
		}

		if (flickering)
		{
			// Hold player
			player.transform.position = anchor;

			// Flicker lights
			for (int i = 0; i < lights.Length; i++)
			{
				if (Random.Range(0, 100) >= 50)
				{
					lights[i].light.enabled = false;
				}
				else
				{
					lights[i].light.enabled = true;
				}
			}
			
			if (Time.time - startTime >= waitForRafters)
			{
				startTime = Time.time;
				raftersFell = true;
				flickering = false;

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

		if (raftersFell)
		{

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
		}
	}
}

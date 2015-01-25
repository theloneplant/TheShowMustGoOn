using UnityEngine;
using System.Collections;

public class BeginSinging : MonoBehaviour
{
	public GameObject player;

	private bool colliding, singing;
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
				anchor = player.transform.position;
			}
		}

		if (singing)
		{
			Debug.Log("IM SINGINGGG");
			player.transform.position = anchor;

			// Start scripted event
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

using UnityEngine;
using System.Collections;

public class ThrowTrash : MonoBehaviour
{
	public AudienceHappiness happiness;
	public GameObject aimObject;
	public BoxCollider area;
	public GameObject[] trash;
	public float throwThreshold, throwDelay, throwSpeed;

	private float currentTime, oldTime;

	// Use this for initialization
	void Start ()
	{
		currentTime = Time.time;
		oldTime = currentTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (happiness.happinessMeter < throwThreshold)
		{
			Debug.Log(currentTime + " - " + throwDelay);
			if (currentTime > oldTime + throwDelay)
			{
				Debug.Log("They're throwing everything!");
				// Generate a random position in the box collider
				float x = Random.Range(area.transform.position.x - area.transform.localScale.x / 2, 
				                       area.transform.position.x + area.transform.localScale.x / 2);
				float y = Random.Range(area.transform.position.y - area.transform.localScale.y / 2, 
				                      area.transform.position.y + area.transform.localScale.y / 2);
				float z = Random.Range(area.transform.position.z - area.transform.localScale.z / 2, 
				                      area.transform.position.z + area.transform.localScale.z / 2);
				Vector3 throwPos = new Vector3(x, y, z);

				// Find the vector to the stage
				Vector3 delta = -(aimObject.transform.position - throwPos) * throwSpeed;

				// Make an object pointed at the stage & throw it
				GameObject newTrash = (GameObject) Instantiate (trash[Random.Range(0, trash.Length)], throwPos, Quaternion.identity);
				newTrash.rigidbody.velocity = delta;

				// Update time
				oldTime = currentTime;

				Debug.Log(delta.ToString() + " - " + newTrash.rigidbody.velocity.ToString());
			}

			// Update time
			currentTime = Time.time + (float) (Random.Range(0, 100) / 100.0f) * Time.deltaTime;
		}
	}
}

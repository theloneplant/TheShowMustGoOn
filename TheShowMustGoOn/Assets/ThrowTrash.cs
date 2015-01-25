using UnityEngine;
using System.Collections;

public class ThrowTrash : MonoBehaviour
{
	public AudienceHappiness happiness;
	public GameObject aimObject;
	public BoxCollider area;
	public GameObject[] trash;
	public float throwThreshold, throwDelay;

	private float currentTime;

	// Use this for initialization
	void Start ()
	{
		currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (happiness.happinessMeter < throwThreshold)
		{
			if (currentTime > throwDelay)
			{

				float x = Random.Range(area.transform.position.x - area.transform.localScale.x / 2, 
				                       area.transform.position.x + area.transform.localScale.x / 2);
				float y = Random.Range(area.transform.position.y - area.transform.localScale.y / 2, 
				                      area.transform.position.y + area.transform.localScale.y / 2);
				float z = Random.Range(area.transform.position.z - area.transform.localScale.z / 2, 
				                      area.transform.position.z + area.transform.localScale.z / 2);
				Vector3 throwPos = new Vector3(x, y, z);
				Vector3 delta = aimObject.transform.position - throwPos;

				GameObject newTrash = Instantiate (trash[Random.Range(0, trash.Length - 1)], throwPos, Vector3.zero);
				newTrash.rigidbody.velocity = delta;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class ThrowBackToStage : MonoBehaviour
{
	public float randomAmount;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Prop")
		{
			float x = Random.Range(-randomAmount, randomAmount);
			float y = Random.Range(-randomAmount, randomAmount);
			float z = Random.Range(-randomAmount, randomAmount);
			Vector3 randomVector = new Vector3(x, y, z);
			other.rigidbody.velocity = -other.rigidbody.velocity + randomVector;
			other.rigidbody.AddTorque(x * 5, y * 5, z * 5);
		}
	}
}

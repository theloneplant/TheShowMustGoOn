using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupObject : MonoBehaviour
{
	public CharacterController player;
	public FixedJoint arm;
	public Text interactText;
	public float offsetAmount;

	private bool colliding, holding;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (colliding && Input.GetKey(KeyCode.F))
		{
			holding = true;
			arm.connectedBody = rigidbody;
		}

		if (Input.GetKeyUp(KeyCode.F))
		{
			holding = false;
			arm.connectedBody = null;
			rigidbody.velocity = player.velocity;
		}
	}

	void OnGUI ()
	{
		if (colliding)
		{
			interactText.text = "'F' to use!";
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Player")
		{
			colliding = true;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Player")
		{
			colliding = false;
			interactText.text = "";
		}
	}
}

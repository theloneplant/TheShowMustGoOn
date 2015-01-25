using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupObject : MonoBehaviour
{
	public CharacterController player;
	public FixedJoint arm;
	public Text interactText;
	public float offsetAmount;

	private Rigidbody other;
	private bool colliding, holding;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (colliding && Input.GetKey(KeyCode.F) && other != null)
		{
			holding = true;
			arm.connectedBody = other;
		}

		if (Input.GetKeyUp(KeyCode.F) && other != null)
		{
			holding = false;
			arm.connectedBody = null;
			other.velocity = player.velocity;
			other = null;
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
		if (other.tag == "Prop")
		{
			colliding = true;
			this.other = other.gameObject.rigidbody;
		}
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Prop")
		{
			colliding = true;
			this.other = other.gameObject.rigidbody;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Prop")
		{
			colliding = false;
			interactText.text = "";
		}
	}
}

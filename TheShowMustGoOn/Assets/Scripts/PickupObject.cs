using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupObject : MonoBehaviour
{
	public CharacterController player;
	public Camera playerCam;
	public FixedJoint arm;
	public Text interactText;
	public float throwSpeed;

	private Rigidbody other;
	private bool colliding, holding;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.F) && other != null)
		{
			if (holding)
			{
				holding = false;
				arm.connectedBody = null;
				Vector3 direction = transform.position - playerCam.transform.position;
				other.velocity = player.velocity + direction * throwSpeed;
				other = null;
			}
			else if (colliding)
			{
				holding = true;
				arm.connectedBody = other;
			}
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

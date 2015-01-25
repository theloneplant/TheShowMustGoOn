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
	public AudienceHappiness crowd;

	private Rigidbody other;
	private GameObject otherObj;
	private bool colliding, holding;


	// Use this for initialization
	void Start ()
	{
	
	}

	void Throw() {
		holding = false;
		arm.connectedBody = null;
		Vector3 direction = transform.position - playerCam.transform.position;
		other.velocity = player.velocity + direction * throwSpeed;
		other = null;
		crowd.happinessMeter += 0.025f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.F) && other != null)
		{
			if (holding)
			{
				if ( otherObj.tag == "EventProp" ) {
					if ( otherObj.GetComponent<EventPropEvent>().IsThrowable() ) {
						otherObj.GetComponent<EventPropEvent>().held = false;
						Throw();
					}
				}
				else {
					Throw ();
				}
			}
			else if (colliding)
			{
				holding = true;
				arm.connectedBody = other;
				if ( otherObj.tag == "EventProp" ) {
					EventPropEvent e = other.gameObject.GetComponent<EventPropEvent>();
					if ( e != null ) {
						e.TriggerEvent();
					}
				}
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
		if (other.tag == "Prop")
		{
			colliding = true;
			this.other = other.gameObject.rigidbody;
			otherObj = other.gameObject;
		}
		else if (other.tag == "EventProp") {
			// trigger the event on the event prop object
			colliding = true;
			otherObj = other.gameObject;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Prop" || other.tag == "EventProp")
		{
			colliding = true;
			this.other = other.gameObject.rigidbody;
			otherObj = other.gameObject;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Prop")
		{
			colliding = false;
			interactText.text = "";
		}
		else if (other.tag == "EventProp") {
			colliding = false;
			interactText.text = "";
		}
	}
}

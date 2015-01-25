using UnityEngine;
using System.Collections;

public abstract class EventPropEvent : MonoBehaviour {

	public int eventsPerformed;
	public AudienceHappiness audience;
	public bool eventFinished;
	public bool held;

	// Use this for initialization
	void Start () {
		eventsPerformed = 0;
		held = false;
	}

	// Trigger this object's special event
	public abstract void TriggerEvent();

	// Is throwable
	public bool IsThrowable() {
		return eventFinished;
	}

}

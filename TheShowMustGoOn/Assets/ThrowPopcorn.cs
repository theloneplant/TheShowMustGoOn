using UnityEngine;
using System.Collections;

public class ThrowPopcorn : MonoBehaviour
{
	public AudienceHappiness happiness;
	public ParticleEmitter emitter;
	public float throwThreshold;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (happiness.happinessMeter < throwThreshold)
		{
			emitter.emit = true;
		}
		else
		{
			emitter.emit = false;
		}
	}
}

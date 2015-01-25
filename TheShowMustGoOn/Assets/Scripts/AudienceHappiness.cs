using UnityEngine;
using System.Collections;

public class AudienceHappiness : MonoBehaviour {

	public float happinessMeter; // Between 0.0 and 1.0 #HashtagLerp4Ever
	public float audienceInterval; // How often the audience cheers/boos
	private float lastTime;
	private float lastMood;

	// Use this for initialization
	void Start () {
		lastTime = Time.time;
	}

	// Called every (audienceInterval) seconds, computes the change in
	// the audience's mood from the last time, and then plays a positive
	// or negative sound accordingly
	void CheerOrBoo() {
		if (lastMood - happinessMeter > 0.0) {
			// If the audience's happy level went down, play a boo!
		} else {
			// Otherwise, play a yay!
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime >= audienceInterval) {
			// It is time to decide how you've fared with the audience
			CheerOrBoo();
			lastTime = Time.time;
		}
	}
}

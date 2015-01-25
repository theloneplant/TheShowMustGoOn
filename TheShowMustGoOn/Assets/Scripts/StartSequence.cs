using UnityEngine;
using System.Collections;

public class StartSequence : MonoBehaviour
{
	public Collider curtainLeft, curtainRight;
	public float moveAmount, duration;

	private Vector3 curtainLeftStart, curtainRightStart;
	private float startTime;
	private bool started;
	
	void Start ()
	{
		curtainLeftStart = curtainLeft.transform.position;
		curtainRightStart = curtainRight.transform.position;
	}

	void Update ()
	{
		if (Input.GetKeyDown("space") && !started)
		{
			startTime = Time.time;
			started = true;
		}

		// If the game has been started, aka space has been pressed
		if (started)
		{
			MoveCurtain (true, curtainLeft, curtainLeftStart);
			MoveCurtain (false, curtainRight, curtainRightStart);
		}
	}

	private void MoveCurtain (bool moveLeft, Collider curtain, Vector3 curtainStart)
	{
		Vector3 curtainEnd;
		float timePercent = Mathf.Clamp((Time.time - startTime) / duration, 0, 1);

		if (moveLeft)
		{
			curtainEnd = curtainStart + new Vector3 (-moveAmount, 0, 0);
		}
		else
		{
			curtainEnd = curtainStart + new Vector3 (moveAmount, 0, 0);
		}
		curtain.transform.position = Vector3.Lerp(curtainStart, curtainEnd, timePercent);
	}
}

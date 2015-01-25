using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCheck : MonoBehaviour
{
	public BeginSinging singing;
	public Text winText, winMessage, timer;
	public float secondsUntilWin;

	private float startTime;
	private float timeElapsed;
	private bool started;

	// Use this for initialization
	void Start ()
	{
		started = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (started)
		{
			timeElapsed = Time.time - startTime;

			if (timeElapsed > secondsUntilWin)
			{
				winText.enabled = true;
				winMessage.enabled = true;
			}
			else
			{
				float delta = secondsUntilWin - timeElapsed;
				timer.text = (int) delta / 60 + ":" + (int) delta % 60;
			}
		}
	}

	public void startTimer ()
	{
		startTime = Time.time;
		started = true;
	}
}

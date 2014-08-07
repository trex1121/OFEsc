/*
 * Timer.cs takes GUItext and increments it by Time.Delatime (h:m:s)
 */
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public GUIText curTime;
	private bool posted;
	private bool started;
	private float hours, minutes, seconds;
	

	void Start () 
	{
		seconds = 0f;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}


	public void GameStart()
	{
		started = true;
	}
	public void GameOver ()
	{
		print ("Timer says you are done");
	}

	void Update()
	{
		//if (started)
		seconds += Time.deltaTime;

		if(seconds >= 60f)
		{
			minutes +=1;
			seconds = 0f;
		}

		if(minutes >= 60f)
		{
			hours +=1;
			minutes = 0;
		}

		curTime.text = string.Format("{0}:{1}:{2}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"));
	}
}

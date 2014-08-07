/*
 * WorkTime.cs converst game clock time to game world time
 * to define a work day.
 */
using UnityEngine;
using System.Collections;

public class WorkTime : MonoBehaviour
{
	public WorkClock clockTime;

		
		void Update ()
		{
		//Debug only
			//print (GameTime ());
		}

	public string GameTime ()
	{
		return string.Format ("{0}:{1}:{2}", 
		                      clockTime.GetHours.ToString ("00"), 
		                      clockTime.GetMinutes.ToString ("00"), 
		                      clockTime.GetSeconds.ToString ("00"));
	}
}


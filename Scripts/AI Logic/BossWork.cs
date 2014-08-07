using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossWork : Work
{
	Dictionary<Transform, int> meetingSchedule = new Dictionary<Transform, int>();

		
		void Update ()
		{
		//TODO deactivate test keys
		// Testing Block ------------
			if(Input.GetKeyDown(KeyCode.M))
			{
				Debug.Log("Set Meeting");
				meeting = true;
			}
			if(Input.GetKeyDown(KeyCode.P))
			{
				Debug.Log("Added 1 Paper");
				papersLeft += 1;
			}
				//----------
			if(papersLeft > 0)
			{
				paperwork = true;
			}
		}


	public void SetMeetingWith(int time, Work setTime)
	{
		//check if there is a meeting on the books with employee X; if not set meeting time for you both
	}
}


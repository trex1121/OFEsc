using UnityEngine;
using System.Collections;

public class TestController: MonoBehaviour
{
	public GameObject closestsWaypoint; 

	public WaypointController _waypontcontroller = null; // assign Waypoint Controller GameObject

	private bool firstUpdate = true;

		void FixedUpdate ()
		{
            //find closest waypoint on firstUpdate
			if(firstUpdate)
		{
			closestsWaypoint = _waypontcontroller.FindClosestsWaypoint(transform); //Use WaypointController* funtion to find Closest Waypoint to transform
		}
		}
}


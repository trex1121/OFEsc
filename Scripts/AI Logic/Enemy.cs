using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	public GameObject closestsWaypoint;
	public WaypointController _waypontcontroller = null;
	public Transform playerTransform;
	public List<Transform> playerLocation = new List<Transform>();
	public Transform myTransform;

	private bool firstUpdate = true;
	public float smoothTime = 1.3f;
	public float maxSpeed = 2.0f;
	private Vector3 velocity = Vector3.zero;
	public float moveNextDistance = 3f;

	void Start()
	{
		myTransform = GetComponent<Transform>();
		playerTransform = GameObject.FindGameObjectWithTag(Tags.player).transform;
		_waypontcontroller = GameObject.FindGameObjectWithTag(Tags.waypointcontroller).GetComponent<WaypointController>();
		closestsWaypoint = _waypontcontroller.FindClosestsWaypoint(myTransform);
		playerLocation = closestsWaypoint.GetComponent<Waypoint>().GetPlayerLocation();
	}
	
	void Update ()
	{
		var distanceToWaypoint = Vector3.Distance(myTransform.position, closestsWaypoint.transform.position);
		var distanceToPlayer = Vector3.Distance(myTransform.position, playerTransform.position);

			if(distanceToWaypoint < moveNextDistance &&
		   playerLocation.Count > 1)
		{
			closestsWaypoint = playerLocation[playerLocation.Count - 2].gameObject;
			playerLocation = closestsWaypoint.GetComponent<Waypoint>().GetPlayerLocation();
		}
		else if(playerLocation.Count < 1 && distanceToPlayer < 1.5f)
		{
			closestsWaypoint = playerTransform.gameObject;
		}


		myTransform.position = Vector3.SmoothDamp(myTransform.position, closestsWaypoint.transform.position, ref velocity, smoothTime, maxSpeed);
		                                          
	}
}


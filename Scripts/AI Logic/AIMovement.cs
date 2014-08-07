using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMovement : MonoBehaviour 
{
	public float speed;
	public Transform pathingTarget;

	public Transform[] pathingTargets;//TODO using for front and back approach

	//inspector set pathing zones
	public Transform bathroom;
	public Transform fridge;
	public Transform boardRoom;
	public Transform myDesk;

	public List<Vector2> path;

	public Vector3 GetCurrentTarget
	{
		get{return pathingTarget.position;}
	} //return position of current target

	// LateUpdate is called once per frame
	void LateUpdate () 
	{
		//TODO deactivate test keys
		//Testing Blocking ----------
//		if(Input.GetKeyDown(KeyCode.E))
//		{
//			//set path nodes to list
//			path = NavMesh2D.GetSmoothedPath(transform.position,pathingTarget.position);
//		}
		//------------

		if(path != null && path.Count != 0)//if there is a path list created and there are more than 0
		{
			// move towards [0] element path
			transform.position = Vector2.MoveTowards(transform.position, path[0], speed*Time.deltaTime);
			if(Vector2.Distance(transform.position,path[0]) < 0.01f) // once close..
			{
				path.RemoveAt(0);//delete [0] element path
			}
		}
	}

	public void SetTarget(Transform target)
	{
		Debug.Log (string.Format("{0} is MOVING to {1}", this.name, target.name));
		pathingTarget = target;
		path = NavMesh2D.GetSmoothedPath(transform.position,target.position);
	}
}

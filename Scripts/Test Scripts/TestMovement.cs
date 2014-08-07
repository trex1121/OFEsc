using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMovement : MonoBehaviour 
{
	public float speed;
	public Transform pathingTarget;

	public Transform[] pathingTargets;

	private List<Vector2> path;
	
	// LateUpdate is called once per frame
	void LateUpdate () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			//set path nodes to list
			path = NavMesh2D.GetSmoothedPath(transform.position,pathingTarget.position);
		}
		
		if(path != null && path.Count != 0)
		{
			// move towards [0] element path
			transform.position = Vector2.MoveTowards(transform.position, path[0], speed*Time.deltaTime);
			if(Vector2.Distance(transform.position,path[0]) < 0.01f) // once close..
			{
				path.RemoveAt(0);//delete [0] element path
			}
		}
	}
}

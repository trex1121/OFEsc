using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewRay : MonoBehaviour
{
	List<Transform> path;
	public Color rayColor = Color.red;

	void OnDrawGizmos()
	{
		Gizmos.color = rayColor;

		var path_objs = GetComponentsInChildren<Transform>();
		path = new List<Transform>();

		foreach(var Listed in path_objs)
		{
			if(Listed != this.transform)
			path.Add(Listed);
		}

		for(int i = 0; i < path.Count; i++)
		{
			Vector3 pos = path[i].position;
			if(i > 0)
			{
				var prev = path[i-1].position;
				Gizmos.DrawLine(prev,pos);
				Gizmos.DrawWireSphere(pos, 0.4f);
			}

		}
	}
}


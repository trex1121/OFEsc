using UnityEngine;
using System.Collections;

public class SkyCam : MonoBehaviour 
{
	public Transform target;

	private Transform transCached;

	void Start () 
	{
		transCached = GetComponent<Transform>();
		if(target == null)
		{
			throw new MissingReferenceException("Object has no target");
		}
	}
	
	void LateUpdate ()//unity tip* use late update for objects receiving info for objects acting in update such as camera follow
	{
		transCached.transform.position = new Vector3(target.position.x, target.position.y,
		                                            transCached.position.z);
	}
}

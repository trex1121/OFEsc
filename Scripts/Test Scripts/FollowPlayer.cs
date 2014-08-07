using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Transform target;
	public  Vector2 offset;

	private Transform transCached;


	void Start () 
	{
		transCached = GetComponent<Transform>();

		if(target == null)
		{
			throw new MissingReferenceException("Object has no target");
		}
	}
	
	void Update () 
	{
		transCached.transform.position = new Vector3(offset.x + target.position.x, 
		                                             offset.y + target.position.y,
		                                             transCached.position.z);
	}
}


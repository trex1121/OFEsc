using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour
{
	public GameObject[] waypoints;
	 
	public float searchRadi = 0.2f;
	//public LayerMask mask = 1 << 10; // can also be written LayerMask.NameToLayer("Name of targe
	//public Waypoint startWaypoint = null; // assign a default waypoint
	public Transform playerTransform;

	private int @breakWhile = 3; // while loop fail condition
	private int randomWaypoint;

		void Start ()
		{
			playerTransform = GameObject.FindGameObjectWithTag(Tags.player).transform; // find player tranform
			waypoints = GameObject.FindGameObjectsWithTag(Tags.waypoint); // collect waypoints
			//startWaypoint.initializeData();
			StartCoroutine(WaitAndUpdate());
		}

		public GameObject FindClosestsWaypoint(Transform @inTransfrom)
		{
			var @inPosition = inTransfrom.position;
			Collider2D[] closetsWay = null;
			//set Vector 2
			var position = new Vector2(inPosition.x, inPosition.y);
			closetsWay = Physics2D.OverlapCircleAll(position, searchRadi, 1 << LayerMask.NameToLayer(Layers.waypoints));
			
			//find neighbors up to 2 plus self
			while(closetsWay.Length < 3 && breakWhile < 10)
			{
				closetsWay = Physics2D.OverlapCircleAll(position, searchRadi, 1 << LayerMask.NameToLayer(Layers.waypoints));
				
				searchRadi +=0.5f;
				breakWhile++;
			}

			if(closetsWay.Length > 0)
			{
                GameObject storedGameObject = null; // initialize storage

                //check it listed waypoint
                foreach(Collider2D found in closetsWay)
                {

                    var distance = (found.transform.position - inTransfrom.position).sqrMagnitude;//check each distance to the calling transform
                    if (distance < 1)//filter results based on a distance of 1 unit
                    {
                        //print("found a closets"); // Debug
                        storedGameObject = found.gameObject; // store results to be returned in outer scope
                    }
                }

                return storedGameObject != null ? storedGameObject : closetsWay[0].gameObject; // if storedGameObject* is no longer void assign found else default value
			}
		    else return null; // if no colliders are found then return empty
		}

	//find the closest waypoint to the player and initialize it
	IEnumerator WaitAndUpdate()
	{
		while(true) // controlled infinite Loop that will always return true when initialized
		{
			//print ("Test WaitAndUpdate");
			GameObject closestToPlayer  = FindClosestsWaypoint(playerTransform);
			closestToPlayer.GetComponent<Waypoint>().initializeData();
			yield return new WaitForSeconds(2f);
		}

	}

	public GameObject GetRandomWaypoint()
	{
		randomWaypoint = Random.Range(0, waypoints.Length - 1);

		return waypoints[randomWaypoint];
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Waypoint : MonoBehaviour 
{
	public List<Collider2D> neighbors;
	public float searchRadi = 0.5f;
	public LayerMask mask = 1 << 10; // can also be written LayerMask.NameToLayer("Name of target layer");
	public List<Transform> playerLocation = new List<Transform>(); // list of directions to get to the player
    public Transform MyTransform;

	private int @breakWhile; 


	void Awake()
	{
        MyTransform = GetComponent<Transform>();
		//set Vector 2
		var position = new Vector2(transform.position.x, transform.position.y);

		//find neighbors up to 2 plus self
		while(neighbors.Count < 3 && breakWhile < 10)
		{
			Collider2D[] col = Physics2D.OverlapCircleAll(position, searchRadi, mask);
			neighbors = new List<Collider2D>(col);
			searchRadi +=1;
			breakWhile++;
		}

		//remove each collider that has a wall between the points
		foreach(Collider2D listed in neighbors)
		{
			Vector2 nTransfrom = new Vector2(listed.transform.position.x, listed.transform.position.y);
			Vector2 myTransform = new Vector2(MyTransform.position.x, MyTransform.position.y);
			RaycastHit2D hitinfo = Physics2D.Linecast(myTransform,nTransfrom, LayerMask.NameToLayer("Waypoints"));
			if(hitinfo.collider.tag == Tags.waypoint)
			{
				Debug.Log ("Removing"+listed+"from list");
								//neighbors.Remove(listed);
			}
		}
	}

	public void initializeData()
	{
		playerLocation.Clear();
		setData(playerLocation);
	}

	//receive data from neighbors

	 void setData(List<Transform> listing)
	{
		// if directions are empty or current list is less than stored list
		if(playerLocation.Count == 0 // if no player location exists
		   || listing[0] != playerLocation[0] // if the list is out dated
		   || listing.Count < playerLocation.Count)// if recieved list is faster
		{
			playerLocation = listing; // put the new list in local
			playerLocation.Add(this.transform); //add ourself to the list
		
		Debug.Log (string.Format("{0} send the data of {1}",this.name, playerLocation[0]));
		sendData(); // send updated list to neighbor
		}
	}

	// sends data to neighbors

	void sendData()
	{

			foreach(Collider2D listed in neighbors)
			{
				if(!playerLocation.Contains(listed.transform))
				listed.GetComponent<Waypoint>().setData(new List<Transform>(playerLocation));
			// new List<Transform>(playerLocation) create a personalized copy of list for this transform

			}
	}

	public List<Transform> GetPlayerLocation()
	{
		return new List<Transform>(playerLocation); //passes the calling gameObject its own  copy* of the list
	}

}

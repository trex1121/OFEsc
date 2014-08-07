using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalEmployeeStat : MonoBehaviour
{
	public List<Transform> ListedAsAtDesk;// find all workers currently at their desks
		
		void Start()
	{
		NotificationCenter.DefaultCenter.AddObserver(this, "AddToAtDesk");
		NotificationCenter.DefaultCenter.AddObserver(this, "LeftDesk");
	}
		// Update is called once per frame
		void Update ()
		{
		//TODO deactivate test keys
		// Testing Block ------------
		if(Input.GetKeyDown(KeyCode.L))
		{
			Debug.Log("------- List of at desk ------");

			foreach(Transform listed in ListedAsAtDesk)
			{

				Debug.Log (listed.name + "is at his desk");
			}
		}
		
		//----------
		}

	void AddToAtDesk(NotificationCenter.Notification employee) // registers employee through notification center
	{
		print(employee.sender.name + " has logged into the intranet" );

		if(!ListedAsAtDesk.Contains((Transform)employee.data[1])) // if employee is not already a part of list
		ListedAsAtDesk.Add((Transform)employee.data[1]); // add to the At Desk List
	}

	void LeftDesk(NotificationCenter.Notification employee) //de-register employee through notification center
	{
		print(string.Format("It is {0} that the item {1} exists the" +
		                    "At Desk data base",ListedAsAtDesk.Contains((Transform)employee.data[1])
		                    ,employee.sender.name));


		if(ListedAsAtDesk.Contains((Transform)employee.data[1])) // if List contains employee
		ListedAsAtDesk.Remove((Transform)employee.data[1]);// de- list

		else // if the employee is not a part of the list you to try to remove them
			print ("[-] "+employee.sender.name+"wasn't listed as at desk");
	}
}


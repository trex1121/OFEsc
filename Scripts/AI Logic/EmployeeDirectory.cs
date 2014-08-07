using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EmployeeDirectory : MonoBehaviour
{
	public Transform closestsEmp;

	private Transform transCache;
	public List<Transform> Employees;

	void Start()
	{
		transCache = GetComponent<Transform>();

		Debug.Log("Looking up People");
		Employees = CompileEmployeeList("People");
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			GetCloseEmployee ();
		}
	}

	List<Transform> CompileEmployeeList(string layer)
	{
		GameObject[] toArray = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		List<GameObject> goArray = toArray.ToList();
		List<Transform> goList = new List<Transform>();
		for (int i = 0; i < goArray.Count; i++) {
		   if (goArray[i].layer == LayerMask.NameToLayer(layer)) {
		     goList.Add(goArray[i].transform);
		   }
		}
		if (goList.Count == 0) 
		{
			Debug.Log("Nobody Found");
		   return null;
		}
			//Debug.Log(goList[0]);
			return goList;
		}

	Transform GetCloseEmployee ()
	{
		//FIXME Could be a performance hit..keep an eye out
		closestsEmp = Employees.Select (person => new {
			person = person,
			position = person.position
		}).Aggregate ((current, next) => (current.position - transCache.position).sqrMagnitude < (next.position - transCache.position).sqrMagnitude ? current : next).person;
		print (this.name + "<color=red>" + "Looked Up the closests person " + closestsEmp.name + "</color>");
		return closestsEmp;
	}
}
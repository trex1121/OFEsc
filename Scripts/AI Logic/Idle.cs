using UnityEngine;
using System;
using System.Collections;

public class Idle : MonoBehaviour
{
	public int energyLevel;
	
	void Start()
	{
		StartCoroutine(sleepDeprive()); // repeating cycle

	}

	///TODO check if each employee is at desk for idle commads and reporting
	public virtual void StartIdle()
	{
		if(energyLevel == 0)
		{
			StartCoroutine(GotoSleep());
		}

	}

	public virtual void StopIdle()
	{
		StopCoroutine("GotoSleep");
		Debug.Log (this.name+" decided to get back to work");
	}

	public IEnumerator sleepDeprive()
	{
		while(true && energyLevel > 0)
		{
			energyLevel--;
			yield return new WaitForSeconds(1f);	
		}
	}

	public IEnumerator GotoSleep()
	{
		Debug.Log (this.name + " "+ "says screw this, time for some shut eye");

		while(energyLevel < 100)
		{
			energyLevel ++;
			yield return new WaitForSeconds(1f);
		}
	}
}


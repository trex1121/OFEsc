/* Needs.cs
 * Defines a characters Essentials
 */
using UnityEngine;
using System.Collections;

public class Needs : MonoBehaviour 
{
	public string myName;
	public bool @isHungry;
	public bool @needsBathroom;

	public bool eating = false;
	public bool urinating = false;

	[HideInInspector]
	public bool addressNeeds;

	public int stomach;
	public int bladder;

	public int fullStomach = 20;

	public int fullBladder = 10;

	void Start () 
	{
		this.name = myName;
		StartCoroutine(Live());
	}
	
	void Update () 
	{
		addressNeeds = isHungry == true ||needsBathroom == true;
	}

	public IEnumerator Eat()
	{
		eating = true; // eating in progress
		Debug.Log (string.Format("{0} is going eat", this.name));

		//eat until stomach is full
		while(isHungry)
		{
			stomach +=1;//eat
			//break loop
			if(stomach == fullStomach)
			{
				eating = false;
				isHungry = false;
			}
			yield return new WaitForSeconds(1f);

		}

	}

	public IEnumerator Urinate()
	{
		urinating = true;//in progress
		Debug.Log (string.Format("{0} is going to bath", this.name));

		//urinate until done
		while(needsBathroom)
		{
			bladder -=1; //empty bladder
			//breakloop
			if(bladder == 0)
			{
				urinating = false;
				needsBathroom = false;
			}
			yield return new WaitForSeconds(1f);
		}
	}//urinate

	IEnumerator Live()//updating status
	{
		while(true)
		{
			if(!eating && stomach !=0)
				stomach --;//become hungry

			if(!urinating && bladder < fullBladder)//if not currently peeing and bladder not empty
				bladder ++;

			if(stomach == 0)
				isHungry = true;

			if(bladder == fullBladder)
				needsBathroom = true;
			yield return new WaitForSeconds(2f);
		}
	}
}

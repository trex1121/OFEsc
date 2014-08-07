using UnityEngine;
using System.Collections;


public class Brain : MonoBehaviour
{
	private Needs myNeeds;
	private AIMovement movement;
	public Work workLogic;
	public bool atDesk;
	public Idle myIdle;

	public enum ThinkingState
	{
		RELIEF,
		IDLE,
		WORK
	}

	 ThinkingState curState = ThinkingState.IDLE;

	void Start()
	{
		movement = GetComponent<AIMovement>();
		myNeeds = GetComponent<Needs>();
		myIdle = GetComponent<Idle>();
	}

	void Update()
	{
		atDesk = this.transform.position == movement.myDesk.position;

		switch(curState)
		{

			case ThinkingState.IDLE:

				if(!myNeeds.addressNeeds 
			   	&& !workLogic.isAttendingMetting || !workLogic.doingPaperWork  && atDesk)
				{
					myIdle.StartIdle();	
				}

				if(myNeeds.addressNeeds || workLogic.meeting || workLogic.paperwork)
				{
					myIdle.StopIdle();
					Debug.Log(string.Format("{0} Moving out of {1}", this.name, curState.ToString()));
					curState = ThinkingState.RELIEF;
				}
				return;


			case ThinkingState.RELIEF:
				//check if relieving is not in progress
				if(!myNeeds.eating || !myNeeds.urinating)
				{
					if(myNeeds.isHungry & !myNeeds.eating & !myNeeds.urinating)//if you are hungry and not currently
					{
						GotoKitchen();
					}

					if(myNeeds.needsBathroom & !myNeeds.urinating & !myNeeds.eating)//if you need to pee and not peeing
					{
						GotoBathroom();//go pee
					}

					if(!myNeeds.isHungry && !myNeeds.needsBathroom)//if you currently don't have to do anything
					{
						Debug.Log(string.Format("{0} Moving out of {1}", this.name, curState.ToString()));
						curState = ThinkingState.WORK;
					}
				}
				return;


			case ThinkingState.WORK:
					//there is a meeting and you aren't attending to attend
					if(workLogic.meeting && !workLogic.isAttendingMetting)//if there is a meeting and your aren't in one
					{
						GotoMeeting();
					}
					//if there is no meeting and you have paper work you aren't doing
					if(!workLogic.meeting && !workLogic.isAttendingMetting 
			   			&& workLogic.paperwork && !workLogic.doingPaperWork)
					{
						GoDoPaperWork();
					}
					//if you are not in a meeting and not doing paper work
					if(!workLogic.meeting && !workLogic.isAttendingMetting && !workLogic.doingPaperWork) //if no working tasks
					{
						Debug.Log(string.Format("{0} Moving out of {1}", this.name, curState.ToString()));
						curState = ThinkingState.IDLE;
					}
				return;

		default:
			Debug.LogError("<color = red>[-] Error no state chosen for AI </color>", this.gameObject);
			break;

		}
	}

	#region Needs
	void GotoKitchen()
	{
		if(movement.path.Count < 1)
		{
			movement.SetTarget(movement.fridge); //go to the fridge
		}
		if(transform.position == movement.fridge.position)//if you have arrived
		{
			StartCoroutine(myNeeds.Eat());//begin eating
		}
	}

	void GotoBathroom()
	{
		if(movement.path.Count < 1)//if not currently traveling on a path
		{
			movement.SetTarget(movement.bathroom); //go to the bathroom
		}
		if(transform.position == movement.bathroom.position)//if you have arrived
		{
			StartCoroutine(myNeeds.Urinate());//begin urinating
		}
	}
	#endregion

	#region Work
	void GotoMeeting()
	{
		if(movement.path.Count < 1)
		{
			movement.SetTarget(movement.boardRoom); //go to the board room
		}
		if(transform.position == movement.boardRoom.position)//if you have arrived
		{
			StartCoroutine(workLogic.AttendMeeting());//begin meeting
		}
	}

	void GoDoPaperWork ()
	{
		if(movement.path.Count < 1)
		{
			movement.SetTarget(movement.myDesk); //go to the board room
		}
		if(transform.position == movement.myDesk.position)//if you have arrived
		{
			StartCoroutine(workLogic.DoPaperWork());//begin meeting
		}
	}
	#endregion
}


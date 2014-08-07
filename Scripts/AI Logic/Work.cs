using UnityEngine;
using System.Collections;

public class Work : MonoBehaviour
{
	public GameObject[] workers;//holds a list of sublevel employees
	public string workersTag = "";//set name of sublevel employees tag

	public bool meeting;
	public bool isAttendingMetting;
	public int meetingTime;//TODO code for specific times
	public short meetingDruation = 5;

	public bool doingPaperWork;
	public int papersLeft;
	public bool paperwork;

	public int GetPapersLeft
	{
		get {return papersLeft;}
		set { papersLeft = value;}
	}

	public bool atDesk;

	private AIMovement mov;
	private Transform mytrans;
	private Hashtable listMe = new Hashtable();

	//TODO added Desk Assignment
	void Start()
	{
		mytrans = GetComponent<Transform>();
		mov = GetComponent<AIMovement>();

		workers = GameObject.FindGameObjectsWithTag(workersTag);
		listMe.Add(1,mytrans);
	
	}

	void Update()
	{
		atDesk = mytrans.position == mov.myDesk.position;
		//TODO take out debug test keys
		if(atDesk && Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log(string.Format("{0} is at work desk ", this.name)); //tell me you are at your desk


			NotificationCenter.DefaultCenter.PostNotification(this,"AddToAtDesk",listMe);
		}
		else if (!atDesk && Input.GetKeyDown(KeyCode.Q))
		{
			NotificationCenter.DefaultCenter.PostNotification(this,"LeftDesk", listMe);
		}

		//TODO deactivate test keys
		// Testing Block ------------
		if(Input.GetKeyDown(KeyCode.M))
		{
			Debug.Log("Set Meeting");
			meeting = true;
		}
		if(Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log("Added 1 Paper");
			papersLeft += 1;
		}
		//----------

		if(papersLeft > 0)
		{
			paperwork = true;
		}

	}

	public virtual IEnumerator AttendMeeting()
	{
		isAttendingMetting = true; // meeting in progress
		Debug.Log ("Attending Meeting");
		
		while(meeting)//if still in meeting
		{
			meetingDruation -=1;//decrease time
			print ("Meeting in Progress");
			if(meetingDruation  < 1)
			{
				isAttendingMetting = false;
				meeting = false;
			}
			yield return new WaitForSeconds(1f);
			if(meeting == false && meetingDruation < 0)
			{
				meetingDruation = 5;
			}
			
		}
	}

	public virtual IEnumerator DoPaperWork()
	{	
		//FIXME if a meeting is set while doing paper work will still do paper work will in meeting
		doingPaperWork = true; // meeting in progress
		Debug.Log (string.Format("{0} is doing paperwork", this.name));
		
		while(paperwork)//if still have paper work
		{
			papersLeft -=1;//decrease time
			print (string.Format("{0} is doing paperwork", this.name));

			if(papersLeft <= 0) //if no papers left
			{
				doingPaperWork = false;
				paperwork = false;
			}

			if(mytrans.position != mov.myDesk.position)
			{
				Debug.Log(string.Format("{0} stopped work to do other things",this.name));
				doingPaperWork = false;
				StopCoroutine("DoPaperWork");
				break;
			}

			yield return new WaitForSeconds(2f);
			
		}
	}
	//TODO check if each employee is at desk for idle commads and reporting
	public virtual IEnumerator StartIdle()
	{

		yield return new WaitForSeconds(1f);
	}
}


/*Author.Terell Carter
 * EnemyVelocity.cs 
 * measures the objects velocity = d/t and returns the struct output
 */
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class UnitVelocity : MonoBehaviour 
{
	public float timeFactor; //refresh rate in seconds for lastpos update
	public float vFactor; //unity units/distance
	private Transform myTrans;

	private float distance;

	private Vector2 curPos;
	private Vector2 lastPos;

	public struct Velocity
	{
		public float x, y;

		public Velocity(float _x, float _y)
		{
			x = _x;
			y = _y;
		}
	}

	public float GetDistance
	{
		get{return distance;}
	}

	public Velocity _vel;

	void Start () 
	{
		myTrans = GetComponent<Transform>();
		curPos = myTrans.position;
		lastPos = curPos;
		//start reading last position
		StartCoroutine(TimedUpdate(timeFactor));
	}
	
	void Update () 
	{
		//set current Vector2 position
		curPos = new Vector2(myTrans.position.x, myTrans.position.y);

		//measure distance between current and last position for reference
		distance = ((curPos - lastPos).sqrMagnitude)/vFactor;

		//calculate x & y velocity
		_vel = new Velocity ((curPos.x - lastPos.x)/vFactor
		                     ,(curPos.y - lastPos.y)/vFactor);

	}

	IEnumerator @TimedUpdate(float t)
	{
		while(true)
		{
			lastPos = curPos;
			//Debug.Log(string.Format("The {0} V2 velocity is: {1}", this.name, distance));
			yield return new WaitForSeconds(t);
		}
	}
}

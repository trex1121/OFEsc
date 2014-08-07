using UnityEngine;
using System.Collections;

public class UnitControl: MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.


	public Animator anim;					// Reference to the player's animator component.
	public Transform wallChecks;
	public bool facingUp;

	void Start()
	{
		anim = GetComponentInChildren<Animator>();	
	}

	
	void FixedUpdate ()
	{


		// Cache the horizontal input.
		float fwdMov = Input.GetAxis("Horizontal");
		float rtMov = Input.GetAxis("Vertical");

		// The Speed animator parameter is set to the absolute value of the horizontal and vertical input for animation.
		anim.SetFloat("FwdSpeed", Mathf.Abs(fwdMov));
		anim.SetFloat("RtSpeed", Mathf.Abs(rtMov));



		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(fwdMov * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * fwdMov * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the player is changing direction (f has a different sign to velocity.y) or hasn't reached maxSpeed yet...
		if(rtMov * rigidbody2D.velocity.y < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.up * rtMov * moveForce);
	

		// If the input is moving the player right and the player is facing left...
		if(fwdMov > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(fwdMov < 0 && facingRight)
			// ... flip the player.
			Flip();

		if(rtMov > 0 && !facingUp)
		{
			//flip the player
			yFlip ();
		}
		else if(rtMov < 0 && facingUp)
		{
			yFlip ();
		}


	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

//		Quaternion theRot = transform.localRotation;
//		if(theRot.z == 0)
//		{
//			theRot.z +=90;
//		}
//		theRot.z *= -1;
//		transform.localRotation = theRot;
	}

	void yFlip()
	{
		/// <summary> Invert Pixel when vertical input is changed.</summary>
		facingUp = !facingUp;

		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;

//		Quaternion theRot = transform.localRotation;
//		if(theRot.z > 0)
//		{
//			theRot.z = 0;
//		}
//
//		transform.localRotation = theRot;

	}

}

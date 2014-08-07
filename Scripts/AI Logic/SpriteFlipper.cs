/*SpriteFlipper.cs
 * takes informaiton from UnitVelocity and applies it to define
 * sprite direction. Also handles animation.
 */
using UnityEngine;
using System.Collections;

public class SpriteFlipper : MonoBehaviour
{
	public UnitVelocity enemyVelocity;
	public Animator anim;

	private bool facingRight;
	private bool facingUp;

	public bool IsRight
	{
		get{return facingRight;}
	}

	public bool IsUP
	{
		get{return facingRight;}
	}

		struct Velocity
	{
		public float x;
		public float y;

		public Velocity(float _x, float _y)
		{
			x = _x;
			y = _y;
		}
	}

	Velocity v;

		void Start ()
		{
			v = new Velocity(0f, 0f);
		}
	
		void Update ()
		{
			//set velocity ref
				v = new Velocity(enemyVelocity._vel.x, enemyVelocity._vel.y);

			//print (v.x.ToString());

			// The Speed animator parameter is set to the absolute value of the horizontal and vertical input for animation.
			anim.SetFloat("HorzSpeed", Mathf.Abs(v.x));
			anim.SetFloat("VertSpeed", Mathf.Abs(v.y));
			
			// If the input is moving the player right and the player is facing left...
			if(v.x > 0 && facingRight)
				// ... flip the player to turn right.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(v.x < 0 && !facingRight)
				// ... flip the player.
				Flip();
			
			if(v.y > 0 && !facingUp)
			{
				//flip the player
				yFlip ();
			}
			else if(v.y < 0 && facingUp)
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

	}
	
	void yFlip()
	{
		/// <summary> Invert Pixel when vertical input is changed.</summary>
		facingUp = !facingUp;
		
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}
}


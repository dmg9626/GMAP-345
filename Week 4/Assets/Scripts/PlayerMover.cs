using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour 
{
	public GameObject playerSprite;

	public float moveSpeed;

	private Rigidbody2D rbody;

	void Start () 
	{
		rbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		// Set initial velocity to 0
		Vector2 velocity = Vector2.zero;

		// Parse input from player as velocity
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			velocity += Vector2.up;
		}
		else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			velocity += Vector2.down;
		}
		if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.A)) {
			velocity += Vector2.left;
		}
		else if(Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.D)) {
			velocity += Vector2.right;
		}

		// Set velocity as normalized vector (max magnitude of 1) scaled by moveSpeed
		rbody.velocity = velocity.normalized * moveSpeed;

		AnimateSprite(velocity.normalized);
	}

	void AnimateSprite(Vector2 movement)
	{
		if(movement.magnitude > 0) {
			playerSprite.transform.localEulerAngles = Vector3.forward * HelperSingleton.instance.angleByVelocity[movement];
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1;

    // private PlayerMover playerMover;
    private Rigidbody2D rigidbody;
    private Animator animator;

	void Start ()
    {
        // playerMover = GetComponent<PlayerMover>();
        rigidbody = GetComponent<Rigidbody2D>();
        // animator = playerMover.animator;
	}
	
	public void GetHit()
    {
        health--;
        
        if (health <= 0)
        {
            rigidbody.velocity = Vector2.zero;
            // playerMover.enabled = false;
            // animator.SetTrigger("Die");
        }
    }
}

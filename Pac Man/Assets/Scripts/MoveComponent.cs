using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Attach this to an Actor to allow movement via execution of MoveCommand

 */

public class MoveComponent : MonoBehaviour {

	/// <summary>
	/// Movement speed
	/// </summary>
	public float moveSpeed = 5;

	/// <summary>
	/// Speed at which animation should play (1 is default speed)
	/// </summary>
	public float animateSpeed = 1;

	/// <summary>
	/// Animator
	/// </summary>
	protected Animator animator;

    /// <summary>
    /// Direction player is currently facing
    /// </summary>
    public BaseConstants.Direction currentDirection;

	protected void Start()
	{
		animator = GetComponent<Animator>();
	}

    public virtual void ManageMovement()
    {
        Debug.LogError(name + " | MoveComponent.MangeMovement() not implemented");
    }

    public virtual void Jump()
    {
        Debug.LogError(name + " | MoveComponent.Jump() not implemented");
    }
}

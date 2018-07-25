using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Side-scrolling MoveComponent

 */

public class SideScrollerMoveComponent : MoveComponent 
{
    /// <summary>
    /// Speed increase while sprinting
    /// </summary>
    public float sprintModifier;

    /// <summary>
    /// Inital upward speed of jump
    /// </summary>
    public float jumpSpeed;

    override
    public void ManageMovement()
    {
        // Parse player input
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Hold down shift to sprint
        float speed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift)) {
            speed += sprintModifier;
        }

        // Move actor in direction of horizontal input
        Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        currentVelocity.x = ParseHorizontalInput(input, speed).x;
        GetComponent<Rigidbody2D>().velocity = currentVelocity;

        // Animate movement according to input
        AnimateWalk(currentVelocity);
    }

    /// <summary>
    /// Makes the player jump
    /// </summary>
    override 
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpSpeed;
    }

    /// <summary>
    /// Parses raw input into horizontal movement (strips vertical component)
    /// </summary>
    /// <returns>Player movmement vector</returns>
    /// <param name="input">Player input</param>
    protected Vector2 ParseHorizontalInput(Vector2 input, float speed)
    {
        return new Vector2(input.x, 0) * speed;
    }

    /// <summary>
    /// Animates movement based on horizontal/vertical input
    /// </summary>
    /// <param name="input">Player input</param>
    protected void AnimateWalk(Vector2 input)
    {
        // Animate walking if receiving input
        if (input.x != 0f) {
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }

        // Get current direction
        currentDirection = InputToDirection(input.x, true);

        // Flip sprite horizontally if facing left
        GetComponent<SpriteRenderer>().flipX = (currentDirection == BaseConstants.Direction.Left);
    }

    /// <summary>
    /// Parses a float representing player input (horizontal or vertical axis) and returns corresponding direction
    /// </summary>
    /// <param name="input">Player input on horizontal/vertical axis</param>
    /// <param name="horizontal">True if input direction is horizontal, false if vertical</param>
    protected BaseConstants.Direction InputToDirection(float input, bool horizontal)
    {
        if (input > 0)
        {
            return horizontal ? BaseConstants.Direction.Right : BaseConstants.Direction.Up;
        }
        else
        {
            return horizontal ? BaseConstants.Direction.Left : BaseConstants.Direction.Down;
        }
    }
}

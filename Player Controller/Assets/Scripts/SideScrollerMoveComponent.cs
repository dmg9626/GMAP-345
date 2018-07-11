using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerMoveComponent : MoveComponent {

    public void ManageMovement(Vector2 input)
    {
        // Move actor in direction of input
        Vector2 movement = input * moveSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = movement;

        // Animate walking if receiving input
        if (input.x != 0f || input.y != 0f)
        {
            animator.SetBool("Moving", true);
            AnimateWalk(input);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    /// <summary>
    /// Animates movement based on horizontal/vertical input
    /// </summary>
    /// <param name="input">Player input</param>
    protected void AnimateWalk(Vector2 input)
    {
        // Get current direction
        currentDirection = (BaseConstants.Direction)animator.GetInteger("Direction");

        // Calculate and set new direction
        BaseConstants.Direction newDirection = ParseInput(input.x, true);
        animator.SetInteger("Direction", (int)newDirection);
    }

    /// <summary>
    /// Parses a float representing player input (horizontal or vertical axis) and returns corresponding direction
    /// </summary>
    /// <param name="input">Player input on horizontal/vertical axis</param>
    /// <param name="horizontal">True if input direction is horizontal, false if vertical</param>
    protected BaseConstants.Direction ParseInput(float input, bool horizontal)
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

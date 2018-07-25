using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMoveComponent : MoveComponent {

	// Use this for initialization
	void Start () {
        base.Start();

        animator.speed = animateSpeed;
	}

    /// <summary>
    /// Moves/animates actor based on button presses
    /// </summary>
    override
    public void ManageMovement()
    {
        // Get player input
        Vector2 input;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            input = Vector2.up;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            input = Vector2.left;
        }
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            input = Vector2.down;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            input = Vector2.right;
        }
        else {
            input = DirectionHelper.DirectionToVector(currentDirection);
        }
        
        // Move actor in direction of input
        Vector2 movement = input * moveSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = movement;

        // Save direction to use next frame
        currentDirection = DirectionHelper.VectorToDirection(input);

        // Animate walking if receiving input
        if (input.x != 0f || input.y != 0f)
        {
            AnimateWalk(input);
        }
    }

    

    /// <summary>
    /// Animates movement based on horizontal/vertical input
    /// </summary>
    /// <param name="input">Player input</param>
    protected void AnimateWalk(Vector2 input) 
    {
        // Get current direction
        // currentDirection = (BaseConstants.Direction)animator.GetInteger("Direction");

        // Calculate and set new direction
        BaseConstants.Direction newDirection = FaceDirection(input);

        // TODO: set direction by rotation, not animator
        // animator.SetInteger("Direction", (int)newDirection);
    }

    /// <summary>
    /// Calculates direction for player to face based on horizontal/vertical input
    /// </summary>
    /// <param name="horizontal">Horizontal input</param>
    /// <param name="vertical">Vertical input</param>
    /// <returns>Direction to face</returns>
    protected BaseConstants.Direction FaceDirection(Vector2 input)
    {
        // If moving diagonally, continue facing along axis of currentDirection
        if(input.x != 0 && input.y != 0){
            if(DirectionHelper.IsVertical(currentDirection)) {
                return ParseInput(input.y, false);
            }
            else {
                return ParseInput(input.x, true);
            }
        }

        // Otherwise face in direction of raw input
        else if(input.x != 0) {
            return ParseInput(input.x, true);
        }
        else if(input.y != 0) {
            return ParseInput(input.y, false);
        }

        return currentDirection;
    }

    /// <summary>
    /// Parses a float representing player input (horizontal or vertical axis) and returns corresponding direction
    /// </summary>
    /// <param name="input">Player input on horizontal/vertical axis</param>
    /// <param name="horizontal">True if input direction is horizontal, false if vertical</param>
    protected BaseConstants.Direction ParseInput(float input, bool horizontal)
    {
        if (input > 0) {
            return horizontal ? BaseConstants.Direction.Right : BaseConstants.Direction.Up;
        }
        else {
            return horizontal ? BaseConstants.Direction.Left : BaseConstants.Direction.Down;
        }
    }
}

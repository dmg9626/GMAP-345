using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMoveComponent : MoveComponent {

    private Rigidbody2D rbody;

    public Intersection intersection;

	// Use this for initialization
	void Start () {
        base.Start();

        animator.speed = animateSpeed;

        rbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves/animates actor based on button presses
    /// </summary>
    override
    public void ManageMovement()
    {
        //Vector2 input = GetInput();

        //// Move actor in direction of input
        //Vector2 movement = input * moveSpeed;
        //rbody.velocity = movement;

        //// Save direction to use next frame
        //currentDirection = DirectionHelper.VectorToDirection(input);

        //// Animate walking if receiving input
        //if (!currentDirection.Equals(BaseConstants.Direction.None))
        //{
        //    AnimateMovement();
        //}



        Vector2 input = GetInput();

        BaseConstants.Direction direction = DirectionHelper.VectorToDirection(input);

        if (direction != BaseConstants.Direction.None && CanMove(direction)) {
            Debug.Log("Can move " + direction);

            Move(direction);

            // Animate walking if receiving input
            if (!currentDirection.Equals(BaseConstants.Direction.None)) {
                AnimateMovement();
            }
        }
    }

    protected Vector2 GetInput()
    {
        // Get player input
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            return Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            return Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            return Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            return Vector2.right;
        }
        return input;
    }

    protected void Move(BaseConstants.Direction direction)
    {
        // Move actor in direction of input
        Vector2 movement = DirectionHelper.DirectionToVector(direction) * moveSpeed;
        rbody.velocity = movement;

        // Save direction to use next frame
        currentDirection = direction;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Intersection intersection = collision.gameObject.GetComponent<Intersection>();
        if (intersection != null) {
            Debug.Log("Reached intersection: " + intersection.name);
            Debug.Log("Valid directions: " + intersection.validDirections.Count);

            this.intersection = intersection;

            if(!CanMove(currentDirection)) {
                Move(BaseConstants.Direction.None);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        intersection = null;
    }

    /// <summary>
    /// Returns true if player can move in given direction at this intersection, false otherwise
    /// </summary>
    /// <param name="direction">The direction.</param>
    /// <param name="intersection">The intersection.</param>
    protected bool CanMove(BaseConstants.Direction direction)
    {
        if(intersection != null) {
            return intersection.validDirections.Contains(direction);
        }
        return false;
    }

    

    /// <summary>
    /// Animates movement based on horizontal/vertical input
    /// </summary>
    /// <param name="input">Player input</param>
    protected void AnimateMovement() 
    {
        // Calculcate angle of rotation based on current direction
        Vector3 rotation = new Vector3(0, 0, DirectionHelper.DirectionToRotation(currentDirection) ?? 0);

        // Assign to quaternion.eulerAngles
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotation;
        transform.rotation = rot;
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

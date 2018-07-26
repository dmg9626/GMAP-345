using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMoveComponent : MoveComponent {

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

        if (direction != BaseConstants.Direction.None) {
            Debug.Log("Can move " + direction);

            Move(direction);

            // Animate walking if receiving input
            if (!currentDirection.Equals(BaseConstants.Direction.None)) {
                AnimateMovement();
            }
        }
    }

    /// <summary>
    /// Returns player input from arrow keys/WASD
    /// </summary>
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

    /// <summary>
    /// Moves player in specified direction.
    /// </summary>
    /// <param name="direction">The direction.</param>
    protected void Move(BaseConstants.Direction direction)
    {
        // Move actor in direction of input
        Vector2 movement = DirectionHelper.DirectionToVector(direction) * moveSpeed;
        rbody.velocity = movement;

        // Save direction to use next frame
        currentDirection = direction;
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Intersection intersection = collision.gameObject.GetComponent<Intersection>();
    //    if (intersection != null) {
    //        Debug.Log("Reached intersection: " + intersection.name);
    //        Debug.Log("Valid directions: " + intersection.validDirections.Count);

    //        this.intersection = intersection;

    //        if(!CanMove(currentDirection)) {
    //            Move(BaseConstants.Direction.None);
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    intersection = null;
    //}

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

        // Assign rotation to quaternion.eulerAngles
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotation;
        transform.rotation = rot;
    }
}

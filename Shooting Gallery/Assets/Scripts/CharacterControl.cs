using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    /// <summary>
    /// CharacterController
    /// </summary>
    public CharacterController cController;
    
    /// <summary>
    /// Reference to forward direction
    /// </summary>
    public Transform forward;

    /// <summary>
    /// Movement speed
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Initial upward speed of jump
    /// </summary>
    public float jumpSpeed;

    /// <summary>
    /// Gravity pulling player back down from jump
    /// </summary>
    public float gravity;
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;

        if(cController.isGrounded) {
            // Jump
            if(Input.GetKeyDown(KeyCode.Space)) {
                Debug.Log("Jumping");
                velocity.y = jumpSpeed;
            }

            // Apply downwards force while grounded to improve collision with ground
            //else {
            //    velocity.y = -1;
            //}
        }
        // Apply falling gravity
        //else {
            velocity.y -= gravity;
        //}

        // Move in direction of velocity
        cController.Move(forward.TransformDirection(velocity) * Time.deltaTime);
	}
}

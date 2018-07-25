using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This command takes directional input and moves/animates the actor accordingly. It requires the actor to
have a MoveComponent and an Animator/AnimationController configured for movement.

At the moment it only works for player input - some reworking is necessary for this command to be used for
handling movement of NPCs

 */

public class MoveCommand : Command {
	public override void execute(Actor actor) {

		// Get player input
        // TODO: design this command to handle enemy AI movement as well
        //float vertical = Input.GetAxis("Vertical");
        //float horizontal = Input.GetAxis("Horizontal");

        GameController.LogCommands.Log("Executing command " + name);


        MoveComponent moveComponent = actor.GetComponent<MoveComponent>();

        // Check for attached MoveComponent
        if(moveComponent != null)
        {
            // Move/animate player
            moveComponent.ManageMovement();
        }
        else
        {
            GameController.LogCommands.LogWarning("Unable to execute MoveCommand on actor " + actor.name + " - no MoveComponent found");
        }
        
	}
}

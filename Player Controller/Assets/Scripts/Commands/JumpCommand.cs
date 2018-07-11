using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command {


	public JumpCommand() { type = Type.MOVEMENT; }

	public override void execute(Actor actor) {

        MoveComponent moveComponent = actor.GetComponent<MoveComponent>();

        // Check for attached MoveComponent before jumping
        if(moveComponent == null) {
            GameController.LogCommands.LogWarning("Unable to execute JumpCommand on actor " + actor.name + " - no MoveComponent found");
			return;
        }

		// Jump if player presses key
		if(Input.GetKeyDown(KeyCode.Space)) {
            moveComponent.Jump();
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/*

Place this on a GameObject in the scene to allow player control

All player input (behavior defined as Commands) is executed through the PlayerController. When these Commands 
are executed, the controlled Actor is passed as an argument. This allows simple commands such as movement or 
jumping to be simply executed on a variety of Actors with little to no fine-tuning.

Each available command (stored in the controlled Actor's Action Queue) is executed each frame. If you only want
the behavior to trigger at a specific moment - such as when the player presses a button - those checks should be
made within the Command definition (usually in Command.execute()).

This design is best suited for a game which features switching control between characetrs, but can also prove 
useful for programmoing behavior to be used across many different actors.

If no actor is assigned at Start(), it searches the scene for an actor with isPlayer = true

 */

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Actor player is currently controlling
    /// </summary>
    public Actor controlledActor;

    /// <summary>
    /// Collection of commands to execute
    /// </summary>
    protected List<Command> commands;

    void Start()
    {
        // Instantiate commands
        Command shootCommand = new ShootCommand();
        Command moveCommand = new MoveCommand();

        // TODO: do this more dynamically
        // TODO: pull commands from Actor, rather than operating off same set of commands for each actor
        commands = new List<Command> { shootCommand, moveCommand };

        // Set actor to player if null
        if(controlledActor == null)
        {
            SetActor(FindPlayer());
        }
    }

    /// <summary>
    /// Returns reference to Player actor in scene (assumes isPlayer = true)
    /// </summary>
    public Actor FindPlayer()
    {
        // TODO: check if player actor not found
        return GameObject.FindObjectsOfType<Actor>().Where(i => i.isPlayer).First() as Actor;
    }

    void Update () 
    {
        // Iterate through each commandType in action queue and execute corresponding command
        foreach(Command.Type commandType in controlledActor.actionQueue)
        {
            commands.Find(i => i.type == commandType).execute(controlledActor);
        }

        // No longer have to execute each command manually
        //moveCommand.execute(actor);
        //shootCommand.execute(gameObject);
    }

    /// <summary>
    /// Sets the actor controlled by player input
    /// </summary>
    /// <param name="actor">Actor</param>
    public void SetActor(Actor actor)
    {
        this.controlledActor = actor;
        Debug.Log("Changed actor to " + actor.name);
    }
   
    /// <summary>
    /// Destroys the actor gameobject and spawns in a new one to replace it
    /// </summary>
    /// <param name="actor">Actor to kill</param>
    public void KillActor(Actor actor)
    {
        Debug.Log(actor.name + " died");

        // Spawn a clone of the actor before destroying (for demo purposes only)
        CloneActor(actor);

        // Destroy the old one
        Destroy(actor.gameObject);

        // Find another actor in scene to control if you killed yourself (for demo purposes only)
        // TODO: remove/change this for actual game; player death should mean game over
        if (GameController.PlayerController.controlledActor == actor)
        {
            Actor newControlActor = FindObjectOfType<Actor>();
            GameController.PlayerController.SetActor(newControlActor);
        }
    }

	/// <summary>
    /// Spawns clone of provided actor
    /// </summary>
    /// <param name="actor">Actor</param>
    private void CloneActor(Actor actor)
    {
        // Reference to prefab
        UnityEngine.Object actorPrefab;

        // Select between Player and Actor prefabs
        if (actor.isPlayer)
        {
            actorPrefab = Resources.Load("Prefabs/Player");
        }
        else
        {
            actorPrefab = Resources.Load("Prefabs/Actor");
        }

        // Instantiate actor from prefab
        Actor newActor = Instantiate(actorPrefab as GameObject).GetComponent<Actor>();

        // Move it over a bit to distinguish it from dead actor
        Vector2 offset = new Vector2(UnityEngine.Random.Range(-1F, 1F), UnityEngine.Random.Range(-1F, 1F));
        newActor.transform.position += (Vector3)offset;
    }

}
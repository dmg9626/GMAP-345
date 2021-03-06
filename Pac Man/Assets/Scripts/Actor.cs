﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

An Actor is a controllable entity in the scene, either the player or someone else. 

The only difference between the player character and an identically-configured actor is whether 'isPlayer' is set 
to true or false. 

Ideally this class should also be used for enemies, even if they shouldn't be controlled (add a 'controllable' 
bool to class to handle this)

*/

public class Actor : MonoBehaviour {
	/// <summary>
	/// Actor health
	/// </summary>
	public float health;
	
	/// <summary>
	/// True if actor is player character
	/// </summary>
	public bool isPlayer;

    /// <summary>
    /// Contains objects that are used by the actor
    /// </summary>
    public List<GameObject> inventory;

    /// <summary>
    /// List of commands the actor will perform (used for filtering commands by type)
    /// </summary>
    public List<Command.Type> actionQueue;

    /// <summary>
    /// List of commands the actor can execute
    /// </summary>
    public List<Command> commands;
}

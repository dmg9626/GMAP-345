using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

GameController

This should always be present in the scene, ideally attached to a GameObject that won't be destroyed between
scenes (but I haven't tested functionality between scenes). 

The GameController is a static class that can be used to access the PlayerController (and through it the player 
actor/controlled actor), LogHelpers, and anything else that should be globally available. 

The LogHelpers (LogPhysics, LogCommands, etc) are called from other classes to log specific information to the
console. The associated boolean values (logPhysics, logCommand, etc) can be enabled/disabled to show/hide 
messages logged through that logger; this allows developers to filter out unrelated log messages when debugging.

 */

public class GameController : MonoBehaviour {
	[Header("Loggers")]
    /// <summary>
    /// Physics logs will display in console if true
    /// </summary>
	public bool logPhysics;
    
    /// <summary>
    /// Command logs will display in console if true
    /// </summary>
    public bool logCommand;

	void Start()
	{
        // Initialize loggers
		LogPhysics = new LogHelper();
        LogCommands = new LogHelper();
        InitializeLoggers();

        // Player Controller
        PlayerController = GetComponent<PlayerController>();
	}

	private void InitializeLoggers()
	{
		LogPhysics.SetLogging(logPhysics);
        LogCommands.SetLogging(logCommand);
	}
	/// <summary>
	/// Physics logger
	/// </summary>
	public static LogHelper LogPhysics;

    /// <summary>
    /// Command logger
    // Ideally this should allow filtering by Command and Actor executing it
    /// </summary>
    public static LogHelper LogCommands;

    /// <summary>
    /// Player controller
    /// </summary>
    public static PlayerController PlayerController { get; private set; }

    /// <summary>
    /// Score granted for each pellet obtained
    /// </summary>
    public int pelletScore;

    /// <summary>
    /// Score granted for each power up obtained
    /// </summary>
    public int powerUpScore;

    /// <summary>
    /// Power up move speed increase
    /// </summary>
    public float powerUpSpeedIncrease;

    /// <summary>
    /// Score granted for each ghost killed
    /// </summary>
    public int ghostKillScore;

}

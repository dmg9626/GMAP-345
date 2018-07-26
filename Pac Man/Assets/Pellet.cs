using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour 
{
    protected GameController gameController;

    /// <summary>
    /// True if this is a powerup pellet
    /// </summary>
    public bool powerUp;

	// Use this for initialization
	void Start ()
    {
        gameController = GameController.FindObjectOfType<GameController>();	
	}

    
}

using System.Collections;
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
    /// List of commands available to the actor
    /// </summary>
    public List<Command.Type> actionQueue;

    /// <summary>
    /// Text label displayed below actor (TODO: remove this from future projects)
    /// </summary>
    protected TextMesh textMesh;

    /// <summary>
    /// Called on left mouse click
    /// </summary>
    void OnMouseDown()
    {
        // Assign this actor to PlayerController
        GameController.PlayerController.SetActor(this);
    }

    private void Start()
    {
        // Initialize text label below actor
        textMesh = transform.GetComponentInChildren<TextMesh>();
        UpdateLabel();
    }

    private void Update()
    {
        // Kill self when health reaches 0
        if(health <= 0) {
            GameController.PlayerController.KillActor(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name + " was hit by " + collision.gameObject.name);

        // Check for projectile
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if(projectile != null) {
            Debug.Log("Projectile: " + projectile.name);

            // Decrement health if projectile shot by different actor
            if(projectile.actor != this) {
                health -= projectile.damage;

                GameObject.Destroy(projectile.gameObject);
                Debug.Log("Health: " + health);

                // Update label with reduced health
                UpdateLabel();
            }
        }
    }

    /// <summary>
    /// Updates text label below actor
    /// </summary>
    public void UpdateLabel()
    {
        textMesh.text = name + "\nHealth: " + health;
    }
}

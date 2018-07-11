using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Add a GameObject with a Projectile component to the Inventory of an Actor to allow him to shoot it.

Ideally a Projectile would fire through a Weapon (maybe even a Clip if you really wanna over-engineer it),
but for the purposes of this demo just throwing a Projectile is fine.

 */

public class Projectile : MonoBehaviour {

    /// <summary>
    /// Time to wait for collision before disappearing
    /// </summary>
    public float lifeSpan;

    /// <summary>
    /// Time counter
    /// </summary>
    private float time = 0;

    /// <summary>
    /// Projectile speed
    /// </summary>
    public float speed;

    /// <summary>
    /// Damage dealt
    /// </summary>
    public float damage;

    /// <summary>
    /// Actor that shot the projectile
    /// </summary>
    public Actor actor;
	
	void Update () {
		if(time > lifeSpan) {
			Destroy(gameObject);
		}
		else {
			time += Time.deltaTime;
		}
	}

    /// <summary>
    /// Shoots projectile in given direction
    /// </summary>
    /// <param name="vector">Vector</param>
    /// <param name="actor">Actor that shot projectile</param>
    public void Shoot(Vector2 vector, Actor actor)
    {
        // Set velocity
        GetComponent<Rigidbody2D>().velocity = vector * speed;
        GameController.LogPhysics.Log("Trajectory: " + vector);

        // Set rotation (flip 90 degrees if needed)
        if (!DirectionHelper.IsVertical(DirectionHelper.VectorToDirection(vector)))
        {
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, 0, 90F);
            transform.rotation = rotation;
        }

        // Save reference to actor so his bullets don't damage him
        this.actor = actor;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour {

    /// <summary>
    /// Minimum time to wait between shots
    /// </summary>
    public float fireRate;

    /// <summary>
    /// Counts time until next shot
    /// </summary>
    private float fireTimer;

    /// <summary>
    /// Reference to projectile prefab
    /// </summary>
    public GameObject projectile;

    /// <summary>
    /// Position projectile should fire from
    /// </summary>
    public Transform fireLocation;

    /// <summary>
    /// Speed at which projectile travels
    /// </summary>
    public float projectileSpeed;

    /// <summary>
    /// Next projectile to spawn
    /// </summary>
    GameObject newProjectile;


	// Use this for initialization
	void Start () {
        // Allow player to fire at game start
        fireTimer = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
        // Increment timer
        fireTimer += Time.deltaTime;

        // Check if fireRate has elapsed
        if(fireTimer >= fireRate) {
            
            // Fire on left mouse click
            if(Input.GetMouseButton(0)) {
                Fire();
            }
        }
	}

    /// <summary>
    /// Fires a new projectile
    /// </summary>
    void Fire()
    {
        // Spawn new projectile
        newProjectile = Instantiate(projectile, fireLocation.position, fireLocation.rotation);

        // Fire it forward
        newProjectile.GetComponent<Rigidbody>().velocity = fireLocation.forward * projectileSpeed * Time.deltaTime;

        // Destroy after 5 seconds
        Destroy(newProjectile, 5);

        // Reset timer to start counting up to next shot
        fireTimer = 0;
    }
}

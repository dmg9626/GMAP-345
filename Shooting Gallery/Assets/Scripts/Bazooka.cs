﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>
    /// Highlight around barrel of gun
    /// </summary>
    public MeshRenderer highlight;

    [Serializable]
    public class Magazine {
        /// <summary>
        /// Amount of bullets in magazine
        /// </summary>
        public float clipSize;

        /// <summary>
        /// Current ammo count
        /// </summary>
        public float currentAmmo;

        /// <summary>
        /// Duration of reloading
        /// </summary>
        public float reloadDuration;

        /// <summary>
        /// Displays amount of ammo in magazine
        /// </summary>
        public Text ammoText;
    }

    public Magazine magazine;

    /// <summary>
    /// True if currently reloading
    /// </summary>
    bool reloading;


	// Use this for initialization
	void Start ()
    {
        // Allow player to fire at game start
        fireTimer = fireRate;

        // Start with full mag of ammo
        magazine.currentAmmo = magazine.clipSize;

        reloading = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Increment timer
        fireTimer += Time.deltaTime;

        // Check if fireRate has elapsed
        if(fireTimer >= fireRate) {
            
            // Fire on left mouse click
            if(Input.GetMouseButton(0)) {

                // Check if we have ammo
                if(magazine.currentAmmo > 0) {
                    Fire();

                    //SetMaterialAlpha(highlight, 1);
                }
                // Reload if not currently reloading
                else if(!reloading) {
                    StartCoroutine(Reload());
                }
            }
        }

        magazine.ammoText.text = "Ammo: " + magazine.currentAmmo;
	}

    /// <summary>
    /// Sets currentAmmo to clipSize after waiting reloadDuration
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reload()
    {
        // Make sure we don't call this Coroutine repeatedly and blow up the stack
        reloading = true;

        Debug.Log("Reloading gun...");

        // Wait reloadDuration seconds
        yield return new WaitForSeconds(magazine.reloadDuration);

        // Set ammo to full clip size
        magazine.currentAmmo = magazine.clipSize;
        Debug.Log("Finished reloading");

        reloading = false;
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

        // Decrement ammo count
        magazine.currentAmmo--;

        Debug.Log("Ammo left: " + magazine.currentAmmo);
    }

    /// <summary>
    /// Changes alpha value on trim material to create a flashing appearance
    /// </summary>
    /// <param name="mRenderer">Mesh renderer to alter material of.</param>
    /// <param name="alpha">Alpha value.</param>
    void SetMaterialAlpha(MeshRenderer mRenderer, float alpha)
    {
        Color color = mRenderer.material.color;
        color.a = alpha;
        mRenderer.material.color = color;
    }
}

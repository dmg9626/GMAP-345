using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 rotationPerSecond;

    ParticleSystem pSystem;

    public Color startColor;

    public Color endColor;

    void Start()
    {
        pSystem = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(rotationPerSecond * Time.deltaTime, Space.World);

        SetParticleEffect();
    }

    /// <summary>
    /// Sets the particle effect using specified color settings
    /// </summary>
    void SetParticleEffect()
    {
        // Create gradient
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(startColor.a, 0.0f), new GradientAlphaKey(endColor.a, 1.0f) }
        );

        // Get reference to color over time module (requires stepping through each component)
        ParticleSystem.MainModule main = pSystem.main;
        ParticleSystem.ColorOverLifetimeModule colorModule = pSystem.colorOverLifetime;

        // Assign gradient to module
        colorModule.color = gradient;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collided with Target gameobject
        if(collision.gameObject.CompareTag("Target")) {
            Debug.Log("Hit gameobject with tag " + collision.gameObject.tag);

            // Destroy object
            Destroy(collision.gameObject);
        }
    }


}

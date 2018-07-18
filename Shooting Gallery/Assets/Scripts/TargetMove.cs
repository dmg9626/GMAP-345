using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour 
{
    /*
     * 
     * Target travels back and forth between point A and B 
     */ 

    /// <summary>
    /// Point A of path
    /// </summary>
    public Transform pointA;

    /// <summary>
    /// Point B of path
    /// </summary>
    public Transform pointB;

    /// <summary>
    /// Move speed
    /// </summary>
    public float moveSpeed;

    Rigidbody rBody;


    // Use this for initialization
    void Start ()
    {
        transform.position = pointA.position;

        rBody = GetComponent<Rigidbody>();

        // Going to position B form A
        rBody.velocity = (pointB.position - pointA.position) * moveSpeed * Time.deltaTime;

	}
	
	// Update is called once per frame
	void Update ()
    {
        // Moving towards A
        if(rBody.velocity.z < 0) {
            if(transform.position.z < pointA.position.z) {
                //Debug.Log("Target Passed A");

                // Move in oppposite direction
                rBody.velocity *= -1;
            }
        }

        // Moving towards B
        else if(rBody.velocity.z > 0) {
            if(transform.position.z > pointB.position.z) {
                //Debug.Log("Target Passed B");

                // Move in oppposite direction
                rBody.velocity *= -1;
            }
        }
	}
}

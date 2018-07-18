﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform player;

    public float mouseSensitivity;

    public Transform cameraTilt;

    public float minTilt;

    public float maxTilt;
	
	// Update is called once per frame
	void Update ()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Set position to player
        transform.position = player.position;

        RotateLocal(player.transform, new Vector3(0, mouseX, 0));
        RotateWorld(cameraTilt.transform, transform.TransformDirection(new Vector3(-mouseY, 0, 0)));
        RotateLocal(transform, new Vector3(0, mouseX, 0));
    
        //transform.eulerAngles = player.transform.eulerAngles;

        //Rotate(cameraTilt.transform, new Vector3(mouseY, 0, 0));

        //Debug.Log("Mouse Y: " + mouseY);
    }

    /// <summary>
    /// Rotates relative to local space
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateLocal(Transform tform, Vector3 rotation)
    {
        if(tform.eulerAngles.x > maxTilt) {
            Debug.Log(tform.name + ": PASSED MAX TILT " + tform.eulerAngles.x);
        }
        else if (tform.eulerAngles.x < minTilt) {
            Debug.Log(tform.name + ": PASSED MIN TILT " + tform.eulerAngles.x);
        }

        rotation *= mouseSensitivity;

        tform.eulerAngles += rotation;
    }

    /// <summary>
    /// Rotates relative to world space
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateWorld(Transform tform, Vector3 rotation)
    {
        if (tform.eulerAngles.x > maxTilt) {
            Debug.Log(tform.name + ": PASSED MAX TILT " + tform.eulerAngles.x);
        }
        else if (tform.eulerAngles.x < minTilt) {
            Debug.Log(tform.name + ": PASSED MIN TILT " + tform.eulerAngles.x);
        }

        rotation *= mouseSensitivity;

        tform.Rotate(rotation.x, rotation.y, rotation.z, Space.World);
    }
}

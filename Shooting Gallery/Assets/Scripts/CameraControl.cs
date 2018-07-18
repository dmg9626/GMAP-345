using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform player;

    public float mouseSensitivity;

    public Transform cameraTilt;

    public Transform gun;

    public float minTilt;

    public float maxTilt;
	
	// Update is called once per frame
	void Update ()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate camera yaw (left/right rotation) and tilt (up/down rotation)
        Vector3 yaw = new Vector3(0, mouseX, 0);
        Vector3 tilt = transform.TransformDirection(new Vector3(-mouseY, 0, 0));

        // Set position to player
        transform.position = player.position;

        // Rotate player left/right
        RotateLocal(player.transform, yaw);

        // Camera yaw left/right
        RotateLocal(transform, yaw);

        // Rotate gun up/down
        RotateWorld(gun.transform, tilt);

        // Camera tilt up/down
        RotateWorld(cameraTilt.transform, tilt);

        
    }

    /// <summary>
    /// Rotates relative to local space
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateLocal(Transform tform, Vector3 rotation)
    {
        
        //Debug.Log(tform.name + " yaw: " + tform.eulerAngles.y);
        //if(tform.eulerAngles.x > maxTilt) {
        //    Debug.Log(tform.name + ": PASSED MAX TILT " + tform.eulerAngles.x);
        //}
        //else if (tform.eulerAngles.x < minTilt) {
        //    Debug.Log(tform.name + ": PASSED MIN TILT " + tform.eulerAngles.x);
        //}

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
        Vector3 currentRotation = tform.eulerAngles;

        if (currentRotation.x > 180) {
            //Debug.Log("Reducing tilt from " + currentRotation.x + " to " + (currentRotation.x - 360));
            currentRotation.x -= 360;
        }

        if (currentRotation.x + rotation.x > maxTilt) {
            //Debug.Log(tform.name + ": PASSED MAX TILT " + (currentRotation.x + rotation.x));

            // clamp current rotation between min tilt and max tilt
            currentRotation.x = maxTilt;

            // prevent user from looking down any further
            rotation.x = Mathf.Clamp(rotation.x, -180, 0);

        }
        else if (currentRotation.x + rotation.x < minTilt) {

            // clamp current rotation between min tilt and max tilt
            currentRotation.x = minTilt;

            rotation.x = Mathf.Clamp(rotation.x, 0, 180);
        }

        rotation *= mouseSensitivity;

        tform.Rotate(rotation.x, rotation.y, rotation.z, Space.World);
    }
}

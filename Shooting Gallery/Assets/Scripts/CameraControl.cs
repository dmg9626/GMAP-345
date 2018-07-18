using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform player;

    public float mouseSensitivity;

    public Transform cameraTilt;

    public Transform gun;

    public float topViewClamp;

    public float bottomViewClamp;
	
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
    /// Rotates relative to local space (used primarily for horizontal look)
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateLocal(Transform tform, Vector3 rotation)
    {
        rotation *= mouseSensitivity;

        tform.eulerAngles += rotation;
    }

    /// <summary>
    /// Rotates relative to world space (used primarily for vertical look)
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateWorld(Transform tform, Vector3 rotation)
    {
        // Get current rotation between range 180 and -180
        Vector3 currentRotation = tform.eulerAngles;

        if (currentRotation.x > 180) {
            currentRotation.x -= 360;
        }

        // Check if player is trying to look too low
        if (currentRotation.x + rotation.x > bottomViewClamp) {

            // Clamp current rotation between min tilt and max tilt
            currentRotation.x = bottomViewClamp;

            // Prevent user from looking down any further
            rotation.x = Mathf.Clamp(rotation.x, -180, 0);
        }

        // Check if player is trying to look too high
        else if (currentRotation.x + rotation.x < topViewClamp) {

            // Clamp current rotation between min tilt and max tilt
            currentRotation.x = topViewClamp;

            rotation.x = Mathf.Clamp(rotation.x, 0, 180);
        }

        // Scale rotation by mouse sensitivity
        rotation *= mouseSensitivity;

        // Rotate transform
        tform.Rotate(rotation.x, rotation.y, rotation.z, Space.World);
    }
}

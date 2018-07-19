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
        Vector3 tilt = new Vector3(-mouseY, 0, 0);

        // Set position to player
        transform.position = Vector3.Lerp(transform.position, player.position, .25f);

        // Rotate player left/right
        RotateWorld(player.transform, yaw);

        // Camera yaw left/right
        RotateWorld(transform, yaw);

        // Rotate gun up/down
        RotateLocal(gun.transform, tilt);

        // Camera tilt up/down
        RotateLocal(cameraTilt.transform, tilt);
    }

    /// <summary>
    /// Rotates relative to local space (used primarily for horizontal look)
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateLocal(Transform tform, Vector3 rotation)
    {
        if(rotation.x != 0) {
            // Clamp rotation to vertical look limits
            rotation = ClampRotation(tform, rotation);
        }
        
        // Scale by mouse sensitivity
        rotation *= mouseSensitivity;

        // Directly apply local rotation to transform
        tform.localEulerAngles += rotation;
    }

    /// <summary>
    /// Rotates relative to world space (used primarily for vertical look)
    /// </summary>
    /// <param name="tform">Transform to rotate.</param>
    /// <param name="rotation">Rotation vector.</param>
    void RotateWorld(Transform tform, Vector3 rotation)
    {
        if(rotation.x != 0) {
            // Clamp rotation to vertical look limits
            rotation = ClampRotation(tform, rotation);
        }

        // Scale by mouse sensitivity
        rotation *= mouseSensitivity;

        // Rotate transform
        tform.Rotate(rotation.x, rotation.y, rotation.z, Space.World);
    }

    /// <summary>
    /// Clamps vertical rotation between topViewClamp and bottomViewClamp
    /// </summary>
    /// <param name="tform">Transform</param>
    /// <param name="rotation">Raw rotation input</param>
    /// <returns></returns>
    Vector3 ClampRotation(Transform tform, Vector3 rotation) 
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

        return rotation;
    }
}

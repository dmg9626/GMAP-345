using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform player;

    public float mouseSensitivity;
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position;
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0, Space.World);

        player.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0, Space.World);
	}
}

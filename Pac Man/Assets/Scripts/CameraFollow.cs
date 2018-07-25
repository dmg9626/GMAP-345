using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	/// <summary>
	/// Actor to follow
	/// </summary>
	public Actor actor;

	/// <summary>
	/// Offset from actor's position
	/// </summary>
	public Vector3 offset;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = actor.transform.position + offset;
	}
}

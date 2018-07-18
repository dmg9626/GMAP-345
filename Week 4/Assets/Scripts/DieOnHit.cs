using UnityEngine;
using System.Collections;

public class DieOnHit : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D c)
	{
		Destroy(gameObject);
	}
}

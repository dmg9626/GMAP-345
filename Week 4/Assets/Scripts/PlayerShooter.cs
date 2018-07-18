using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public float fireTime;
    public Animator animator;

    private Vector2 lastVelocity = Vector2.up;
    private float timeToFire;
    private Rigidbody2D rigidbody;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        timeToFire -= Time.deltaTime;

        if (rigidbody.velocity.magnitude > 0f)
        {
            lastVelocity = rigidbody.velocity.normalized;
        }

        if (Input.GetKey(KeyCode.Space) && timeToFire <= 0f)
        {
            animator.SetTrigger("Cast");
            timeToFire = fireTime;
        }
	}

    public void FireBullet()
    {
        GameObject b = Instantiate(bullet, transform.position, Quaternion.Euler(lastVelocity)) as GameObject;
        b.GetComponent<Rigidbody2D>().velocity = lastVelocity * bulletSpeed;
        b.transform.localEulerAngles = animator.transform.localEulerAngles;
    }
}

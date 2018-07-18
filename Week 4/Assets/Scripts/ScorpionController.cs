using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionController : MonoBehaviour
{
    private enum State { Wander, Chase, Attack, Die };

    public float wanderSpeed;
    public float chaseSpeed;
    public float minWanderTime;
    public float maxWanderTime;
    public Animator animator;
    public LayerMask willAttackLayer;
    public float sightDistance;
    public float strikeDistance;
    public float rotationSpeed;

    private State currentState;
    private float currentWanderTime;
    private Vector2 currentDirection;
    private GameObject currentTarget;
    private Rigidbody2D rigidbody;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        EnterStateWander();
	}
	

	void Update ()
    {
		switch(currentState)
        {
            case State.Wander:
                UpdateWander();
                break;
            case State.Chase:
                UpdateChase();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Die:
                UpdateDie();
                break;
        }
	}

    private void EnterStateWander()
    {
        currentState = State.Wander;
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        currentWanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    private void UpdateWander()
    {
        rigidbody.velocity = currentDirection * wanderSpeed;
        currentWanderTime -= Time.deltaTime;
        animator.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(currentDirection.y, currentDirection.x)) - 90f);

        if (currentWanderTime <= 0f)
        {
            EnterStateWander();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, sightDistance, willAttackLayer.value);
        if (hit.collider != null)
        {
            EnterStateChase(hit.collider.gameObject);
        }
    }

    private void EnterStateChase(GameObject target)
    {
        currentState = State.Chase;
        currentTarget = target;
        currentDirection = (target.transform.position - transform.position).normalized;
    }

    private void UpdateChase()
    {
        Vector2 targetDirection = (currentTarget.transform.position - transform.position).normalized;
        currentDirection = Vector3.RotateTowards(currentDirection.normalized, targetDirection, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
        rigidbody.velocity = currentDirection * chaseSpeed;
        animator.transform.localEulerAngles = Vector3.forward * Mathf.Atan2(currentDirection.y, currentDirection.x);
        animator.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(currentDirection.y, currentDirection.x)) - 90f);
        float targetDistance = Vector3.Distance(currentTarget.transform.position, transform.position);

        if (targetDistance <= strikeDistance)
        {
            EnterStateAttack();
        }
        else if (targetDistance > sightDistance || Vector2.Angle(currentDirection, targetDirection) > 30f)
        {
            currentState = State.Wander;
        }
    }

    private void EnterStateAttack()
    {
        currentState = State.Attack;
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("attack");
    }

    private void UpdateAttack()
    {
        // Nothing to do here.
    }

    public void DoAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, strikeDistance, willAttackLayer.value);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<PlayerHealth>() != null)
            {
                hit.collider.gameObject.GetComponent<PlayerHealth>().GetHit();
            }
        }
    }

    public void AttackOver()
    {
        EnterStateWander();
    }

    public void Die()
    {
        EnterStateDie();
    }

    private void EnterStateDie()
    {
        currentState = State.Die;
        animator.SetTrigger("Die");
        rigidbody.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    private void UpdateDie()
    {
        if (animator.GetComponent<SpriteRenderer>().sprite == null)
        {
            Destroy(gameObject);
        }
    }
}

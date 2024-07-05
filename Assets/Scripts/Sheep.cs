using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sheep_State
{
    IDLE, 
    FIGHTING,
    SPRINTING,
}

public class Sheep : MonoBehaviour
{
    public float moveSpeed = 1;
    public float sprintSpeed = 2;
    public Transform destination;
    public Animator animator;
    public Vector3 initialPosition;
    public Vector3 actualDestination;
    public int damage;
    public bool angered;
    public int life;
    public bool dead;

    private bool shouldMove = true;
    public Sheep_State my_State = Sheep_State.IDLE;

    private Vector3 previousPosition;
    private Coroutine sprintCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // destination = new Vector2(10, 0);
        actualDestination = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        animator = GetComponent<Animator>();
        dead = false;
        angered = true;
        life = 2;
        damage = 1;
        previousPosition = transform.position;

        if (angered)
        {
            sprintCoroutine = StartCoroutine(SprintRandomly());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;

        switch (my_State)
        {
            case Sheep_State.IDLE:
                if(shouldMove || !angered)
                {
                    MoveTowardsDestination();
                }
                break;

            case Sheep_State.FIGHTING:
                StartCoroutine(HandleFightingState());
                break;

            case Sheep_State.SPRINTING:
                MoveTowardsDestination(sprintSpeed);
                break;


        }

        if (life == 0)
        {
            dead = true;
            isDead();
        }

        FlipSpriteBasedOnMovement();
        previousPosition = transform.position;
    }

    void MoveTowardsDestination(float speed = -1)
    {
        if (speed < 0)
        {
            speed = moveSpeed;
        }

        transform.position = Vector2.MoveTowards(transform.position, actualDestination, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destination.position) < 0.1f)
        {
            shouldMove = false;
        }

        if (!angered && Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            isDead();
        }

    }

    void FlipSpriteBasedOnMovement()
    {
        Vector3 currentPosition = transform.position;
        float movementDirectionX = currentPosition.x - previousPosition.x;

        if (movementDirectionX < 0)
        {
            // Moving right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movementDirectionX > 0)
        {
            // Moving left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life <= 0)
        {
            isDead();
        }
    }

    void isDead()
    {
        Destroy(gameObject);
  
    }

    public void StopMovement()
    {
        shouldMove = false;
    }

    private IEnumerator HandleFightingState()
    {
        // Stop movement immendiately
        StopMovement();

        // Wait for 1 second
        yield return new WaitForSeconds(1);

        // Change state back to IDLE and resume movement
        my_State = Sheep_State.IDLE;
        shouldMove = true;
    }

    public IEnumerator DestroyCity()
    {
        Debug.Log("Attacking city");
        yield return new WaitForSeconds(10);

        if (dead) yield break;

        actualDestination = initialPosition;
        my_State = Sheep_State.IDLE;
        shouldMove = true;
        angered = false;
        if (animator != null) { animator.SetBool("Enraged", false); animator.SetBool("Sprinting", false); }


       
    }

    public IEnumerator SprintRandomly()
    {
       while (angered && !dead)
        {
            yield return new WaitForSeconds(Random.Range(2, 10));
            if (dead) yield break;

            my_State = Sheep_State.SPRINTING;
            if (animator != null) { animator.SetBool("Sprinting", true); }

            yield return new WaitForSeconds(1);
            if (dead) yield break;

            my_State = Sheep_State.IDLE;
            if (animator != null) { animator.SetBool("Sprinting", false); }
  
        }

    }
}

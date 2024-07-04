using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sheep_State
{
    IDLE, 
    FIGHTING,
}

public class Sheep : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform destination;
    public Vector3 initialPosition;
    public Vector3 actualDestination;
    public int damage;
    public bool angered;
    public int life;
    public bool dead;

    private bool shouldMove = true;
    public Sheep_State my_State = Sheep_State.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        // destination = new Vector2(10, 0);
        actualDestination = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        dead = false;
        angered = true;
        life = 2;
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {

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

        }

        if(life==0)
        {
            dead = true;
            isDead();
        }

    
    }

    void MoveTowardsDestination()
    {
        // Sheep moves to the designated destination
        transform.position = Vector2.MoveTowards(transform.position, actualDestination, moveSpeed * Time.deltaTime);

        // Checks if the sheep has arrived to the destination
        if (Vector2.Distance(transform.position, destination.position) < 0.1f)
        {
            //Debug.Log("Sheep has arrived the destination");
            shouldMove = false;
        
        }

        // Checks if the sheep have arrived to their initial position again
        if (!angered && Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            isDead();
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

        actualDestination = initialPosition;
        my_State = Sheep_State.IDLE;
        shouldMove = true;
        angered = false;
    }
}

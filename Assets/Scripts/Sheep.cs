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
        dead = false;
        angered = false;
        life = 2;
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {

        switch (my_State)
        {
            case Sheep_State.IDLE:
                if(shouldMove)
                {
                    MoveTowardsDestination();
                }
                break;

            case Sheep_State.FIGHTING:
                StartCoroutine(HandleFightingState());
                break;

        }
             
        /*
         * if (angered)
         * {
         * }
         */

        if(life==0)
        {
            dead = true;
            isDead();
        }
    }

    void MoveTowardsDestination()
    {
        // Sheep moves to the designated destination
        transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);

        // Checks if the sheep has arrived to the destination
        if (Vector2.Distance(transform.position, destination.position) < 0.1f)
        {
            //Debug.Log("Sheep has arrived the destination");
            shouldMove = false;
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
}

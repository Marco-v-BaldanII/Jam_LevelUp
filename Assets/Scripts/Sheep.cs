using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform destination;
    public int damage;
    public bool angered;
    public int life;
    public bool dead;

    private bool shouldMove = true;


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
        if (shouldMove)
        {
            MoveTowardsDestination();
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
        //transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);

        // Checks if the sheep has arrived to the destination
        if (Vector2.Distance(transform.position, destination.position) < 0.1f)
        {
            //Debug.Log("Sheep has arrived the destination");
            shouldMove = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf"))
        {
            Wolf_AI wolf = collision.gameObject.GetComponent<Wolf_AI>();
            if (wolf != null && wolf._isDragging == false)
            {
                TakeDamage(damage);
                wolf.TakeDamage(damage);
            }
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
}

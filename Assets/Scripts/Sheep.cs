using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float moveSpeed = 5;
    public Vector3 destination;
    public int damage;
    public bool angered;
    public int life;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector3(10, 0, 10);
        dead = false;
        angered = false;
        life = 2;
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsDestination();
        
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
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // Checks if the sheep has arrived to the destination
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            //Debug.Log("Sheep has arrived the destination");
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

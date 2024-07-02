using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float moveSpeed = 5;
    public Vector3 destination;
    public int damage;
    public bool angered = false;

    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector3(10, 0, 10);
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
    }

    void MoveTowardsDestination()
    {
        // Sheep moves to the designated destination
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // Checks if the sheep has arrived to the destination
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            Debug.Log("Sheep has arrived the destination");
        }
    }
}

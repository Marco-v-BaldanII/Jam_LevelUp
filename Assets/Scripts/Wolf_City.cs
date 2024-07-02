using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wolf_City : wolf_task
{
    private int num_cotton = 0;
    private TextMeshProUGUI cotton_counter;


    // Start is called before the first frame update
    void Start()
    {
        cotton_counter = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true )
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && wolf.has_cotton ) // Also check if wolf is not already in the list
            {
                num_cotton++;
                cotton_counter.text = num_cotton.ToString();

                wolf.has_cotton = false;
                wolf.moving_towards_task = true;
            }
        }
    }

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.gameObject.GetComponent<Wolf_AI>();
            if (wolf != null && wolf.has_cotton) // Also check if wolf is not already in the list
            {
                num_cotton++;
                cotton_counter.text = num_cotton.ToString();

                wolf.has_cotton = false;
                wolf.moving_towards_task = true;
            }
        }
    }

   
}

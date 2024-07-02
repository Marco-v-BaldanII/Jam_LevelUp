using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wolf_City : wolf_task
{
    private int num_cotton = 0;
    private int inteligence = 0;
    public TextMeshProUGUI cotton_counter;
    public TextMeshProUGUI intelligence_counter;
    public ProgressBar intelligence_bar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BuildCity");

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
            if (wolf != null && wolf.has_cotton ) // when the wolf is transporting cotton
            {
                num_cotton++;
                cotton_counter.text = num_cotton.ToString();
                current_wolf = wolf;
                wolf.has_cotton = false;
                wolf.moving_towards_task = true;

            }
            else if (wolf != null) // When the wolf gets assigned to build city
            {
                my_wolfs.Add(wolf);
                current_wolf = wolf;
                current_wolf.ChangeTask(this);

            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && my_wolfs.Contains(wolf) == true) 
            {
                my_wolfs.Remove(wolf);
            }
        }
    }



    private IEnumerator BuildCity()
    {
        yield return new WaitForSeconds(3);

        while (inteligence < 1001)
        {

            yield return new WaitForSecondsRealtime(1);
            if (num_wolves > 0)
            {
               
                intelligence_bar.Add(2 * num_wolves);
              
            }
            else
            {
              
                intelligence_bar.Add(-1);
               
            }

        }

    }
   
}

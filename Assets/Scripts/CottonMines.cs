using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CottonMines : wolf_task
{
    public float mining_time = 5.0f;
    private Wolf_AI mining_wolf;
    int num_cotton = 0;

    private bool mining = false;
    public float mine_time = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (my_wolfs.Count() <= 0) { mining = false; }

        if (mining == false && num_wolves > 0)
        {
            mining = true;
            StartCoroutine("Mine_Cotton");

        }

    }

    private IEnumerator Mine_Cotton()
    {
        yield return new WaitForSeconds(3);

        while (num_wolves > 0)
        {
            int w_index = 0;
            while (my_wolfs[w_index % num_wolves].has_cotton == true) { w_index++; yield return null; if (num_wolves <= 0) { break; } }
            if (num_wolves <= 0) { break; }
            my_wolfs[w_index % num_wolves].has_cotton = true; // Give Cotton to cottonless wolf
            my_wolfs[w_index % num_wolves].moving_towards_task = false;
            yield return new WaitForSecondsRealtime(mine_time / num_wolves);


        }


    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && wolf.has_cotton == true)
            {
                wolf.my_state = Wolf_State.MINING;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && wolf.has_cotton == false && wolf.my_mood == Wolf_Mood.NORMAL && wolf.my_state != Wolf_State.PLAYING && wolf.my_state != Wolf_State.WALKING_TO_NOTHING) // Also check if wolf is not already in the list
            {
                current_wolf = wolf;
                current_wolf.ChangeTask(this, Wolf_State.MINING);
                if (!my_wolfs.Contains(wolf) && !wolf._isDragging)
                {
                    my_wolfs.Add(current_wolf);
                }

            }
        }
    }

}

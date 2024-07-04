using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum INTELIGENCE_LEVEL
{
    LOW,
    MID,
    HIGH
}


public class Wolf_City : wolf_task
{
    private int num_cotton = 0;
    private int inteligence = 0;

    uint intelligence_state = 1;

    public TextMeshProUGUI cotton_counter;
    public TextMeshProUGUI intelligence_counter;
    public Wolf_Spawner wolf_spawner;
    public ProgressBar intelligence_bar;
    private Animator animator;
    public INTELIGENCE_LEVEL intelligence_level = INTELIGENCE_LEVEL.LOW;

    // Start is called before the first frame update
    void Awake()
    {
        if (intelligence_bar == null)
        {
            GameObject obj = GameObject.Find("Intelligence_Bar");
            intelligence_bar = obj.GetComponent<ProgressBar>();
        }
        animator = GetComponent<Animator>();
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
            if (wolf != null && wolf.my_state == Wolf_State.MINING ) // when the wolf is transporting cotton
            {
                num_cotton++;
                cotton_counter.text = "Available cotton : " + num_cotton.ToString();
                current_wolf = wolf;
                wolf.has_cotton = false;
                wolf.moving_towards_task = true;

            }
            else if (wolf != null && wolf.my_state != Wolf_State.PLAYING && wolf.my_state != Wolf_State.WALKING_TO_NOTHING) // When the wolf gets assigned to build city
            {
                my_wolfs.Add(wolf);
                current_wolf = wolf;
                current_wolf.ChangeTask(this);

            }
        }
        if (collision.gameObject.CompareTag("Sheep") == true)
        {
            Debug.Log("sdfekfsefjbñsoefbeoifheihf");
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

        while (intelligence_bar.Get() < 801)
        {
            HandleCity_Animaions();


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

    void HandleCity_Animaions()
    {
        if (intelligence_bar.Get() > 266 && intelligence_level == INTELIGENCE_LEVEL.LOW)
        {
            intelligence_level = INTELIGENCE_LEVEL.MID;
            animator.SetInteger("level", 2);
            wolf_spawner.Change_Wolf_Level(2);
        }
        if (intelligence_bar.Get() > 533 && intelligence_level == INTELIGENCE_LEVEL.MID)
        {
            intelligence_level = INTELIGENCE_LEVEL.HIGH;
            animator.SetInteger("level", 3);
            wolf_spawner.Change_Wolf_Level(3);
        }

        if (intelligence_bar.Get() < 266 && intelligence_level == INTELIGENCE_LEVEL.MID)
        {
            intelligence_level = INTELIGENCE_LEVEL.LOW;
            animator.SetInteger("level", 1);
            wolf_spawner.Change_Wolf_Level(1);
        }
        if (intelligence_bar.Get() < 533 && intelligence_level == INTELIGENCE_LEVEL.HIGH)
        {
            intelligence_level = INTELIGENCE_LEVEL.MID;
            animator.SetInteger("level", 2);
            wolf_spawner.Change_Wolf_Level(2);
        }

    }

    public int CheckCotton() { return num_cotton; }

    public void AddCotton(int added)
    {
        num_cotton += added;
    }

    public void AddIntelligence(int decrement)
    {
        intelligence_bar.Add(decrement);
    }

}

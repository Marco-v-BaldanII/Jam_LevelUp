using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class wolf_task : MonoBehaviour
{

    protected List<Wolf_AI> my_wolfs = new List<Wolf_AI>();
    protected Wolf_AI current_wolf;
    public Collider2D collider;

    protected int num_wolves = 0;

    protected List<Sheep> my_sheep = new List<Sheep>();
    protected Sheep current_sheep;

    protected int num_sheep = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        num_wolves = my_wolfs.Count();
        num_sheep = my_sheep.Count();
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if(wolf != null && wolf.has_cotton == false) // Also check if wolf is not already in the list
            {
                current_wolf = wolf;
                current_wolf.ChangeTask(this);
                if (! my_wolfs.Contains(wolf) && !wolf._isDragging)
                {
                    my_wolfs.Add(current_wolf);
                }
   
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && wolf.has_cotton == true) 
            {
               
                if (my_wolfs.Contains(wolf))
                {
                    my_wolfs.Remove(wolf);
                }
            }
        }
    }

}

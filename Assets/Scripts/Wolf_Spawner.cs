using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wolf_Spawner : MonoBehaviour
{

    public GameObject wolf_prefab;
    public Transform spawn_point;
    private List<Wolf_AI> active_wolfs = new List<Wolf_AI>();
    public Wolf_City city;
    public int spawn_rate = 800;
    private Animator animator;
    public Transform[] talk_positions;
    private int talk_wait_min = 5;
    private int talk_wait_max = 15;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SewWolf();
        StartCoroutine("Spawn_Wolfs");
        StartCoroutine("Get_Wolfs_Talking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SewWolf()
    {
        GameObject wolf = Instantiate(wolf_prefab, spawn_point.position, transform.rotation, transform);
        Wolf_AI w = wolf.GetComponent<Wolf_AI>();
        w.wolf_city = city.gameObject;

        // Set intial state to ENRAGED  with a certain probability
        if (Random.Range(0,100) < 20)
        {
            w.my_mood = Wolf_Mood.ENRAGED;
            Debug.Log("Generated enraged wolf");
        }
        active_wolfs.Add(w);
    }

    private IEnumerator Spawn_Wolfs()
    {
        while (city.intelligence_bar.Get() < 1001)
        {
            yield return new WaitForSecondsRealtime(spawn_rate);
            if (active_wolfs.Count() < 20)
            {
                SewWolf();
            }
        }
    }

    private IEnumerator Get_Wolfs_Talking()
    {
        while (city.intelligence_bar.Get() < 1001)
        {
            yield return new WaitForSecondsRealtime(Random.Range(talk_wait_min, talk_wait_max));
            Wolf_AI wolf1 = null; Wolf_AI wolf2 = null;

            for (int i = 0; i < active_wolfs.Count(); ++i)
            {
                if (active_wolfs[i].my_state != Wolf_State.FIGHTING)
                {
                    wolf1 = active_wolfs[i];
                }
            }
            for (int i = 0; i < active_wolfs.Count(); ++i)
            {
                if (active_wolfs[i].my_state != Wolf_State.FIGHTING && wolf1 != null && wolf1 != active_wolfs[i])
                {
                    wolf2 = active_wolfs[i];
                }
            }

            if (wolf1 != null && wolf2 != null)
            {
                Vector3 dest = talk_positions[Random.Range(0, talk_positions.Length)].position;
                wolf1.StartTalking(dest, true); wolf2.StartTalking(dest, false);



            }
        }
    }

}

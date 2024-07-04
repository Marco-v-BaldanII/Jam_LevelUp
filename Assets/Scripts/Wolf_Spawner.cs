using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wolf_Spawner : MonoBehaviour
{

    public GameObject wolf_prefab;
    public Transform spawn_point;
    private List<Wolf_AI> active_wolfs = new List<Wolf_AI>();
    private List<Wolf_AI> playing_wolfs = new List<Wolf_AI>();
    public Wolf_City city;
    public int spawn_rate = 800;
    private Animator animator;
    public Transform[] talk_positions;
    public int talk_wait_min = 12;
    public int talk_wait_max = 30;
    public int talk_probability = 30;
    public int enraged_probability = 5;

    // Start is called before the first frame update
    void Start()
    {
        if(city == null) city = GameManager.Instance.city;
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
        // Include probability of spawning enraged wolf
        GameObject wolf = Instantiate(wolf_prefab, spawn_point.position, transform.rotation, transform);
        Wolf_AI w = wolf.GetComponent<Wolf_AI>();
        w.wolf_city = city.gameObject;


        if (Random.Range(0,100)< enraged_probability)
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
                if (active_wolfs[i].my_state != Wolf_State.FIGHTING && active_wolfs[i].my_state != Wolf_State.PLAYING)
                {
                    wolf1 = active_wolfs[i];
                }
            }
            for (int i = 0; i < active_wolfs.Count(); ++i)
            {
                if (active_wolfs[i].my_state != Wolf_State.FIGHTING && active_wolfs[i].my_state != Wolf_State.PLAYING && wolf1 != null && wolf1 != active_wolfs[i])
                {
                    wolf2 = active_wolfs[i];
                }
            }
            int talk = Random.Range(0, 101);

            if (wolf1 != null && wolf2 != null && talk > talk_probability)
            {
                Vector3 dest = talk_positions[Random.Range(0, talk_positions.Length)].position;
                wolf1.StartTalking(dest, true); wolf2.StartTalking(dest, false);

                // Check if wolf1 is normal and wolf2 is enraged
                if(wolf1.my_mood == Wolf_Mood.NORMAL && wolf2.my_mood == Wolf_Mood.ENRAGED)
                {
                    yield return new WaitForSeconds(3);
                    wolf1.my_mood = Wolf_Mood.ENRAGED;
                    Debug.Log("Normal wolf became enraged!");
                }
                else if (wolf1.my_mood == Wolf_Mood.ENRAGED && wolf2.my_mood==Wolf_Mood.NORMAL)
                {
                    yield return new WaitForSeconds(3);
                    wolf2.my_mood = Wolf_Mood.ENRAGED;
                    Debug.Log("Normal wolf became enraged!");
                }

            }
        }
    }

    public void Wolf_PlayTime(Transform carpetPos)
    {
        float W =  (active_wolfs.Count() );
        int target_wolves =(int) W;

        for (int i = 0; i < target_wolves; ++i)
        {
            active_wolfs[i].StartPlayTime(carpetPos.transform);
            playing_wolfs.Add(active_wolfs[i]);

        }
        StartCoroutine("Stop_PlayTime");

    }

    public int GetNumWolves()
    {
        return active_wolfs.Count();
    }

    private IEnumerator Stop_PlayTime()
    {
        yield return new WaitForSecondsRealtime(10);
        foreach (Wolf_AI wolf in playing_wolfs) {
            wolf.Back_To_Idle();
            
        }
        playing_wolfs.Clear();
    }

}

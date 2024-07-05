using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MODE
{
    NORMAL,
    TUTORIAL
}


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
    public int talk_wait_min = 0;
    public int talk_wait_max = 1;
    public int talk_probability = 50;
    public int enraged_probability = 5;
    [SerializeField] private ProgressBar cotton_bar;
    public Transform[] spawn_positions;
    private bool[] occupied_buildings;
    private int spawned_wolves = 0;

    public Collider2D collider;

    public MODE my_mode = MODE.NORMAL;

    // Start is called before the first frame update
    void Start()
    {
        if(city == null) city = GameManager.Instance.city;
        animator = GetComponent<Animator>();
        SewWolf();
        if (my_mode == MODE.NORMAL)
        {
            StartCoroutine("Spawn_Wolfs");
            StartCoroutine("Get_Wolfs_Talking");
        }
        occupied_buildings = new bool[3];
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int altered_wolves = 0;

        foreach (Wolf_AI wolf in active_wolfs)
        {
            if(wolf.my_mood != Wolf_Mood.ENRAGED) {
                switch (city.intelligence_level)
                {
                    case INTELIGENCE_LEVEL.LOW:
                        Change_Wolf_Level(1);
                        break;
                    case INTELIGENCE_LEVEL.MID:
               
                        Change_Wolf_Level(2);
                        break;
                    case INTELIGENCE_LEVEL.HIGH:
                
                        Change_Wolf_Level(3);
                        break;
                }
            }
            else
            {
                switch (city.intelligence_level)
                {
                    case INTELIGENCE_LEVEL.LOW:
                        Change_Wolf_Level(1);
                        break;
                    case INTELIGENCE_LEVEL.MID:

                        Change_Wolf_Level(2);
                        break;
                    case INTELIGENCE_LEVEL.HIGH:

                        Change_Wolf_Level(3);
                        break;
                }
                altered_wolves++;
            }
        }

        if(spawned_wolves >= 1 && active_wolfs.Count() == 0 || active_wolfs.Count() == altered_wolves)
        {
            StopAllCoroutines();
            city.Game_Over();
        }

    }

    public void SewWolf()
    {
        // Include probability of spawning enraged wolf
        GameObject wolf = Instantiate(wolf_prefab, spawn_point.position, transform.rotation, transform);
        Wolf_AI w = wolf.GetComponent<Wolf_AI>();
        w.wolf_city = city.gameObject;
        spawned_wolves++;
        w.Init();
        switch (city.intelligence_level)
        {
            case INTELIGENCE_LEVEL.LOW:
                Change_Wolf_Level(1);
                break;
            case INTELIGENCE_LEVEL.MID:
                if(enraged_probability != 100) enraged_probability = 30;
                Change_Wolf_Level(2);
                break;
            case INTELIGENCE_LEVEL.HIGH:
                if (enraged_probability != 100) enraged_probability = 90;
                Change_Wolf_Level(3);
                break;
        }


        if (spawned_wolves > 4 &&  Random.Range(0,100)< enraged_probability)
        {
            w.my_mood = Wolf_Mood.ENRAGED;
            w.gameObject.layer = LayerMask.NameToLayer("EnragedWolf");
           
            Debug.Log("Generated enraged wolf");
        }
        active_wolfs.Add(w);
    }

    public void RemoveWolves(Wolf_AI wolf)
    {
        active_wolfs.Remove(wolf);
    }
       

    private IEnumerator Spawn_Wolfs()
    {
        while (city.intelligence_bar.Get() < 1001)
        {
            cotton_bar.Fill_With_Time(spawn_rate - 2);
            yield return new WaitForSecondsRealtime(spawn_rate-2);
            animator.SetTrigger("sew");
            yield return new WaitForSecondsRealtime(2);
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

            float progress = city.intelligence_bar.Get();
            int scaled_talk_probability = talk_probability + ((int)progress / 100) * 12;

            int talk = Random.Range(0, 101);

            if (wolf1 != null && wolf2 != null && talk < scaled_talk_probability)
            {
                Vector3 dest = talk_positions[Random.Range(0, talk_positions.Length)].position;
                wolf1.StartTalking(dest, true); wolf2.StartTalking(dest, false);

                // Check if wolf1 is normal and wolf2 is enraged
                if(wolf1.my_mood == Wolf_Mood.NORMAL && wolf2.my_mood == Wolf_Mood.ENRAGED)
                {
                    yield return new WaitForSeconds(3);
                    if (wolf1 != null && wolf2 != null)
                    {
                        wolf1.my_mood = Wolf_Mood.ENRAGED;
                        wolf1.gameObject.layer = LayerMask.NameToLayer("EnragedWolf");
                        wolf1.animator.SetBool("enraged", true);
                        Debug.Log("Normal wolf became enraged!");
                    }
                }
                else if (wolf1.my_mood == Wolf_Mood.ENRAGED && wolf2.my_mood==Wolf_Mood.NORMAL)
                {
                    yield return new WaitForSeconds(3);
                    if (wolf1 != null && wolf2 != null)
                    {
                        wolf2.my_mood = Wolf_Mood.ENRAGED;
                        wolf2.gameObject.layer = LayerMask.NameToLayer("EnragedWolf");
                        wolf2.animator.SetBool("enraged", true);
                        Debug.Log("Normal wolf became enraged!");
                    }
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

    public void Change_Wolf_Level(int level)
    {
        level = Mathf.Clamp(level, 1, 3);
        foreach (Wolf_AI wolf in active_wolfs)
        {
            if (wolf != null && wolf.animator != null && wolf.my_mood != Wolf_Mood.ENRAGED)
            {
                wolf.animator.SetInteger("level", level);
               
            }
            if (wolf.hurt == false) { wolf.life = level; }

            wolf.damage = level;

        }
    }

    public Vector3 Spawn_Buildings(float time_alive)
    {
        for(int i = 0; i < 3; ++i)
        {
            if(occupied_buildings[i] == false)
            {
                occupied_buildings[i] = true;
                StartCoroutine(Free_UP_Building_Space(time_alive, i));
                return spawn_positions[i].position;
            }
        }
        return Vector3.zero;
    }

    private IEnumerator Free_UP_Building_Space(float wait, int index_building)
    {
        yield return new WaitForSecondsRealtime(wait);
        occupied_buildings[index_building] = false;
    }

}

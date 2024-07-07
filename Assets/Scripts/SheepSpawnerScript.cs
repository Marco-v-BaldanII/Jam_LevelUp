using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DIFFICULTY
{
    EASY,
    MEDIUM,
    HARD,
    HERALD_OF_CHAOS,
    INSANITY

}

public class SheepSpawnerScript : MonoBehaviour
{
    public GameObject sheepPrefab;
    public Transform city_destination;
    public Wolf_City city;
    public TextMeshProUGUI sheep_warning;
    private float timer = 0f;
    private float global_timer = 0f;

    public MODE tutorial = MODE.NORMAL;
    public DIFFICULTY sheep_difficulty = DIFFICULTY.HERALD_OF_CHAOS;
    public Transform[] spawn_positions;

    public bool inminent_spawn = false;
    public bool spawned = false;
    public List<Sheep> active_sheep;

    public int sheeps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawning_Sheep_Horde");
        StartCoroutine("HandleHordeStregth");
      
        active_sheep =  new List<Sheep>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSheep()
    {
        Vector3 spawn = spawn_positions[Random.Range(0, 4)].position;

            GameObject newSheep = Instantiate(sheepPrefab, spawn, Quaternion.identity, transform);
            Sheep sheepScript = newSheep.GetComponent<Sheep>();
            sheepScript.destination = city_destination;
            sheepScript.initialPosition = spawn;
            if(tutorial == MODE.TUTORIAL) active_sheep.Add(sheepScript);
        spawned = true;
        sheeps++;
      
    }

    private IEnumerator Spawning_Sheep_Horde()
    {
        while(city.intelligence_bar.Get() > -1)
        {
           
            float wait_time = 0.0f;
            int spawn_amount = 1;
            if (!inminent_spawn)
            {

                switch (sheep_difficulty)
                {
                case DIFFICULTY.EASY:
                    wait_time = Random.Range(48.0f, 60.0f);
                    spawn_amount = Random.Range(1, 3);
                    break;
                case DIFFICULTY.MEDIUM:
                    wait_time = Random.Range(30.0f, 40.0f);
                    spawn_amount = Random.Range(3, 5);
                    break;
                case DIFFICULTY.HARD:
                    wait_time = Random.Range(23.0f, 33.0f);
                    spawn_amount = Random.Range(4, 7);
                    break;
                case DIFFICULTY.HERALD_OF_CHAOS:
                    wait_time = Random.Range(15.0f, 23.0f);
                    spawn_amount = Random.Range(7, 10);
                    break;
                

                }
            }
            else { spawn_amount = 1; wait_time = 4; SpawnSheep(); }


            yield return new WaitForSecondsRealtime(wait_time-3);

            sheep_warning.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(3);

            sheep_warning.gameObject.SetActive(false);
            inminent_spawn = false;
           
            for(int i = 0; i < spawn_amount; ++i)
            {
                SpawnSheep();
                yield return new WaitForSecondsRealtime(Random.Range(1.2f, 3.0f));


            }

        }


    }

    private IEnumerator HandleHordeStregth()
    {
        yield return new WaitForSecondsRealtime(50);
        sheep_difficulty = DIFFICULTY.MEDIUM;
        yield return new WaitForSecondsRealtime(80);
        sheep_difficulty = DIFFICULTY.HARD;
        yield return new WaitForSecondsRealtime(60);
        sheep_difficulty = DIFFICULTY.HERALD_OF_CHAOS;
        //yield return new WaitForSecondsRealtime(60);
        //sheep_difficulty = DIFFICULTY.INSANITY;


    }

}
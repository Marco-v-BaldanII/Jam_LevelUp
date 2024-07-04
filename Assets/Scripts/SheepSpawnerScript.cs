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
    HERALD_OF_CHAOS

}

public class SheepSpawnerScript : MonoBehaviour
{
    public GameObject sheepPrefab;
    public Transform city_destination;
    public Wolf_City city;
    public TextMeshProUGUI sheep_warning;
    private float timer = 0f;
    private float global_timer = 0f;

    public DIFFICULTY sheep_difficulty = DIFFICULTY.HERALD_OF_CHAOS;
    public Transform[] spawn_positions;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawning_Sheep_Horde");
        StartCoroutine("HandleHordeStregth");
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSheep()
    {
        Vector3 spawn = spawn_positions[Random.Range(0, 4)].position;

            GameObject newSheep = Instantiate(sheepPrefab, spawn, Quaternion.identity, transform);
            Sheep sheepScript = newSheep.GetComponent<Sheep>();
            sheepScript.destination = city_destination;
            sheepScript.initialPosition = spawn;
      
      
    }

    private IEnumerator Spawning_Sheep_Horde()
    {
        while(city.intelligence_bar.Get() > -1)
        {
            float wait_time = 0.0f;
            int spawn_amount = 1;
            switch (sheep_difficulty)
            {
                case DIFFICULTY.EASY:
                    wait_time = Random.Range(30.0f, 45.0f);
                    spawn_amount = Random.Range(1, 3);
                    break;
                case DIFFICULTY.MEDIUM:
                    wait_time = Random.Range(25.0f, 35.0f);
                    spawn_amount = Random.Range(1, 6);
                    break;
                case DIFFICULTY.HARD:
                    wait_time = Random.Range(18.0f, 28.0f);
                    spawn_amount = Random.Range(1, 8);
                    break;
                case DIFFICULTY.HERALD_OF_CHAOS:
                    wait_time = Random.Range(12.0f, 22.0f);
                    spawn_amount = Random.Range(1, 10);
                    break;


            }


            yield return new WaitForSecondsRealtime(wait_time-3);

            sheep_warning.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(3);

            sheep_warning.gameObject.SetActive(false);

           
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
        yield return new WaitForSecondsRealtime(70);
        sheep_difficulty = DIFFICULTY.HARD;
        yield return new WaitForSecondsRealtime(60);
        sheep_difficulty = DIFFICULTY.HERALD_OF_CHAOS;


    }

}
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
    public float spawnRate = 2f;
    public Transform city_destination;
    public Wolf_City city;
    public TextMeshProUGUI sheep_warning;
    private float timer = 0f;
    private float global_timer = 0f;

    private DIFFICULTY sheep_difficulty = DIFFICULTY.HERALD_OF_CHAOS;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawning_Sheep_Horde");
      
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
           // SpawnSheep();
            timer = 0f;
        }
    }

    void SpawnSheep()
    {
        GameObject newSheep = Instantiate(sheepPrefab, transform.position, transform.rotation);
        Sheep sheepScript = newSheep.GetComponent<Sheep>();
        sheepScript.destination = city_destination;
      
    }

    private IEnumerator Spawning_Sheep_Horde()
    {
        while(city.intelligence_bar.Get() > -1)
        {
            float wait_time = 0.0f;
            switch (sheep_difficulty)
            {
                case DIFFICULTY.EASY:
                    wait_time = Random.Range(30.0f, 45.0f);
                    break;
                case DIFFICULTY.MEDIUM:
                    wait_time = Random.Range(25.0f, 35.0f);
                    break;
                case DIFFICULTY.HARD:
                    wait_time = Random.Range(18.0f, 28.0f);
                    break;
                case DIFFICULTY.HERALD_OF_CHAOS:
                    wait_time = Random.Range(12.0f, 22.0f);
                    break;


            }


            yield return new WaitForSecondsRealtime(wait_time-3);

            sheep_warning.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(3);

            sheep_warning.gameObject.SetActive(false);

            int spawn_amount = Random.Range(3, 6);
            for(int i = 0; i < spawn_amount; ++i)
            {
                SpawnSheep();
                yield return new WaitForSecondsRealtime(Random.Range(0.3f, 1.2f));


            }

        }


    }

}
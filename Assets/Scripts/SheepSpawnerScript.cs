using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawnerScript : MonoBehaviour
{
    public GameObject sheepPrefab;
    public float spawnRate = 2f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnSheep();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnSheep();
            timer = 0f;
        }
    }

    void SpawnSheep()
    {
        GameObject newSheep = Instantiate(sheepPrefab, transform.position, transform.rotation);
        Sheep sheepScript = newSheep.GetComponent<Sheep>();
      
    }
}
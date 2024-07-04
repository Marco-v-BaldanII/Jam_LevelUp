using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float alive_time = 10.0f;

    public Wolf_City civilization;

    protected void Start()
    {
        GameObject obje = GameObject.Find("Civilization");
        civilization = obje.GetComponent<Wolf_City>();

    }
    // Update is called once per frame
    protected void Update()
    {
        alive_time -= Time.deltaTime;

        if(alive_time <= 0)
        {

            Destroy(this.gameObject);

        }



    }
}

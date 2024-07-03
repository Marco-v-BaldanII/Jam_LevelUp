using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float alive_time = 10.0f;




    // Start is called before the first frame update
    void Start()
    {
        
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

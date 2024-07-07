using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blibioteca : Building
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        StartCoroutine("StartReading");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }


    private IEnumerator StartReading()
    {
        while (alive_time > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            civilization.AddIntelligence(civilization.wolf_spawner.GetNumWolves() *2);
        }


    }

}

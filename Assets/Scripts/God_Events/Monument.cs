using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : Building
{
    // Start is called before the first frame update

    public Wolf_City civilization;

    void Start()
    {
        GameObject obje = GameObject.Find("Civilization");
        civilization = obje.GetComponent<Wolf_City>();
        StartCoroutine("Raise_Faith");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }


    private IEnumerator Raise_Faith()
    {
        while (alive_time > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            civilization.AddIntelligence(-civilization.wolf_spawner.GetNumWolves());
        }

    }

}

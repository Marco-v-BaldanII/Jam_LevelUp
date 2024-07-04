using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : Building
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        civilization.wolf_spawner.Wolf_PlayTime(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // tienes acceso a el wolf_spawner, llama para que convierta a algunos wolves a modo play y ps eso, luego debes guardarlos en algun sitio para revertirlos

    }
}

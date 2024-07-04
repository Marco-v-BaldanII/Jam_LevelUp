using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle_Parent : MonoBehaviour
{

    private Whistle whistle;
    Vector3 unitVector = new Vector3(1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        whistle = GetComponentInChildren<Whistle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if(whistle.gameObject.activeSelf == false)
            {
                whistle.gameObject.SetActive(true);
            }

        }
        else if(whistle.transform.localScale == unitVector)
        {
            if (whistle.gameObject.activeSelf == true)
            {
                whistle.gameObject.SetActive(false);
            }
        }
    }
}

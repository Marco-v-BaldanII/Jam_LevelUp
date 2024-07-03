using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle_Timer : MonoBehaviour
{

    public float wait_time = 15;
    private Image Fill;
    private float max_wait;
    private Card my_card;

    // Start is called before the first frame update
    void Start()
    {
        max_wait = wait_time;
        Fill = GetComponentInChildren<Image>();
        my_card = GetComponentInParent<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        wait_time -= Time.deltaTime;
        Fill.fillAmount = wait_time / max_wait;

        if (wait_time < 0) { wait_time = 0; }
    }



}

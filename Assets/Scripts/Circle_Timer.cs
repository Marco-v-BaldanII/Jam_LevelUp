using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle_Timer : MonoBehaviour
{

    public float wait_time = 15;
    public Image Fill;
    private Image my_image;
    private float max_wait;
    private Card my_card;

    public Color startColor;
    public Color endColor;

    public float duration;

    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        max_wait = wait_time;
        my_image = GetComponent<Image>();
        duration = wait_time;
        my_card = GetComponentInParent<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        wait_time -= Time.deltaTime;
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / duration;

        // Lerp between the start and end colors
        Color lerpedColor = Color.Lerp(startColor, endColor, t);
        my_image.color = lerpedColor;


        Fill.fillAmount = wait_time / max_wait;


        if (wait_time < 0) {
            wait_time = 0;

            my_card.EnableCard();

            this.gameObject.SetActive(false);


        }
    }

    public void ReStart()
    {
        wait_time = max_wait;
        elapsedTime = 0;
    }

}

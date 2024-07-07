using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle_Timer : MonoBehaviour
{

    public float wait_time = 15;
    public Image Fill;
    public Image color_changing_image;
    private float max_wait;
    private Card my_card;

    public Color startColor;
    public Color endColor;

    public float duration;

    private float elapsedTime = 0.0f;
    bool finished = false;

    public GameObject background_circle;
    // Start is called before the first frame update
    void Start()
    {
     
     
   
        my_card = GetComponentInParent<Card>();
      
    }

    public void Init(float time)
    {
        wait_time = time;
        max_wait = wait_time;
        duration = time;
    }

    // Update is called once per frame
    void Update()
    {
        wait_time -= Time.deltaTime;
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / duration;

        // Lerp between the start and end colors
        Color lerpedColor = Color.Lerp(startColor, endColor, t);
        color_changing_image.color = lerpedColor;


        Fill.fillAmount =  (max_wait - wait_time) / max_wait;
       // Debug.Log("Fillamount" + Fill.fillAmount.ToString());


        if (wait_time < 0 && !finished) {
            wait_time = 0;
            finished = true;
            my_card.EnableCard();

            this.gameObject.SetActive(false);
            color_changing_image.gameObject.SetActive(false);

            
        }
    }

    public void ReStart()
    {
        Fill.gameObject.SetActive(true);
        wait_time = max_wait;
        elapsedTime = 0;
        finished = false;
        background_circle.SetActive(true);
    }

    public int Get_Time()
    {
        return (int)wait_time;
    }

}

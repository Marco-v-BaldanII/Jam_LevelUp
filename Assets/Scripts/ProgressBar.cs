using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    private float targetProgress = 0;
    public float fillSpeed = 0.5f;

    public float fillDuration = 6.0f;
    bool is_full = false;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(1);
    }

    // Update is called once per frame
    void Update()
    {
        //if (slider.value < targetProgress)
        //{
        //    slider.value += fillSpeed * Time.deltaTime;
        //}

        if(is_full)
        {
            slider.value -= 0.005f;
            if(slider.value <= 0.03f)
            {
                slider.value = 0;
                is_full = false;
            }
        }

    }

    IEnumerator FillSlider()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / fillDuration);
            yield return null; // Wait for the next frame
        }
        // Ensure the slider is completely filled at the end
        slider.value = 1;
        is_full = true;
    }

    public void Fill_With_Time(float seconds)
    {
        fillDuration = seconds;
        StartCoroutine(FillSlider());
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    public void Add(int value)
    {
        slider.value += value;
    }

    public float Get() { return slider.value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Loading_Text : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(text != null) { StartCoroutine("Loading"); }
        else { SceneManager.LoadScene("SampleScene"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Loading()
    {
        while (true)
        {
            text.text = "Loading";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading.";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading..";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading...";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading.";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading..";
            yield return new WaitForSeconds(0.2f);
            text.text = "Loading...";
            yield return new WaitForSeconds(0.2f);

            SceneManager.LoadScene("SampleScene");
        }
    }
}

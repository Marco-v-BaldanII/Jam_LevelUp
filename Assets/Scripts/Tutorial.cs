using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    //public TextMeshProUGUI textComponent;
    //public Button myButton;
    public string[] lines;
    public float textSpeed = 0.1f;

    private int index = 0;

    void Awake()
    {
        //if (textComponent == null)
        //{
        //    Debug.LogError("Text Component is not assigned.");
        //}

        //if (myButton == null)
        //{
        //    Debug.LogError("Button is not assigned.");
        //}
        //else
        //{
        //    myButton.onClick.AddListener(AdvanceText);
        //}

        //if (lines == null || lines.Length == 0)
        //{
        //    Debug.LogError("Lines array is empty or not assigned.");
        //}
    }

    void Start()
    {
        //if (lines.Length > 0)
        //{
        //    StartCoroutine(TypeLine());
        //}
    }

    void AdvanceText()
    {
        //if (textComponent.text == lines[index])
        //{
        //    NextLine(); // if current line has finished write the next
        //}
        //else
        //{
        //    StopAllCoroutines();
        //    textComponent.text = lines[index]; // Directly fills out the line
        //}
    }

    //private IEnumerator TypeLine()
    //{
    //    foreach (char c in lines[index].ToCharArray())
    //    {
    //        textComponent.text += c;
    //        yield return new WaitForSecondsRealtime(textSpeed);
    //    }
    //}

    //void NextLine()
    //{
    //    if (index < lines.Length - 1)
    //    {
    //        index++;
    //        textComponent.text = string.Empty;
    //        StartCoroutine(TypeLine());
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
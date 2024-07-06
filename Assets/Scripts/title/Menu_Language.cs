using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Language
{
    ENGLISH,
    CATALAN
}

public class Menu_Language : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Text Play;
    [SerializeField] private Text Quit;
    [SerializeField] private Text Tutorial;
    [SerializeField] private Text Language;
    Language language;
    void Start()
    {
        language = global::Language.CATALAN;
    }

    // Update is called once per frame
    void Update()
    {
        

        

    }

    private void English_Names()
    {
        Play.text = "Play";
        Quit.text = "Quit";
        Tutorial.text = "How to play";
        Language.text = "Language";
        language = global::Language.ENGLISH;
    }

    private void Catalan_Names()
    {
        Play.text = "Jugar";
        Quit.text = "Surt";
        Tutorial.text = "Com jugar";
        Language.text = "Llenguatge";
        language = global::Language.CATALAN;
    }

    public void Change_Language()
    {
        if(language == global::Language.ENGLISH) { language = global::Language.CATALAN; Catalan_Names(); }
        else { language = global::Language.ENGLISH; English_Names(); }
    }
    

}

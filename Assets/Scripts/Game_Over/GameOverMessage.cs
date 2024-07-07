using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMessage : MonoBehaviour
{
    public GameObject[] intelligence_quotes;
    public GameObject[] ignorance_quotes;
    public GameObject[] cat_intelligence_quotes;
    public GameObject[] cat_ignorance_quotes;

    private TextMeshProUGUI selected_quote;

    bool intelligence = false;

    bool first = true;


    // Start is called before the first frame update
    void Start()
    {
        if (first)
        {
            first = false;

            int id = PlayerPrefs.GetInt("death");
            if (id == 1)
            {
                intelligence = true;
            }
            int index = Random.Range(0, 4);

            if (intelligence)
            {
                if (GameManager.Instance.game_language == Language.ENGLISH)
                {
                    intelligence_quotes[index].gameObject.SetActive(true);
                }
                else
                {
                    cat_intelligence_quotes[index].gameObject.SetActive(true);
                }
            }
            else
            {
                if (GameManager.Instance.game_language == Language.ENGLISH)
                {
                    ignorance_quotes[index].gameObject.SetActive(true);
                }
                else
                {
                    cat_ignorance_quotes[index].gameObject.SetActive(true);
                }
            }

        }

    }

    public void Back_To_Title()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Start_Game()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

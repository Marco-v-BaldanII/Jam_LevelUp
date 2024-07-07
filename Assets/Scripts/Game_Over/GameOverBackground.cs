using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverBackground : MonoBehaviour
{
    Texture2D background;
    SpriteRenderer renderer;

    [SerializeField] TextMeshProUGUI menu;
    [SerializeField] TextMeshProUGUI jugar;
    [SerializeField] TextMeshProUGUI puntuacio;
    [SerializeField] TextMeshProUGUI condicio_derrota;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI high_score;

    [SerializeField] TextMeshProUGUI new_high_score_text;

    public GameObject new_highscore;
    void Start()
    {
        background = GameManager.Instance.screenshot;
        renderer = GetComponent<SpriteRenderer>();

        Rect rect = new Rect(0, 0, background.width, background.height);
        Sprite screenshotSprite = Sprite.Create(background, rect, new Vector2(0.5f, 0.5f), 100.0f);
        renderer.sprite = screenshotSprite;

        int s = PlayerPrefs.GetInt("current_time");
        int max = PlayerPrefs.GetInt("high_score");

        if(s > max)/*New highscore*/
        {
            new_highscore.SetActive(true);
            PlayerPrefs.SetInt("high_score", s);
            max = s;
        }

        score.text = s.ToString();
        high_score.text = max.ToString();

        if (GameManager.Instance.game_language == Language.ENGLISH)
        {
            menu.text = "Menu";
            jugar.text = "Play";
            puntuacio.text = "Score";
            if (PlayerPrefs.GetInt("death") == 1)
            {

                condicio_derrota.text = "Demise of Intelligence";
            }
            else
            {
                condicio_derrota.text = "Demise of Ignorance";
            }

        }
        else
        {
            new_high_score_text.text = "Rècord";

            if (PlayerPrefs.GetInt("death") == 1)
            {

                condicio_derrota.text = "Perdició d'intel·ligènica";
            }
            else
            {
                condicio_derrota.text = "Perdició d'ignorància";
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

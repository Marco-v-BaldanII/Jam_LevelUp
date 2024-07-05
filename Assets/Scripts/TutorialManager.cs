using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TutorialManager : MonoBehaviour
{

    public int e = 5;

    public TextMeshProUGUI textComponent;
    public Button myButton;

    public string[] lines;
    public float textSpeed = 0.1f;

    private int index = 0;

    public Wolf_AI tutorial_wolf;
    public Wolf_City city;
    public Wolf_Spawner wolf_spawner;
    public CottonMines cotton_mines;
    public SheepSpawnerScript sheep_spawner;
    public Card library_card;

    public ProgressBar intelligence_bar;

    bool tutorial_wolf_instanciated = false;

    bool tutorial_wolf_moved = false;

    bool inactive_city = true;

    bool second_wolves_spawned = false;

    void Awake()
    {
        if (textComponent == null)
        {
            Debug.LogError("Text Component is not assigned.");
        }

        if (myButton == null)
        {
            Debug.LogError("Button is not assigned.");
        }
        else
        {
            myButton.onClick.AddListener(AdvanceText);
        }

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("Lines array is empty or not assigned.");
        }
    }

    void Start()
    {
        if (lines.Length > 0)
        {
            StartCoroutine(TypeLine());
        }
    }

    void AdvanceText()
    {
        if (index != 4 && index != 9 && index != 13 && index != 14 && index != 21)
        {
            if (textComponent.text == lines[index])
            {
                NextLine(); // if current line has finished write the next
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; // Directly fills out the line
            }
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        // At dialogue 3 spawn tutorial wolf
        if (index == 3 && tutorial_wolf_instanciated == false)
        {
            tutorial_wolf.gameObject.SetActive(true);
            tutorial_wolf_instanciated = true;
        }

        UnlockThings();


        // When the player has moved the wolf advance the dialogue
        if(index == 4 && tutorial_wolf != null && tutorial_wolf_moved)
        {
            NextLine(); // if current line has finished write the next
        }

        // When the player has sent the first wolf to the city
        if(index == 9 && city.num_wolves2 > 0)
        {
            NextLine(); // if current line has finished write the next
        }

        if(index == 13 && city.Get_Cotton() >= 5)
        {
            NextLine();
        }
        if(index == 14 && library_card.played_once == true)
        {
            NextLine();
        }
        if(index == 21 && sheep_spawner.active_sheep.Count() == 0 && sheep_spawner.spawned == true && sheep_spawner.sheeps == 0)
        {
            NextLine();
        }
        if(index == 23 && second_wolves_spawned == false)
        {
            second_wolves_spawned = true;
            wolf_spawner.SewWolf();
            wolf_spawner.SewWolf();
            wolf_spawner.enraged_probability = 100;
            wolf_spawner.SewWolf();
            sheep_spawner.gameObject.SetActive(false);
        }

    }



    private void UnlockThings()
    {
        if (tutorial_wolf != null && tutorial_wolf.my_state == Wolf_State.WALKING_TO_NOTHING)
        {
            tutorial_wolf_moved = true;
        }
        if (index == 9 && inactive_city)
        {
            city.collider.enabled = true;
            inactive_city = false;
        }
        if (index == 11 && wolf_spawner != null && wolf_spawner.gameObject.activeSelf == false)
        {
            wolf_spawner.gameObject.SetActive(true);
            wolf_spawner.enraged_probability = 0;
            wolf_spawner.SewWolf();
        }
        if(index == 12 && cotton_mines.collider.enabled == false)
        {
            cotton_mines.collider.enabled = true;
        }
        if (index == 14 && library_card.gameObject.activeSelf == false)
        {
            library_card.gameObject.SetActive(true);
            intelligence_bar.my_mode = MODE.NORMAL;
            intelligence_bar.Add(75);
        }
        if (index == 21 && sheep_spawner.gameObject.activeSelf == false)
        {
            sheep_spawner.gameObject.SetActive(true);
           
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class TutorialManager : MonoBehaviour
{

    public int e = 5;

    public TextMeshProUGUI textComponent;

    private string[] current_lines;

    public string[] lines;

    public string[] lines_catalan;

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

    bool conversation = false;

    bool third_wolves_spawned = false;

    public TextMeshProUGUI spacebar_message;
    

    void Awake()
    {

        string idoma = PlayerPrefs.GetString("language");

        if (idoma == "english")
        {
            current_lines = lines;
        }
        else
        {
            spacebar_message.text = "Pressiona espai per continuar";
            current_lines = lines_catalan;
        }

        if (textComponent == null)
        {
            Debug.LogError("Text Component is not assigned.");
        }


        if (current_lines == null || current_lines.Length == 0)
        {
            Debug.LogError("Lines array is empty or not assigned.");
        }
    }

    void Start()
    {
        if (current_lines.Length > 0)
        {
            StartCoroutine(TypeLine());
        }
    }

    void AdvanceText()
    {
        if (index != 4 && index != 9 && index != 13 && index != 14 && index != 22 && index != 21)
        {
            if (textComponent.text == current_lines[index])
            {
                NextLine(); // if current line has finished write the next
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = current_lines[index]; // Directly fills out the line
            }
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in current_lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < current_lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Back_To_Menu();
            gameObject.SetActive(false);
        }
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Special_Advance_Line()
    {
        if (textComponent.text == current_lines[index])
        {
            NextLine(); // if current line has finished write the next
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = current_lines[index]; // Directly fills out the line
            NextLine();
        }
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space)) { AdvanceText(); }
        if (index == 4 || index == 9 || index == 13 || index == 14 || index == 22 || index == 21)
        {
            //if(spacebar_message.gameObject.activeSelf == true) { spacebar_message.gameObject.SetActive(false); }
        }
        else
        {
            //if (spacebar_message.gameObject.activeSelf == false) { spacebar_message.gameObject.SetActive(true); }
        }


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
           
            textComponent.text = string.Empty;
            Special_Advance_Line();
            // if current line has finished write the next
        }

        // When the player has sent the first wolf to the city
        if(index == 9 && city.num_wolves2 > 0)
        {
            StopCoroutine("TypeLine");
            textComponent.text = string.Empty;
            Special_Advance_Line();
        }

        if(index == 13 && city.Get_Cotton() >= 5)
        {
            StopCoroutine("TypeLine");
            textComponent.text = string.Empty;
            Special_Advance_Line();
        }
        if(index == 14 && library_card.played_once == true)
        {
            StopCoroutine("TypeLine");
            textComponent.text = string.Empty;
            Special_Advance_Line();
        }
        if(index == 21 && Input.GetMouseButtonUp(1))
        {
            StopCoroutine("TypeLine");
            textComponent.text = string.Empty;
            Special_Advance_Line();
        }
        if(index == 22 && sheep_spawner.active_sheep.Count() == 0 && sheep_spawner.spawned == true && sheep_spawner.sheeps == 0)
        {
            StopCoroutine("TypeLine");
            textComponent.text = string.Empty;
            Special_Advance_Line();
        }
        if(index == 24 && second_wolves_spawned == false)
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
            wolf_spawner.SewWolf();
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
        if (index == 22 && sheep_spawner.gameObject.activeSelf == false)
        {
            sheep_spawner.gameObject.SetActive(true);
           
        }
        if(index == 28 && !conversation)
        {
            wolf_spawner.talk_wait_min = 0; wolf_spawner.talk_wait_max = 1;
            wolf_spawner.talk_probability = 10000;
            wolf_spawner.Specific_Conversation(Wolf_Mood.ENRAGED);
            conversation = true;
        }
        if(index == 30 && !third_wolves_spawned)
        {
            if(sheep_spawner.gameObject.activeSelf == false){ sheep_spawner.gameObject.SetActive(true); }
            third_wolves_spawned = true;
            wolf_spawner.enraged_probability = 100;
            wolf_spawner.SewWolf();
            wolf_spawner.SewWolf();
            wolf_spawner.enraged_probability = 0;
            wolf_spawner.SewWolf();
            sheep_spawner.SpawnSheep();
            wolf_spawner.SewWolf();

            wolf_spawner.SewWolf();
            sheep_spawner.SpawnSheep();
            wolf_spawner.SewWolf();
            sheep_spawner.SpawnSheep();
            wolf_spawner.SewWolf();

        }
    }


}

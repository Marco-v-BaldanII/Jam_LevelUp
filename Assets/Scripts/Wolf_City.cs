using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum INTELIGENCE_LEVEL
{
    LOW,
    MID,
    HIGH
}


public class Wolf_City : wolf_task
{
    [SerializeField] int num_cotton = 0;
    private int inteligence = 0;

    uint intelligence_state = 1;

    public TextMeshProUGUI cotton_counter;
    public TextMeshProUGUI intelligence_counter;
    [SerializeField] TextMeshProUGUI time_counter;
    public Wolf_Spawner wolf_spawner;
    public ProgressBar intelligence_bar;
    public INTELIGENCE_LEVEL intelligence_level = INTELIGENCE_LEVEL.LOW;

    [SerializeField] TextMeshProUGUI current_message;
    [SerializeField] TextMeshProUGUI bronze_ages;
    [SerializeField] TextMeshProUGUI silver_ages;
    [SerializeField] TextMeshProUGUI gold_ages;

    public int num_wolves2;

    float timer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (intelligence_bar == null)
        {
            GameObject obj = GameObject.Find("Intelligence_Bar");
            intelligence_bar = obj.GetComponent<ProgressBar>();
        }
        StartCoroutine("BuildCity");

        current_message = bronze_ages;
        current_message.gameObject.SetActive(true);

        if(GameManager.Instance.game_language == Language.CATALAN){
            bronze_ages.text = "Edat de Bronze";
            silver_ages.text = "Edat de Plata";
            gold_ages.text = "Edat d'Or";
        }

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        num_wolves2 = num_wolves;

        timer += Time.deltaTime;

    
        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;

        time_counter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        cotton_counter.text = num_cotton.ToString();
    }

    public int Get_Cotton()
    {
        return num_cotton;
    }

    public override  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true )
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && wolf.my_state == Wolf_State.MINING ) // when the wolf is transporting cotton
            {
                num_cotton++;
                
                current_wolf = wolf;
                wolf.has_cotton = false;
                wolf.moving_towards_task = true;

            }
            else if (wolf != null && wolf.my_state != Wolf_State.PLAYING && wolf.my_state != Wolf_State.WALKING_TO_NOTHING) // When the wolf gets assigned to build city
            {
                my_wolfs.Add(wolf);
                current_wolf = wolf;
                current_wolf.ChangeTask(this);

            }
        }
        if (collision.gameObject.CompareTag("Sheep") == true)
        {
            Sheep sheep = collision.gameObject.GetComponent<Sheep>();
            if (sheep != null)
            {
                StartCoroutine(sheep.DestroyCity());
                current_sheep = sheep;
                my_sheep.Add(current_sheep);
       
            }

        }

    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.GetComponent<Wolf_AI>();
            if (wolf != null && my_wolfs.Contains(wolf) == true) 
            {
                my_wolfs.Remove(wolf);
            }
        }
        if (collision.gameObject.CompareTag("Sheep") == true)
        {
            Sheep sheep = collision.GetComponent<Sheep>();
            if (sheep != null && my_sheep.Contains(sheep) == true)
            {
                my_sheep.Remove(sheep);
            }
        }
    }

    private IEnumerator BuildCity()
    {
        yield return new WaitForSeconds(3);

        while (intelligence_bar.Get() < 801)
        {
            HandleCity_Animaions();


            yield return new WaitForSecondsRealtime(1);
            if (num_wolves > 0)
            {
               
                intelligence_bar.Add(2 * num_wolves);
              
            }
            if (num_sheep > 0)
            {

                intelligence_bar.Add(-6 * num_sheep);

            }
            else
            {
              
                intelligence_bar.Add(-1);
               
            }
            


        }

    }

    void HandleCity_Animaions()
    {
        if (intelligence_bar.Get() > 266 && intelligence_level == INTELIGENCE_LEVEL.LOW)
        {
            intelligence_level = INTELIGENCE_LEVEL.MID;
            wolf_spawner.Change_Wolf_Level(2);
        }
        if (intelligence_bar.Get() > 533 && intelligence_level == INTELIGENCE_LEVEL.MID)
        {
            intelligence_level = INTELIGENCE_LEVEL.HIGH;
            wolf_spawner.Change_Wolf_Level(3);
        }

        if (intelligence_bar.Get() < 266 && intelligence_level == INTELIGENCE_LEVEL.MID)
        {
            intelligence_level = INTELIGENCE_LEVEL.LOW;
            wolf_spawner.Change_Wolf_Level(1);
        }
        if (intelligence_bar.Get() < 533 && intelligence_level == INTELIGENCE_LEVEL.HIGH)
        {
            intelligence_level = INTELIGENCE_LEVEL.MID;
            wolf_spawner.Change_Wolf_Level(2);
        }
        Change_Age_Message(intelligence_level);
    }

    public int CheckCotton() { return num_cotton; }

    public void AddCotton(int added)
    {
        num_cotton += added;
    }

    public void AddIntelligence(int decrement)
    {
        intelligence_bar.Add(decrement);
    }

    void Change_Age_Message(INTELIGENCE_LEVEL level)
    {
        switch (level)
        {
            case INTELIGENCE_LEVEL.LOW:
                current_message.gameObject.SetActive(false);
                current_message = bronze_ages;
                
                break;
            case INTELIGENCE_LEVEL.MID:
                current_message.gameObject.SetActive(false);
                current_message = silver_ages;
                
                break;
            case INTELIGENCE_LEVEL.HIGH:
                current_message.gameObject.SetActive(false);
                current_message = gold_ages;
                
                break;
        }
        current_message.gameObject.SetActive(true);
    }

    public void Game_Over()
    {
        if(intelligence_bar.Get() > 400)
        {
            // Intelligence defeat
            PlayerPrefs.SetInt("death", 1);
        }
        else
        {
            // Ignorance defeat
            PlayerPrefs.SetInt("death", 0);
        }
        GameManager.Instance.ScreenShot();
        PlayerPrefs.SetInt("current_time", (int) timer);

        StartCoroutine("Game_Over2");
    }

    private IEnumerator Game_Over2()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Game_Over");
    }

}

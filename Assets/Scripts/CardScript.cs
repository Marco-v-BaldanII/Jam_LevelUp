using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public int cardID;
    public Image cardImage;
    public Circle_Timer timer;
    private Button myButton;
    public int cost = 5;
    public float wait_time = 15.0f;
    public TextMeshProUGUI second_counter;

    public Wolf_City city;
    public Building god_event;

    public bool played_once = false;

    public Card() { }
    ~Card() { }

    // Start is called before the first frame update
    void Start()
    {

        cardImage = GetComponent<Image>();
    
        myButton = GetComponent<Button>();
        
        InitialiseCard();
        timer.Init(wait_time);
        DisableCard();
     

    }

    // Update is called once per frame
    void Update()
    {
        second_counter.text = timer.Get_Time().ToString();
    }

    // Set the values of the desired card
    public virtual void InitialiseCard()
    {
       // DisableCard();
    }

    // Set the effect of the card
    public virtual void PlayCard()
    {
        Vector3 spawn = city.wolf_spawner.Spawn_Buildings(god_event.alive_time);
        played_once = true;
        if (city.CheckCotton() >= cost)
        {
            city.AddCotton(-cost);

            Building building = Instantiate(god_event, spawn, Quaternion.identity, city.wolf_spawner.transform);
            Debug.Log("Spawning Building");
            timer.gameObject.SetActive(true);
            timer.ReStart();
            DisableCard();
        }
        else
        {
            Debug.Log("not enough cotton");
        }
    }

    public void OnDisable()
    {
        
    }

    public void DisableCard()
    {
        myButton.interactable = false;
        second_counter.gameObject.SetActive(true);
        cardImage.color = new Color(100f/255.0f, 103f/255.0f, 132f/255.0f);
        
    }

    public void EnableCard()
    {
        myButton.interactable = true;
        second_counter.gameObject.SetActive(false);
        cardImage.color = new Color(255/255.0f,255 / 255.0f, 255 / 255.0f);
    }

}

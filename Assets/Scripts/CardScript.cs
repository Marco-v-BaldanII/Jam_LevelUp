using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public Image cardImage;
    private Circle_Timer timer;
    private Button myButton;
    public int cost = 5;
    public Wolf_City city;
    public Building god_event;

    public Card() { }
    ~Card() { }

    // Start is called before the first frame update
    void Start()
    {
        if(city == null) city = GameManager.Instance.city;
        cardImage = GetComponent<Image>();
    
        myButton = GetComponent<Button>();
        timer = GetComponentInChildren<Circle_Timer>();
        InitialiseCard();

     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set the values of the desired card
    public virtual void InitialiseCard()
    {
       // DisableCard();
    }

    // Set the effect of the card
    public virtual void PlayCard()
    {
        if (city.CheckCotton() > -1)
        {
            city.AddCotton(-cost);

            Building building = Instantiate(god_event, city.wolf_spawner.transform);

            timer.gameObject.SetActive(true);
            timer.ReStart();
            DisableCard();
        }
    }

    public void OnDisable()
    {
        
    }

    public void DisableCard()
    {
        myButton.interactable = false;
        cardImage.color = new Color(71.0f/255.0f, 74.0f/255.0f, 97.0f/255.0f);
        
    }

    public void EnableCard()
    {
        myButton.interactable = true;
        cardImage.color = new Color(255/255.0f,255 / 255.0f, 255 / 255.0f);
    }

}

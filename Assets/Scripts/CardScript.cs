using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public Sprite cardImage;
    private Circle_Timer timer;
    private Button myButton;

    public Card() { }
    ~Card() { }

    // Start is called before the first frame update
    void Start()
    {
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

    }

    // Set the effect of the card
    public virtual void PlayCard()
    {

    }

    public void OnDisable()
    {
        
    }

    public void Disable()
    {
        myButton.enabled = false;
    }

}

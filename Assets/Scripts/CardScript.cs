using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardID;
    public Sprite cardImage;

    public Card() { }
    ~Card() { }

    // Start is called before the first frame update
    void Start()
    {
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
}

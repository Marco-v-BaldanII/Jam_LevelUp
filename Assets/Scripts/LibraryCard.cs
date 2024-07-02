using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryCard : Card
{
    public GameObject building;

    public override void InitialiseCard()
    {
        cardID = 1;
    }

    public override void PlayCard()
    {
        // Instantiate the spawn of a library
    }

}

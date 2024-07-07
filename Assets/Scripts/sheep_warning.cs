using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sheep_warning : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI warning;

    // Start is called before the first frame update
    void Start()
    {
        if(warning == null) { warning = GetComponent<TextMeshProUGUI>(); }

        if(GameManager.Instance.game_language == Language.ENGLISH)
        {
            warning.text = "Warning ! A horde of sheep is approaching !";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

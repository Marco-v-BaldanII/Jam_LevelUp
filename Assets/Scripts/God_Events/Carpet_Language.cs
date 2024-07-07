using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Carpet_Language : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI full_description;

    public string en_name;
    public string en_description;
    public string en_full_des;


    public string cat_name;
    public string cat_description;
    public string cat_full_des;

    // Start is called before the first frame update
    void Start()
    {
        

        if(GameManager.Instance.game_language == Language.ENGLISH)
        {
            name.text = en_name;
            description.text = en_description;
            full_description.text = en_full_des;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);

        
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        return cam.ScreenToWorldPoint(mousePoint);
    }

    
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("whistle");
            transform.localScale = new Vector2(transform.localScale.x + 0.4f, transform.localScale.y + 0.4f);

        }
        transform.localScale = new Vector2(transform.localScale.x - 0.2f, transform.localScale.y - 0.2f);

        transform.localScale = new Vector2(Mathf.Clamp(transform.localScale.x, 0.2f, 1f), Mathf.Clamp(transform.localScale.x, 0.2f, 1f));
    }
}

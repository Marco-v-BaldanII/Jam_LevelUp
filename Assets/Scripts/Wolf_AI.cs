using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_AI : MonoBehaviour
{
    public int movement_speed = 3;

    private bool _isDragging;

    public wolf_task my_task;
    public GameObject wolf_city;
    public GameObject marshmallow;
    private Vector3 buffer_pos;
    private Rigidbody2D rigid;
    private Collider2D collider;

    public bool moving_towards_task = false;
    public bool moving_towards_base = false;

    public bool has_cotton = false;

    public int life;
    public int damage;

    private Sheep targetSheep;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        life = 1;
        damage = 1;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (moving_towards_task && my_task != null)
        {
            direction =  my_task.transform.position - transform.position;


        }
        else if (has_cotton && my_task != null)
        {
            direction = wolf_city.transform.position - transform.position;
        }
        else if (targetSheep != null)
        {
            direction = targetSheep.transform.position - transform.position;
        }

        rigid.velocity = direction.normalized * movement_speed;

        Debug.Log(has_cotton != marshmallow.activeSelf);
        if (has_cotton != marshmallow.activeSelf)
        {
            marshmallow.SetActive(has_cotton);
        }

    }

    public void OnMouseDrag()
    {
        _isDragging = true;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    
    }

    private void OnMouseUp()
    {
        transform.position = buffer_pos;
        _isDragging = false;
    }

    public void OnMouseEnter()
    {
        // Check that they are idle
        buffer_pos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Task") == true && _isDragging == false )
        {
            moving_towards_task = false;
        }
        if (collision.gameObject.CompareTag("Sheep") == true && _isDragging == false )
        {
            Sheep sheep = collision.GetComponent<Sheep>();
            if (sheep != null)
                targetSheep = sheep;
        }
    }

    public void ChangeTask(wolf_task task)
    {
        my_task = task;
        moving_towards_task = true;
      

    }

}
 
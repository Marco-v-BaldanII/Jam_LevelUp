using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Wolf_State
{
    IDLE,
    SELECTED,
    MOVING,
    TALKING,
    MINING,
    BUILDING,
    FIGHTING,
    PLAYING,
    WALKING_TO_NOTHING,
}

public enum Wolf_Mood
{
    NORMAL,
    ENRAGED
}


public class Wolf_AI : MonoBehaviour
{
    public int movement_speed = 3;

    public bool _isDragging;

    public wolf_task my_task;
    public GameObject wolf_city;
    public GameObject marshmallow;
    public GameObject speech_bubble;
    public GameObject buffer;

    public GameObject cross;

    private Vector3 buffer_pos;
    private Vector3 direction = Vector3.zero;
    private Rigidbody2D rigid;
    private Animator animator;
    private Collider2D collider;

    public bool moving_towards_task = false;
    public bool moving_towards_base = false;

    public Color enragedColor = Color.red;
    public bool has_cotton = false;

    public int life = 1;
    public int damage;
    public Wolf_State my_state = Wolf_State.IDLE;
    public Wolf_Mood my_mood = Wolf_Mood.NORMAL;

    private SpriteRenderer spriteRenderer;
    private Sheep targetSheep;
    private Wolf_AI targetEnragedWolf;

    private Vector3 library_pos;
    private Vector3 nothing_direction;

    bool showingX = false;
   

    private Vector3 talking_pos;
    bool right_talk;
    bool started_talking = false;

    private Vector3 task_pos_offset = Vector2.zero;

    bool whistled = false;
    bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        if (my_mood == Wolf_Mood.ENRAGED)
        {
            gameObject.layer = LayerMask.NameToLayer("EnragedWolf");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Wolf");
        }

        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        life = 1;
        damage = 1;
        StartCoroutine("IdleMovement");
        StartCoroutine("Task_Offset");

    }

    // Update is called once per frame
    void Update()
    {
        buffer.transform.position = buffer_pos;

        float anim_speed = rigid.velocity.magnitude / 4.0f;
        anim_speed = Mathf.Clamp(anim_speed, 0.4f, 1.2f);
        animator.speed = anim_speed;

       // Debug.Log(rigid.velocity.magnitude);

        switch(my_mood)
        {
            case Wolf_Mood.NORMAL:
                switch (my_state)
                {
                    case Wolf_State.IDLE:
                        if (!whistled)
                        {
                            rigid.velocity = direction.normalized * (movement_speed / 2);
                            has_cotton = false;
                        }

                        break;
                    case Wolf_State.MOVING:
                        if (moving_towards_task && my_task != null)
                        {
                            direction = my_task.transform.position + task_pos_offset - transform.position;
                            rigid.velocity = direction.normalized * movement_speed;

                        }
                        else if (has_cotton && my_task != null)
                        {
                            direction = wolf_city.transform.position + task_pos_offset - transform.position;
                            rigid.velocity = direction.normalized * movement_speed;
                            Debug.Log("back to base");
                        }
                        else if (targetSheep != null)
                        {
                            direction = targetSheep.transform.position - transform.position;
                            rigid.velocity = direction.normalized * movement_speed;
                            Debug.Log("Targeted sheep");
                        }
                        else if (targetEnragedWolf != null)
                        {
                            direction = targetEnragedWolf.transform.position - transform.position;
                            rigid.velocity = direction.normalized * movement_speed;
                            Debug.Log("Targeted enraged wolf");
                        }
                        break;
                    case Wolf_State.MINING:
                        if (moving_towards_task && my_task != null)
                        {
                            direction = my_task.transform.position + task_pos_offset - transform.position;
                            rigid.velocity = direction.normalized * movement_speed;

                        }
                        else if (has_cotton && my_task != null)
                        {
                            direction = wolf_city.transform.position + task_pos_offset - transform.position;
                            rigid.velocity = direction.normalized * movement_speed;
                            Debug.Log("back to base");
                        }
                        break;
                    case Wolf_State.FIGHTING:
                        StopMovement();
                        break;
                    case Wolf_State.TALKING:

                        direction = talking_pos - transform.position;
                        rigid.velocity = direction.normalized * movement_speed;

                        if (Vector2.Distance(transform.position, talking_pos) <= 2 && started_talking == false)
                        {
                            started_talking = true;
                            StartCoroutine("Talk");
                        }

                        break;
                    case Wolf_State.PLAYING:
                        direction = library_pos - transform.position;
                        rigid.velocity = direction.normalized * movement_speed;
                        spriteRenderer.color = Color.cyan;
                        break;
                    case Wolf_State.WALKING_TO_NOTHING:
                        direction = nothing_direction - transform.position;
                        rigid.velocity = direction.normalized * movement_speed;
                        
                        if(Vector2.Distance(transform.position, nothing_direction) < 0.5f)
                        {
                            Back_To_Idle();
                            rigid.velocity = Vector2.zero;
                        }

                        break;
                }
                break;
            case Wolf_Mood.ENRAGED:
                switch(my_state)
                {
                    case Wolf_State.IDLE:
                        rigid.velocity = direction.normalized * (movement_speed / 2);
                        has_cotton = false;
                        break;
                    case Wolf_State.TALKING:

                        direction = talking_pos - transform.position;
                        rigid.velocity = direction.normalized * movement_speed;

                        if (Vector2.Distance(transform.position, talking_pos) <= 2 && started_talking == false)
                        {
                            started_talking = true;
                            StartCoroutine("Talk");
                        }
                        break;
                    case Wolf_State.FIGHTING:
                        StopMovement();
                        break;
                }
                break;

        }
        
        if((rigid.velocity.x > 0 && transform.localScale.x < 0) || (rigid.velocity.x < 0 && transform.localScale.x > 0) && my_state != Wolf_State.TALKING)
        {

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        

        //Debug.Log(has_cotton != marshmallow.activeSelf);
        if (has_cotton != marshmallow.activeSelf)
        {
            marshmallow.SetActive(has_cotton);
        }

        spriteRenderer.color = my_mood == Wolf_Mood.ENRAGED ? enragedColor : spriteRenderer.color;


    }

    public void OnMouseDrag()
    {
        if (my_state != Wolf_State.TALKING && my_state != Wolf_State.PLAYING && my_mood != Wolf_Mood.ENRAGED)
        {
            if (!_isDragging)
            {
                Back_To_Idle();
                buffer_pos = transform.position;
            }
            _isDragging = true;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            collider.isTrigger = true;
        }
        else if(!showingX)
        {
            StartCoroutine("RefuseTOMove");
        }


    }


    private void OnMouseUp()
    {
        bool isColliding = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Default")) != null;

        if (!isColliding)
        {
            my_state = Wolf_State.WALKING_TO_NOTHING;
            nothing_direction = transform.position;
            my_task = null; moving_towards_base = false; moving_towards_task = false;


            Debug.Log("Not colliding with anything.");
        }

        if (my_state != Wolf_State.TALKING && my_state != Wolf_State.PLAYING && my_mood != Wolf_Mood.ENRAGED)
        {
            transform.position = buffer_pos;
            _isDragging = false;
            has_cotton = false;
            
        }

        collider.isTrigger = false;
        //if(my_state == Wolf_State.MINING) { my_state = Wolf_State.IDLE; }

    }

    public void OnMouseEnter()
    {
        // Check that they are idle
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Task") == true && _isDragging == false)
        {
            my_state = Wolf_State.MOVING;
            moving_towards_task = false;
        }
        else if (collision.gameObject.CompareTag("Whistle") == true && my_mood != Wolf_Mood.ENRAGED && my_state != Wolf_State.PLAYING)
        {
            Back_To_Idle();
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Whistle") == true && my_mood != Wolf_Mood.ENRAGED && my_state != Wolf_State.PLAYING)
        {
            whistled = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Whistle") == true && my_mood != Wolf_Mood.ENRAGED && my_state != Wolf_State.PLAYING)
        {
            whistled = false;

        }
    

        if(collision.gameObject.CompareTag("Sheep") == true)
        {
            Sheep sheep = collision.gameObject.GetComponent<Sheep>();
            if (sheep != null && _isDragging == true)
            {
                targetSheep = sheep;
                my_state = Wolf_State.MOVING;
                my_task = null;
                has_cotton = false;

            }
        }

        if (collision.gameObject.CompareTag("Wolf") == true)
        {
            Wolf_AI wolf = collision.gameObject.GetComponent<Wolf_AI>();
            if (wolf != null && _isDragging == true && wolf.my_mood == Wolf_Mood.ENRAGED)
            {
                targetEnragedWolf = wolf;
                my_state = Wolf_State.MOVING;
                my_task = null;
                has_cotton = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sheep") == true)
        {
            Sheep sheep = collision.gameObject.GetComponent<Sheep>();
            if (sheep != null && !_isDragging)
            {
                my_state = Wolf_State.FIGHTING;
                TakeDamage(sheep.damage);
                sheep.TakeDamage(damage);
                sheep.my_State = Sheep_State.FIGHTING;
            }

        }

        if(targetEnragedWolf != null)
        {
            Debug.Log("Auu");
            if (collision.gameObject.CompareTag("Wolf") == true)
            {
                Wolf_AI wolf = collision.gameObject.GetComponent<Wolf_AI>();
                if (wolf != null && !_isDragging && wolf.my_mood == Wolf_Mood.ENRAGED)
                {
                    my_state = Wolf_State.FIGHTING;
                    TakeDamage(wolf.damage);
                    wolf.TakeDamage(damage);
                    wolf.my_state = Wolf_State.FIGHTING;
                }
            }
        }


    }

    public void ChangeTask(wolf_task task, Wolf_State state = Wolf_State.MOVING )
    {
        if(my_mood != Wolf_Mood.ENRAGED)
        {
            ResetVelocity();
            my_task = task;
            moving_towards_task = true;
            my_state = state;
        }
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void StopMovement()
    {
        rigid.velocity = Vector2.zero;
    }


    //----------------IDLE CORROUTINES-----------------//
    private IEnumerator IdleMovement()
    {
        while (life > 0)
        {
            Vector2 new_direction = Vector2.zero;

            

            if (my_state == Wolf_State.IDLE)
            {


                new_direction = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                int rand_sign = Random.Range(0, 2);
                if(rand_sign == 1)
                {
                    new_direction *= -1;
                }
                else
                {
                    new_direction *= 1;
                }
                direction = new_direction;

            }
            float sec = Random.Range(2.0f, 5.0f);

            yield return new WaitForSecondsRealtime(sec);

            yield return null;
        }

    }

  
    public void StartTalking(Vector3 destination, bool right)
    {
        my_state = Wolf_State.TALKING;
        talking_pos = destination;
        right_talk = right;
        has_cotton = false;
        if (right)
        {
            talking_pos.x -= 2;
        }
    }

    //-----------------Talking corroutine----------------//
    private IEnumerator Talk()
    {
        speech_bubble.SetActive(true);
        if (!right_talk) { transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); }
        yield return new WaitForSeconds(3);
        speech_bubble.SetActive(false);
        my_state = Wolf_State.IDLE;
        started_talking = false;
        
    }

    private IEnumerator RefuseTOMove()
    {
        showingX = true;
        for (int i = 0; i <5; ++i)
        {
            cross.SetActive(true);
            yield return new WaitForSecondsRealtime(0.15f);
            cross.SetActive(false);
        }
        showingX = false;
    }

    public void ResetVelocity() { rigid.velocity = Vector3.zero; }


    public void StartPlayTime(Transform library)
    {
        library_pos = new Vector3(library.position.x + Random.Range(-2.0f,2.0f), library.position.y + Random.Range(-1.0f, 1.0f), library.position.z);
        my_state = Wolf_State.PLAYING;
        my_task = null;
        has_cotton = false;
    }

    public void Back_To_Idle()
    {
        my_state = Wolf_State.IDLE;
        my_task = null;
        moving_towards_base = false; moving_towards_task = false;
        if(spriteRenderer != null) spriteRenderer.color = Color.white;
    }

    private IEnumerator Task_Offset()
    {
        while(my_mood == Wolf_Mood.NORMAL)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            task_pos_offset = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }

}

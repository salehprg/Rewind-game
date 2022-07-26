using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float maxspeed;
    public float speed;
    public float jumpHeight;
    public float jumpcut_multiplier;
    public bool falling , rising , climbing;

    Vector2 moveinput;

    int jumpcount = 0;
    bool canjump , jump , jumpcut;
    bool touchground;

    Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {  
        canjump = true;
        jumpcut = false;
    }

    
    Vector2 input;
    void Update()
    {
        moveinput = new Vector2(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical"));

        if(jumpcount < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            jumpcount++;

            jump = true;
        }
        if(canjump && Input.GetKeyUp(KeyCode.Space))
        {
            jumpcut = true;
        }
        
        if(rigidbody.velocity.y < 0)
        {
            rising = false;
            falling = true;
        }
        else if(rigidbody.velocity.y > 0)
        {
            rising = true;
            falling = false;
        }
        else
            rising = falling = false;

    }

    private void FixedUpdate() 
    {
        float speedX = maxspeed * moveinput.x;
		speedX = Mathf.Lerp(rigidbody.velocity.x, speedX, 1);

        rigidbody.velocity = new Vector2(speedX , rigidbody.velocity.y);
        
        if(jump)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
            jump = false;
        }
        if(jumpcut)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x , rigidbody.velocity.y * jumpcut_multiplier);
            jumpcut = false;
        }

        
        //transform.position = Vector2.Lerp(transform.position , new Vector2(transform.position.x + input.x , transform.position.y + input.y) , Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        ContactPoint2D contactPoint = other.contacts.Where(x => x.otherCollider.gameObject == this.gameObject).FirstOrDefault();

        if(contactPoint.collider != null)
        {
            float direction = Vector2.Dot(contactPoint.normal , Vector2.up);

            if(direction >= 0.4f)
            {
                Debug.Log(jumpcount);
                jumpcount = 0;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        ContactPoint2D contactPoint = other.contacts.Where(x => x.otherCollider.gameObject == this.gameObject).FirstOrDefault();

        if(contactPoint.collider != null)
        {
            float direction = Vector2.Dot(contactPoint.normal , Vector2.up);

            if(direction >= -0.1f && direction <= 0.1f)
            {
                climbing = true;
                jumpcount = 0;
            }
            else
                climbing = false;
        }
    }
}

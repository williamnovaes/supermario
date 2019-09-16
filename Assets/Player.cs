using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator anim;

    private float horizontal;
    private bool walking;
    public float velocity;
    private bool facingRigth;

    private float run;
    private bool running;

    public bool jump;
    public float jumpForce;

    public bool grounded;
    public Transform groundCheck;

    public bool roll;

    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        facingRigth = true;
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1"))  
        {
            roll = true;
        }
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        run = Input.GetAxis("Fire3");

       // tratamento mudar de direcao enquanto corre
       // if (h * rb2d.velocity.x < maxSpeed)
       //     rb2d.AddForce(Vector2.right * h * moveForce);

       // if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
       //     rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if ((!facingRigth && horizontal > 0) || (facingRigth && horizontal < 0))
        {
            Flip();
        }

        playerRB.velocity = new Vector2(horizontal * velocity, playerRB.velocity.y);
        if (horizontal != 0f)
        {
            walking = true;
        } else
        {
            walking = false;
        }

        if (jump && !roll)
        {
            anim.SetTrigger("Jump");
            playerRB.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        else if (roll && !jump)
        {
            anim.SetTrigger("Roll");
            playerRB.AddForce(new Vector2(0f, jumpForce));
            roll = false;
        }

        anim.SetBool("Walking", walking);
        anim.SetFloat("SpeedY", playerRB.velocity.y);
        //anim.SetBool("Jumping", jumping);
    }

    void Flip() {
        facingRigth = !facingRigth;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

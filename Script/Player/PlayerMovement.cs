using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerState ps;
    Animator anim;
    AudioManager audioManager;

    [Header("xAxis Movement")]
    [SerializeField] float walkSpeed= 10;
    [Space(5)]

    [Header("Y Axis Movement")]
    [SerializeField] int maxJumpTime=2;
    [SerializeField] float jumpSpeed=12;
    [SerializeField] float fallSpeed=12;
    [SerializeField] float timeJump= 0.2f;
    [SerializeField] float jumpTimehold= 0.07f;
    [Space(5)]

    [Header("Ground Checking")]
    [SerializeField] Transform groundTransform;
    [SerializeField] float groundCheckY= 0.41f;
    [SerializeField] float groundCheckX= 0.3f;
    [SerializeField] LayerMask groundLayer;
    [Space(5)]

    [Header("Roof Checking")]
    [SerializeField] Transform roofTransform; 
    [SerializeField] float roofCheckY=0.41f;
    [SerializeField] float roofCheckX= 0.3f;
    [Space(5)]

    [Header("Partical")]
    [SerializeField] ParticleSystem deathEffect;

    Vector3 spawnPoint;
    int jumpTime;
    float xAxis;
    float yAxis;
    float gravity;
    int stepsRecoilX;
    int stepsRecoilY;
    float jumpedTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.localPosition;
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = AudioManager.instance;
        if (ps == null)
        {
            ps = GetComponent<PlayerState>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();

        Walk(xAxis);
        Flip();
    }
    void FixedUpdate()
    {
        Jump();
        
    }
    void Walk(float xAxis)
    {
        rb.velocity = new Vector2(xAxis * walkSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            ps.isWalking = true;
        }
        else
        {
            ps.isWalking = false;
        }
        if (xAxis > 0)
        {
            ps.isLookingRight = true;
        }
        else if (xAxis < 0)
        { 
            ps.isLookingRight = false;
        }
    }
    void Flip()
    {
        if (xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }
    void StopJumpFast()
    {
        ps.isJumping = false;
        jumpedTime = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        anim.SetBool("IsJumping", false);
    }
    void StopJumpSlow()
    {
        ps.isJumping = false;
        jumpedTime = 0;
        anim.SetBool("IsJumping", false);
    }
    void Jump()
    {
        if (ps.isJumping)
        {
            if (jumpedTime < timeJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpedTime+=Time.deltaTime;
            }
            else
            {
                StopJumpSlow();
            }
            if (Roofed())
            {
                StopJumpFast();
            }
        }
        if (rb.velocity.y < -Mathf.Abs(fallSpeed))
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
        }
    }
    bool Grounded()
    {
       if (Physics2D.Raycast(groundTransform.position, Vector2.down, groundCheckY, groundLayer) 
            || Physics2D.Raycast(groundTransform.position + new Vector3(-groundCheckX, 0), Vector2.down, groundCheckY, groundLayer) 
            || Physics2D.Raycast(groundTransform.position + new Vector3(groundCheckX, 0), Vector2.down, groundCheckY, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool Roofed()
    {
        if (Physics2D.Raycast(roofTransform.position, Vector2.up, roofCheckY, groundLayer) 
            || Physics2D.Raycast(roofTransform.position + new Vector3(roofCheckX, 0), Vector2.up, roofCheckY, groundLayer) 
            || Physics2D.Raycast(roofTransform.position + new Vector3(roofCheckX, 0), Vector2.up, roofCheckY, groundLayer))
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }
    void Inputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if (transform.rotation.z != 0) transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y,0,transform.rotation.w);
        if (transform.parent != null){
            ps.isGrounded = true;
            anim.SetBool("IsGrounded", true);
            jumpTime = maxJumpTime;
        }
        else if (Grounded()&&!ps.isJumping)
        {
            ps.isGrounded = true;
            anim.SetBool("IsGrounded", true);
            jumpTime = maxJumpTime;
        }
        else
        {
            ps.isGrounded = true;
            anim.SetBool("IsGrounded", false);
        }

        if (Input.GetButtonDown("Jump") && jumpTime>0)
        {
            if(audioManager!=null) audioManager.PlaySound("Jump");
            jumpTime--;
            ps.isJumping = true;
            anim.SetBool("IsJumping", true);
        }
        if (!Input.GetButton("Jump") && jumpedTime < jumpTimehold && ps.isJumping)
        {
            StopJumpSlow();
        }
        else if (!Input.GetButton("Jump") && jumpedTime < timeJump && jumpedTime > jumpTimehold && ps.isJumping)
        {
            StopJumpFast();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawLine(groundTransform.position, groundTransform.position + new Vector3(0, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(-groundCheckX, 0), groundTransform.position + new Vector3(-groundCheckX, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(groundCheckX, 0), groundTransform.position + new Vector3(groundCheckX, -groundCheckY));

        Gizmos.DrawLine(roofTransform.position, roofTransform.position + new Vector3(0, roofCheckY));
        Gizmos.DrawLine(roofTransform.position + new Vector3(-roofCheckX, 0), roofTransform.position + new Vector3(-roofCheckX, roofCheckY));
        Gizmos.DrawLine(roofTransform.position + new Vector3(roofCheckX, 0), roofTransform.position + new Vector3(roofCheckX, roofCheckY));
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (audioManager != null) audioManager.PlaySound("Death");
            Instantiate(deathEffect, transform.localPosition, Quaternion.identity);
            transform.parent = null;
            transform.localPosition = spawnPoint;
            PlayerScore.AddDeath();
        }
        if (collision.gameObject.tag.Equals("Obstacles"))
        {
            if (audioManager != null) audioManager.PlaySound("Death");
            Instantiate(deathEffect, transform.localPosition, Quaternion.identity);
            transform.parent = null;
            transform.localPosition = spawnPoint;
            PlayerScore.AddDeath();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Spawn"))
        {
            transform.parent = null;
            spawnPoint = collision.transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDropObject : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckX;
    [SerializeField] float groundCheckY;
    [SerializeField] float maxFallSpeed;
    Rigidbody2D rb;
    float gravity=10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y,-maxFallSpeed, Mathf.Infinity));
        if (CheckGround())
        {
            rb.velocity = new Vector2();
            rb.isKinematic = true;
        }
    }
    // Update is called once per frame
    public void Triggered()
    {
        rb.gravityScale = gravity;
    }
    public void Destroyed()
    {
        Destroy(this.gameObject);
    }
    bool CheckGround()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down,groundCheckY,groundLayer)
            || Physics2D.Raycast(transform.position+new Vector3(groundCheckX,0), Vector2.down, groundCheckY, groundLayer)
            || Physics2D.Raycast(transform.position-new Vector3(groundCheckX,0), Vector2.down, groundCheckY, groundLayer))
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckY));
        Gizmos.DrawLine(transform.position + new Vector3(-groundCheckX, 0), transform.position + new Vector3(-groundCheckX, -groundCheckY));
        Gizmos.DrawLine(transform.position + new Vector3(groundCheckX, 0), transform.position + new Vector3(groundCheckX, -groundCheckY));
    }
}

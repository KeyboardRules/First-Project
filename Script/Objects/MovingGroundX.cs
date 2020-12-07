using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGroundX : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] float speed;
    [SerializeField] float xFrom;
    [SerializeField] float xTo;

    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        if (xFrom < xTo) direction = Vector3.right;
        else direction = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject(xFrom, xTo);
    }
    void MoveObject(float xFrom, float xTo)
    {
        if (xFrom < xTo)
        {
            if (transform.localPosition.x < xFrom)
            {
                direction = Vector3.right;
            }
            if (transform.localPosition.x > xTo)
            {
                direction = Vector3.left;
            }
            transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
        }
        else
        {
            if (transform.localPosition.x > xFrom)
            {
                direction = Vector3.left;
            }
            if (transform.localPosition.x < xTo)
            {
                direction = Vector3.right;
            }
            transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}

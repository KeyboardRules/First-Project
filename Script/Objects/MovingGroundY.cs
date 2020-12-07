using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingGroundY : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] float speed;
    [SerializeField] float yFrom;
    [SerializeField] float yTo;

    Vector3 direction;
    void Start()
    {
        if (yFrom < yTo) direction = Vector3.up;
        else direction = Vector3.down;
    }
    void Update()
    {
        MoveObject(yFrom, yTo);
    }
    void MoveObject(float yFrom,float yTo)
    {
        if(yFrom<yTo)
        {
            if (transform.localPosition.y < yFrom)
            {
                direction = Vector3.up;
            }
            if (transform.localPosition.y > yTo)
            {
                direction = Vector3.down;
            }
            transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
        }
        else
        {
            if (transform.localPosition.y > yFrom)
            {
                direction = Vector3.down;
            }
            if (transform.localPosition.y < yTo)
            {
                direction = Vector3.up;
            }
            transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            
        {
            Debug.Log("in");
            collision.gameObject.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("out");
            collision.gameObject.transform.SetParent(null);
        }
    }
}

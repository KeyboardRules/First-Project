using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectCustom3 : MonoBehaviour
{
    [Header("Time And Speed")]
    [SerializeField] bool isCycle;
    [SerializeField] float speed;
    [Space(5)]

    [Header("Moving Distance")]
    [SerializeField] float xDistance;
    [SerializeField] float yDistance;

    Vector3 firstPosition;
    Vector3 direction;
    float sumDistance;
    bool isTrigger;
    void Start()
    {
        firstPosition = transform.localPosition;
        if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
        {
            sumDistance = Mathf.Abs(xDistance);
        }
        else
        {
            sumDistance = Mathf.Abs(yDistance);
        }
    }
    void Update()
    {
        Moving();
    }
    public void Triggered()
    {
        direction = new Vector3(xDistance / sumDistance, yDistance / sumDistance);
        isTrigger = true;
    }
    public void Recoil()
    {
        direction=new Vector3(-xDistance / sumDistance, -yDistance / sumDistance);
    }
    public bool IsTrigger()
    {
        return isTrigger;
    }
    void Moving()
    {
        if (xDistance > 0 && transform.localPosition.x > firstPosition.x + xDistance || xDistance < 0 && transform.localPosition.x < firstPosition.x + xDistance)
        {
            transform.localPosition =  new Vector3(firstPosition.x+xDistance, transform.localPosition.y);
            if (isCycle)
            {
                direction = new Vector3(-xDistance / sumDistance, direction.y);
            }
            else
            {
                direction = new Vector3(0, direction.y);
                isTrigger = false;
            }
        }
        if (xDistance > 0 && transform.localPosition.x < firstPosition.x || xDistance < 0 && transform.localPosition.x > firstPosition.x)
        {
            transform.localPosition = new Vector3(firstPosition.x, transform.localPosition.y);
            direction = new Vector3(0, direction.y);
            isTrigger = false;

        }
        if (yDistance > 0 && transform.localPosition.y > firstPosition.y + yDistance || yDistance < 0 && transform.localPosition.y < firstPosition.y + yDistance)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,firstPosition.y+ yDistance);
            if (isCycle)
            {
                direction = new Vector3(direction.x, -yDistance / sumDistance);
            }
            else
            {
                direction = new Vector3(direction.x, 0);
                isTrigger = false;
            }
        }
        if (yDistance > 0 && transform.localPosition.y < firstPosition.y || yDistance < 0 && transform.localPosition.y > firstPosition.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, firstPosition.y);
            direction = new Vector3(direction.x, 0);
            isTrigger = false;
        }
        transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && tag != "Enemy" && tag != "Obstacles")
        {
            collision.collider.transform.SetParent(this.transform);
            Triggered();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && tag != "Enemy" && tag != "Obstacles")
        {
            collision.collider.transform.SetParent(null);
            Recoil();
        }
    }
}

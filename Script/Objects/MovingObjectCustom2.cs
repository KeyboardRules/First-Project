using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectCustom2 : MonoBehaviour
{
    [Header("Time And Speed")]
    [SerializeField] float delayTime;
    [SerializeField] float speed;
    [Space(5)]

    [Header("Moving Distance")]
    [SerializeField] float xDistance;
    [SerializeField] float yDistance;

    Vector3 firstPosition;
    Vector3 direction;
    bool countdown;
    float countTime;
    float sumDistance;

    // Start is called before the first frame update
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
        direction = new Vector3(xDistance / sumDistance, yDistance / sumDistance);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }
    void Moving()
    {
        if (!countdown)
        {
            if (xDistance > 0 && transform.localPosition.x > firstPosition.x + xDistance || xDistance < 0 && transform.localPosition.x < firstPosition.x + xDistance)
            {
                transform.localPosition = new Vector3(firstPosition.x + xDistance, transform.localPosition.y);
                direction = new Vector3(-xDistance / sumDistance, direction.y);
                countdown = true;
            }
            if (xDistance > 0 && transform.localPosition.x < firstPosition.x || xDistance < 0 && transform.localPosition.x > firstPosition.x)
            {
                transform.localPosition = new Vector3(firstPosition.x, transform.localPosition.y);
                direction = new Vector3(xDistance / sumDistance, direction.y);
                countdown = true;
            }
            if (yDistance > 0 && transform.localPosition.y  > firstPosition.y + yDistance || yDistance < 0 && transform.localPosition.y  < firstPosition.y + yDistance)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, firstPosition.y + yDistance);
                direction = new Vector3(direction.x, -yDistance / sumDistance);
                countdown = true;
            }
            if (yDistance > 0 && transform.localPosition.y < firstPosition.y || yDistance < 0 && transform.localPosition.y > firstPosition.y )
            {
                transform.localPosition = new Vector3(transform.localPosition.x, firstPosition.y);
                direction = new Vector3(direction.x, yDistance / sumDistance);
                countdown = true;
            }
            transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
        }
        else
        {
            countTime += Time.deltaTime;
            if (countTime >= delayTime)
            {
                countTime = 0;
                countdown = false;
                return;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&tag!="Enemy"&&tag!="Obstacles")
        {
            collision.collider.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && tag != "Enemy" && tag != "Obstacles")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}

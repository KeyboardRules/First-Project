using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class MovingObjectCustom : MonoBehaviour
{
    [Header("Time And Speed")]
    [SerializeField] float delayTime;
    [SerializeField] float speed;
    [Space(5)]

    [Header("Moving Distance")]
    [SerializeField] float xFrom;
    [SerializeField] float xTo;
    [SerializeField] float yFrom;
    [SerializeField] float yTo;

    float countTime;
    float sumDistance;
    Vector3 direction;

    void Start()
    {
        if (math.abs(xFrom-xTo) > math.abs(yFrom-yTo))
        {
            sumDistance = math.abs(xFrom-xTo);
        }
        else
        {
            sumDistance = math.abs(yFrom-yTo);
        }
        direction = new Vector3((xTo - xFrom )/ sumDistance, (yTo - xFrom)/sumDistance);
    }
    // Update is called once per frame
    void Update()
    {
        Moving();
    }
    void Moving()
    {
        if (xTo - xFrom > 0 && transform.localPosition.x < xFrom || xTo - xFrom < 0 && transform.localPosition.x > xFrom )
        {
            direction = new Vector3((xTo - xFrom) / sumDistance, direction.y);
        }
        else if(xTo - xFrom > 0 && transform.localPosition.x > xTo || xTo - xFrom < 0 && transform.localPosition.x < xTo)
        {
            direction = new Vector3((-xTo + xFrom) / sumDistance, direction.y);
        }
        if (yTo - yFrom > 0 && transform.localPosition.y < yFrom || yTo - yFrom < 0 && transform.localPosition.y > yFrom)
        {
            direction = new Vector3(direction.x, (yTo - yFrom) / sumDistance);
        }
        else if (yTo - yFrom > 0 && transform.localPosition.y > yTo || yTo - yFrom < 0 && transform.localPosition.y < yTo)
        {
            direction = new Vector3(direction.x, (-yTo + yFrom) / sumDistance);
        }
        if (direction == new Vector3())
        {
            if (countTime < delayTime)
            {
                countTime += Time.deltaTime;
                Debug.Log(countTime);
            }
            else
            {
                countTime = 0;
                return;
            }
        }
        transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
    }
}

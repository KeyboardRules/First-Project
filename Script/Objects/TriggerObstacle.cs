using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{
    [SerializeField] GameObject[] ostacle;
    // Start is called before the first frame update
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(GameObject os in ostacle)
            {
                if (os.GetComponent<TrapDropObject>())
                {
                    os.GetComponent<TrapDropObject>().Triggered();
                }
                if (os.GetComponent<MovingObjectCustom3>()&& !os.GetComponent<MovingObjectCustom3>().IsTrigger())
                {
                    os.GetComponent<MovingObjectCustom3>().Triggered();
                }
            }
        }
    }
}

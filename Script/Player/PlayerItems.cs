using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    bool hasKey;
    public bool HasKey()
    {
        return hasKey;
    }
    public void GetKey()
    {
        hasKey = true;
    }
}

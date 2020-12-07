using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void ChangeVolumn(float value)
    {
        FindObjectOfType<AudioManager>().ChangeVolumn(value);
    }
}

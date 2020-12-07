using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string themeName;
    private AudioManager audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = AudioManager.instance;
        audio.ChangeSceneTheme(themeName);
    }
}

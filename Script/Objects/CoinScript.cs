using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    AudioManager audio;
    void Start()
    {
        audio = AudioManager.instance;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScore.AddPoint();
            if(audio!=null) audio.PlaySound("Coin");
            Destroy(this.gameObject);
        }
    }
}

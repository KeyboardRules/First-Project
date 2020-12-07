using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    TextMeshProUGUI textCoin;
    int death;
    void Start()
    {
        textCoin = GetComponent<TextMeshProUGUI>();
        death = PlayerScore.GetDeath();
        textCoin.text = "x " + death;
    }

    // Update is called once per frame
    void Update()
    {
        if (death != PlayerScore.GetDeath())
        {
            death = PlayerScore.GetDeath();
            textCoin.text = "x " + death;
        }
    }
}

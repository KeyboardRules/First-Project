using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    TextMeshProUGUI textCoin;
    int coin;
    void Start()
    {
        textCoin = GetComponent<TextMeshProUGUI>();
        coin = PlayerScore.GetScore();
        textCoin.text = "x " + coin;
    }

    // Update is called once per frame
    void Update()
    {
        if (coin != PlayerScore.GetScore())
        {
            coin = PlayerScore.GetScore();
            textCoin.text = "x " + coin;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalSummarize : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Total  :   "+PlayerScore.GetTotal();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSetup : MonoBehaviour
{
    TextMeshPro textDialog;
    private void Awake()
    {
        textDialog = GetComponent<TextMeshPro>();
    }
    public void SetupDialog(string text)
    {
        textDialog.text = text;
    }
}

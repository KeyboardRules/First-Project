using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel: MonoBehaviour
{
    [SerializeField] GameObject textDialog;
    [SerializeField] string text;
    bool isOpenDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerItems>().HasKey())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
            else
            {
                if (!isOpenDialog)
                {
                    StartCoroutine(OpenDialog(2f));
                }
            }
        }
    }
    IEnumerator OpenDialog(float time)
    {
        isOpenDialog = true;
        GameObject textSet = Instantiate(textDialog, transform.position + Vector3.up * 3, Quaternion.identity);
        textSet.GetComponent<DialogSetup>().SetupDialog(this.text);
        yield return new WaitForSeconds(time);
        Object.Destroy(textSet);
        isOpenDialog = false;
    }
}

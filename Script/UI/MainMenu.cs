using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsDisable", false);
    }
    public void PlayClick()
    {
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        anim.SetBool("IsDisable", true);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(1);
    }
}

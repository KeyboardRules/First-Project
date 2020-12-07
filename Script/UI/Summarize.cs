using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Summarize : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsDisable", false);
    }
    public void onMenuClick()
    {
        StartCoroutine(ChangeScene(0));
    }
    public void onReplayClick()
    {
       StartCoroutine(ChangeScene(1));
    }
    IEnumerator ChangeScene(int scene)
    {
        anim.SetBool("IsDisable", true);
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(scene);
    }
}

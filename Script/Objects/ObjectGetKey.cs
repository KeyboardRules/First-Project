using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetKey : MonoBehaviour
{
    [SerializeField] GameObject textDialog;
    [SerializeField] string text;
    bool isOpenDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& collision.GetComponent<PlayerItems>())
        {
            if (!collision.GetComponent<PlayerItems>().HasKey())
            {
                StartCoroutine(GetKey(collision));
            }
        }
    }
    IEnumerator GetKey(Collider2D collision)
    {
        yield return new WaitForSeconds(.5f);
        collision.GetComponent<PlayerItems>().GetKey();
        GameObject dialog = Instantiate(textDialog, transform.position + Vector3.up * 3, Quaternion.identity);
        dialog.GetComponent<DialogSetup>().SetupDialog(text);
        Object.Destroy(dialog, 2f);

    }
}

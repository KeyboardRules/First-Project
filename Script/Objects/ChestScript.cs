using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestScript : MonoBehaviour
{
    [Serializable]
    struct ContainObject
    {
        public GameObject objectContain;
        public int number;
    }
    [SerializeField] ContainObject[] containObjects;
    AudioManager audioManager;

    System.Random rnd;
    Animator anim;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        anim = GetComponent<Animator>();
        //OpenChest();
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isOpen)
            {
                StartCoroutine(OpenChest());
                isOpen = true;
            }
        }
    }
    IEnumerator OpenChest()
    {
        anim.SetBool("isOpen",true);
        if(audioManager!=null) audioManager.PlaySound("Chest");
        yield return new WaitForSeconds(0.5f);
        foreach(ContainObject x in containObjects)
        {
            for(int i = 0; i < x.number; i++)
            {
                GameObject contain=Instantiate(x.objectContain, transform.position+Vector3.up/1.2f, Quaternion.identity);
                contain.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-3,3),Random.Range(0,6)),ForceMode2D.Impulse);
            }
        }
    }
}

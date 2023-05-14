using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDialogue : MonoBehaviour
{
    Animator UIanimator;
    private bool firsttalk=false;
    GameObject playersave;
 void Start()
    {
        UIanimator = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Animator>();
        playersave = GameObject.FindGameObjectWithTag("playersave");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player") {
            UIanimator.SetBool("talk",true);
            if (!firsttalk)
            {
                playersave.GetComponent<playersave>().AddNpcTime();
               UIanimator. gameObject.GetComponentInChildren<changeconversation>().ChangeConversation();
                UIanimator.gameObject.GetComponentInChildren<changeselection>().ChangeConversation();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firsttalk = true;
            UIanimator.SetBool("talk", false);
        }
    }
}

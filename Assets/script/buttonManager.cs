using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    Animator godUI;
    private void Start()
    {
        godUI = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Animator>();
    }
    public void selectionButton() {
        godUI.SetBool("talk",false);
    }
}

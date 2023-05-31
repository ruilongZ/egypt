using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    Animator godUI;
    GameObject player;
    private void Start()
    {
        godUI = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
    }
    public void selectionButton() {
        godUI.SetBool("talk",false);
    }
    public void Resetbutton()
    {
        SceneManager.LoadScene("start");
    }
    public void Menubutton()
    {
        SceneManager.LoadScene("menu");
    }

}

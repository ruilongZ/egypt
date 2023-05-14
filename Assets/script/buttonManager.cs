using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    Animator godUI;
    GameObject player;
    public Scrollbar bloodbar;
    private void Start()
    {
        godUI = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        setbloodbar();
    }
    public void selectionButton() {
        godUI.SetBool("talk",false);
    }
    public void setbloodbar() {
        if (player != null)
        {
            bloodbar.size = player.GetComponentInChildren<PlayControl>().currentlife / player.GetComponentInChildren<PlayControl>().maxlife;
        }
        else {
            bloodbar.size = 0;
        }
    }
}

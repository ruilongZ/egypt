using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapControl : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= transform.parent.transform.position.y)
        {
            Renderer.sortingOrder = 7;
        }
        else {
            Renderer.sortingOrder = 5;
        }
    }
}

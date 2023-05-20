using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    GameObject player;
    Animator animator;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position + new Vector3(-0.7f, 0.5f, 0), transform.position) < 0.1f)
        {
        }
        else
        {
            transform.Translate((player.transform.position - transform.position + new Vector3(-0.7f, 0.5f, 0)).normalized * speed * Time.deltaTime);
        }
        animator.SetFloat("isright",(player.transform.position.x-transform.position.x)*100);
    }
}

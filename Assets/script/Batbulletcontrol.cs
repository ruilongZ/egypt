using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batbulletcontrol : MonoBehaviour
{
    GameObject player;
    Vector3 dir;
    Animator animator;
    float speed;
    bool die;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dir = player.transform.position + player.GetComponent<PlayerMovementNew>().dir-transform.position;
        animator = GetComponent<Animator>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            speed = 0;
        }
        else {
            speed = Random.Range(0.9f, 1.2f);
        }
        transform.Translate(dir * Time.deltaTime*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"|| other.tag == "block") {
            animator.SetTrigger("hit");
            Destroy(gameObject, 0.35f);
            die = true;
        }
    }
}

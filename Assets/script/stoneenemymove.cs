using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stoneenemymove : MonoBehaviour
{
    public float movespeed;
    public float currentspeed;
    private bool dead;
    public float speeddamp;

    public Animator animator;
    private CharacterController EnemyController;
    GameObject player;
    public bool blocked;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        EnemyController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentspeed = movespeed;
        dir = randomDir();
        animator.SetTrigger("move");
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            currentspeed = 0;
            animator.SetTrigger("die");
        }
        else
        {
            currentspeed = Mathf.Lerp(currentspeed, 0, speeddamp * Time.deltaTime);
            if (blocked)
            {
                dir = (player.transform.position - gameObject.transform.position).normalized;
            }
            EnemyController.Move(dir * Time.deltaTime * currentspeed);
            if (currentspeed < 0.5)
            {
                animator.SetTrigger("stop");
                if (currentspeed < 0.2)
                {
                    blocked = false;
                    currentspeed = movespeed;
                    animator.SetTrigger("move");
                    dir = randomDir();
                }
            }
        }
        animator.SetFloat("x", dir.x * 10);
    }

    Vector3 randomDir()
    {
        Vector3 dir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0).normalized;
        return dir;
    }
    public void block()
    {
        animator.SetTrigger("move");
        blocked = true;
        currentspeed = movespeed * 1.3f;
    }
    public void die()
    {
        dead = true;
        animator.SetTrigger("die");
    }
}

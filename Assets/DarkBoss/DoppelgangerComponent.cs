using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoppelgangerComponent : MonoBehaviour
{
    [Header("»ù´¡²ÎÊý")]
    public float currentLife;
    public float maxLife;


    Animator animator;
    GameObject player;
    Collider collider;
    public bool die = false;

    [Header("ËØ²Ä")]
    public GameObject Bullet;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentLife = maxLife;
        collider = this.GetComponent<Collider>();
        StartCoroutine("bullet");
    }
    IEnumerator bullet() {
        yield return new WaitForSeconds(2f);
        Instantiate(Bullet, this.transform.position, this.transform.rotation);
    }
    //Boss Get Damage
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet" || (other.tag == "Player" && other.name == "character"))
        {
            switch (other.tag)
            {
                case "playerbullet":
                    currentLife -= other.GetComponent<BulletMovementNew>().damage;
                    break;
                case "Player":
                    if (other.GetComponent<PlayControl>().sprintdamageequip && other.GetComponent<PlayControl>().ShiftPressed)
                    {
                        currentLife -= other.GetComponent<PlayControl>().defence;
                    }
                    break;
            }
            if (currentLife <= 0)
            {
                currentLife = 0;
                animator.SetTrigger("die");
                die = true;
                //if (currentMagicCircle)
                //{
                //    Destroy(currentMagicCircle);
                //}
                collider.enabled = false;
            }
        }
    }
}

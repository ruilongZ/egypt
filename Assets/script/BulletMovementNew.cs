using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BulletMovementNew : MonoBehaviour
{
    public float damage;
    GameObject player;
    float passtime;
    public Vector3 Offset;
    Animator animator;
    SphereCollider selfcollider;
    bool blocked;
    float passlife;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        selfcollider = GetComponent<SphereCollider>();
        damage = player.GetComponent<PlayerMovementNew>().damage;
    }

    void Update()
    { 
        passtime += Time.deltaTime;
        passlife += Time.deltaTime;
        if (passlife > player.GetComponent<PlayerMovementNew>().BulletRange && !blocked)
        {
            Destroy(gameObject);
        }
        if (blocked)
        {
            transform.Translate(Vector3.zero);
        }
        else
        {
            if (passtime >= 0.4f)
            {
                transform.Translate(Vector3.right * Time.deltaTime * player.GetComponent<PlayerMovementNew>().BulletSpeed);
            }
            else
            {
                transform.position = player.transform.position + Offset;
            }
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="block"|| other.tag == "Enemy" || other.tag == "boss") {
            blocked = true;
            StartCoroutine("DestoryBullet");
        }
    }
    public IEnumerator DestoryBullet() {
        animator.SetBool("destory", true);
        selfcollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBullet : MonoBehaviour
{
    GameObject player;
    Vector3 dir;
    Animator animator;
    public float MaxSpeed;
    public float FloatTime;
    public GameObject EnemySpawned;
    
    float speed;
    bool die = false;
    float t=0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dir = player.transform.position + player.GetComponent<PlayerMovementNew>().dir - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (die)
        {
            speed = 0;
            Instantiate(EnemySpawned, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            speed = Mathf.Lerp(MaxSpeed, 0.1f, t);
        }
        if (t<=1f&&speed>0.1f)
        {
            t += Time.fixedDeltaTime/FloatTime;
        }
        else if(speed<=0.1f)
        {
            die=true;
        }
        transform.Translate(dir * Time.deltaTime * speed);
    }
}

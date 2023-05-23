using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunbosscontrol : MonoBehaviour
{
    [Header("机制相关")]
    float damageCount;
    public float standdamagetolife;
    int hitcount;
    public int maxhitcount;
    bool isbird;
    public float turntobirdlife;


    [Header("基础参数")]
    public float currentlife;
    public float maxlife;
    public float turnlife;
    // Start is called before the first frame update
    void Start()
    {
        currentlife = maxlife;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isbird)
        {

        }
        else { 
        
        }
    }
    void rest() { }
    void avoidbullet() {

    }
    void shield() {

    }
    void turntobird() {
        isbird = true;
    }

    void birdbullet() {

    }
    void die() { 
    
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="playerbullet"||(other.tag=="Player"&&other.name=="character")) {
            if (hitcount <= maxhitcount)
            {
                switch (other.tag)
                {
                    case "playerbullet":
                        damageCount += other.GetComponent<BulletMovementNew>().damage;
                        break;
                    case "Player":
                        damageCount += other.GetComponent<PlayControl>().defence;
                        break;
                }
                hitcount++;
            }
            else {
                hitcount = 0;
                if (damageCount >= standdamagetolife)
                {
                    currentlife -= damageCount;
                }
               damageCount = 0;
            }
            if (currentlife<=turntobirdlife) {
                turntobird();
            }
            if (currentlife<=0) {
                die();
            }
        }
    }
}

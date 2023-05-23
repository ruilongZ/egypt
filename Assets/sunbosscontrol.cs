using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class sunbosscontrol : MonoBehaviour
{
    float skillpasstime;
    bool switchskilltofire;
    bool switchskilltoshield;
    bool switchskilltorest;
    bool firetoshield;

    [Header("机制相关")]
    float damageCount;
    public float standdamagetolife;
    int hitcount;
    public int maxhitcount;
    bool isbird;
    public float turntobirdlife;
    public float firetime;
    public float RestTime;
    public float shieldtime;



    [Space]
    [Header("基础参数")]
    public float currentlife;
    public float maxlife;
    Animator animator;

    [Space]
    [Header("护盾技能")]
    public GameObject shieldvfx;
    public GameObject bat;
    float spawnpasstime;
    public float spawnCD;


    [Space]
    [Header("爆炸技能")]
    float firepasstime;
    public float bulletCD;
    public GameObject firebullet;
    // Start is called before the first frame update
    void Start()
    {
        currentlife = maxlife;
        animator = GetComponent<Animator>();
        switchskilltofire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isbird)
        {
            if (switchskilltofire && !switchskilltoshield && !switchskilltorest)
            {
                //animator.SetTrigger("startskill1");
                fireball();
                if (skillpasstime < firetime)
                {
                    skillpasstime += Time.deltaTime;
                }
                else
                {
                    skillpasstime = 0;
                    switchskilltofire = false;
                    switchskilltorest = true;
                    switchskilltoshield = false;
                }
            }

            if (!switchskilltofire && !switchskilltoshield && switchskilltorest)
            {
                rest();
                if (skillpasstime < RestTime)
                {
                    skillpasstime += Time.deltaTime;
                }
                else
                {
                    skillpasstime = 0;
                    firetoshield = !firetoshield;
                    if (firetoshield)
                    {
                        switchskilltofire = false;
                        switchskilltorest = false;
                        switchskilltoshield = true;
                    }
                    else
                    {
                        switchskilltofire = true;
                        switchskilltorest = false;
                        switchskilltoshield = false;
                    }
                }
            }

            if (!switchskilltofire && switchskilltoshield && !switchskilltorest)
            {
               // animator.SetTrigger("startskill2");
                shield();
                if (skillpasstime < shieldtime)
                {
                    skillpasstime += Time.deltaTime;
                }
                else
                {
                    skillpasstime = 0;
                    switchskilltofire = false;
                    switchskilltorest = true;
                    switchskilltoshield = false;
                }
            }
        }
        else { 
        
        }
    }
    void rest() {
        shieldvfx.SetActive(false);
    }
    void fireball() {
        if (firepasstime < bulletCD)
        {
            firepasstime += Time.deltaTime;
        }
        else
        {
            Instantiate(firebullet, GameObject.Find("player").transform.position, Quaternion.identity);
            firepasstime = 0;
        }
    }

    Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(transform.position.x - 8, transform.position.x + 8), Random.Range(transform.position.y - 4, transform.position.y + 4), 0);
        return point;
    }
    void shield() {
        shieldvfx.SetActive(true);
        if (spawnpasstime < spawnCD)
        {
            spawnpasstime += Time.deltaTime;
        }
        else
        {
            Instantiate(bat, GetRandomPointInRoom(), Quaternion.identity);
            spawnpasstime = 0;
        }
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

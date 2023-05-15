using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotmonoster2p : MonoBehaviour
{
    [Header("基础参数")]
    GameObject monster1;
    float life;
    GameObject player;
    public GameObject rotmonsterp3;

    [Space]
    [Header("机制参数")]
    public int hitcount;
    public GameObject bullet;
    public GameObject bat;
    public float bulletTime;
    public float RestTime;
    public float spawnTime;
    public float SwitchSkillCD;

    [Space]
    [Header("发射子弹技能")]
    public float bulletCD;
    [Space]
    [Header("生成怪物技能")]
    public float spwanCD;

    float passtime;
    int currenthit;
    float skillpasstime;
    bool switchskilltobullet;
    bool switchskilltospawn;
    bool switchskilltorest;
    bool bullettospawn;
    // Start is called before the first frame update
    void Start()
    {
        monster1 = GameObject.Find("RotMonsterP1");
        life = monster1.GetComponent<rotmonster1p>().life;
        player = GameObject.Find("player");
        switchskilltobullet = true;
        bullettospawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchskilltobullet && !switchskilltospawn && !switchskilltorest)
        {
            bulletattack();
            if (skillpasstime < bulletTime)
            {
                skillpasstime += Time.deltaTime;
            }
            else
            {
                skillpasstime = 0;
                switchskilltobullet = false;
                switchskilltorest = true;
                switchskilltospawn = false;
            }
        }

        if (!switchskilltobullet && !switchskilltospawn && switchskilltorest)
        {
            rest();
            if (skillpasstime < RestTime)
            {
                skillpasstime += Time.deltaTime;
            }
            else
            {
                skillpasstime = 0;
                bullettospawn = !bullettospawn;
                if (bullettospawn)
                {
                    switchskilltobullet = false;
                    switchskilltorest = false;
                    switchskilltospawn = true;
                }
                else
                {
                    switchskilltobullet = true;
                    switchskilltorest = false;
                    switchskilltospawn = false;
                }
            }
        }

        if (!switchskilltobullet && switchskilltospawn && !switchskilltorest)
        {
            spawnbat();
            if (skillpasstime < spawnTime)
            {
                skillpasstime += Time.deltaTime;
            }
            else
            {
                skillpasstime = 0;
                switchskilltobullet = false;
                switchskilltorest = true;
                switchskilltospawn = false;
            }
        }

    }
    void rest()
    {

    }

    void bulletattack()
    {
        if (passtime < bulletCD)
        {
            passtime += Time.deltaTime;
        }
        else
        {
                Instantiate(bullet, transform.position, Quaternion.identity);
            passtime = 0;
        }
    }
    void spawnbat()
    {
        if (passtime < spwanCD)
        {
            passtime += Time.deltaTime;
        }
        else
        {
            Instantiate(bat, GetRandomPointInRoom(), Quaternion.identity);
            passtime = 0;
        }

    }
    Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(transform.position.x - 8, transform.position.x + 8), Random.Range(transform.position.y - 4, transform.position.y + 4), 0);
        return point;
    }
    void die()
    {
        Instantiate(rotmonsterp3, transform.position - Vector3.right * Random.Range(1.5f, 3f) + Vector3.up * Random.Range(-2, 2), Quaternion.identity);
        Instantiate(rotmonsterp3, transform.position + Vector3.right * Random.Range(1.5f, 3f) + Vector3.up * Random.Range(-2, 2), Quaternion.identity);
        Destroy(gameObject, 2f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet")
        {
            life -= other.GetComponent<BulletMovementNew>().damage;
            currenthit++;
            if (currenthit == hitcount)
            {
                die();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotmonster3pright : MonoBehaviour
{
    Animator animator;

    [Header("基础参数")]
    GameObject monster2;
    float life;
    GameObject player;

    [Space]
    [Header("机制参数")]
    public GameObject bullet;
    public float bulletTime;
    public float RestTime;
    public float spawnDurationTime;

    [Space]
    [Header("发射子弹技能")]
    public float bulletCD;
    [Space]
    [Header("冲撞技能")]
    float sprintspeed;
    public float maxsprintspeed;
    public float speeddamping;
    public int sprintTime;
    int currentsprintTime;

    float passtime;
    float skillpasstime;
    bool switchskilltobullet;
    bool switchskilltospawn;
    bool switchskilltorest;
    bool bullettospawn;
    bool isdead;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        monster2 = GameObject.Find("RotMonsterP2Right(Clone)");
        life = monster2.GetComponent<Rotmonoster2p>().life;
        player = GameObject.Find("player");
        bullettospawn = false;
        StartCoroutine("settrue");
    }
    IEnumerator settrue() {
        yield return new WaitForSeconds(2);
        switchskilltobullet = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (switchskilltobullet && !switchskilltospawn && !switchskilltorest)
        {
            animator.SetTrigger("skill1start");
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
            animator.SetTrigger("skill2start");
            sprinttoplayer();
            if (skillpasstime < spawnDurationTime)
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
        animator.SetTrigger("skill1end");
        animator.SetTrigger("skill2end");
        currentsprintTime = 0;
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
    void sprinttoplayer()
    {
        if (currentsprintTime < sprintTime)
        {

            if (sprintspeed < Random.Range(0.08f, 0.2f))
            {
                currentsprintTime++;
                sprintspeed =Random.Range( maxsprintspeed-2, maxsprintspeed );
            }
            else
            {
                if (!isdead) {
                    transform.Translate((player.transform.position - transform.position).normalized * sprintspeed * Time.deltaTime);
                    sprintspeed = Mathf.Lerp(sprintspeed, 0, Time.deltaTime * speeddamping);
                }
            }
        }
    }

    void die()
    {
        Destroy(gameObject, 1.5f);
        isdead = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet")
        {
            life -= other.GetComponent<BulletMovementNew>().damage;
            if (life <= 0)
            {
                die();
                animator.SetTrigger("die");
            }
        }
    }
}

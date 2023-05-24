using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class DarkBossState1 : MonoBehaviour
{
    [Header("�������")]
    [Header("�������")]
    public float CDTime;
    float cdTimer;
    bool isCD = true;
    [Header("����ǰҡ")]
    public float TimeBefore;
    float beforeTimer;
    [Header("�������ʱ��")]
    public float TimeAfter;
    float afterTimer;
    [Header("�˺�")]
    public float CircleDamage;
    public float BulletDamage;
    [Header("�˺�ת������")]
    public float HealRate;
    [Header("���ٱ���")]
    public float SlowDownRate;

    Vector3 playerPos = Vector3.zero;



    [Space]
    [Header("��������")]
    public float currentLife;
    public float maxlife;


    Animator animator;
    GameObject player;
    Collider collider;
    bool die = false;

    [Header("�ز�")]
    public GameObject MagicCircle;
    GameObject currentMagicCircle;
    public GameObject Warning;
    GameObject warning;

    public enum Skill
    {
        MagicCircleSkill,
        DoppeLgangerSkill,
    };
    public Skill currentSkill = Skill.MagicCircleSkill;
    Skill lastSkill;

    void Start()
    {
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if(cdTimer>=CDTime&&!die)
        {
            isCD = false;
            cdTimer = 0f;
            switch (currentSkill)
            {
                case Skill.MagicCircleSkill:
                    StartCoroutine(SpawnMagicCircle());
                    lastSkill = Skill.MagicCircleSkill;
                    return;
                case Skill.DoppeLgangerSkill:
                    lastSkill = Skill.DoppeLgangerSkill;
                    return;

            }
        }
        //CD Time Counter
        if(isCD)
        {
            cdTimer += Time.fixedDeltaTime;
        }
        //When The Dead Animation is Finished, Destroy The Boss
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            if (currentLife <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }


    private IEnumerator SpawnMagicCircle()
    {
        playerPos = player.transform.position;
        warning = Instantiate(Warning, playerPos, player.transform.rotation);
        warning.GetComponent<destoryself>().destorytime = TimeBefore - 0.5f;
        yield return new WaitForSeconds(TimeBefore);
        Destroy(warning);
        currentMagicCircle=Instantiate(MagicCircle, playerPos+ GetRandomPointAroundPlayer(), player.transform.rotation);
        MagicCircle.GetComponent<MagicalCircleComponent>().DestroyTime = TimeAfter;
        isCD = true;
    }

    Vector3 GetRandomPointAroundPlayer()
    {
        Vector3 point = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        return point;
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
                    currentLife -= other.GetComponent<PlayControl>().defence;
                    break;
            }
            if (currentLife <= 0)
            {
                currentLife = 0;
                animator.Play("Dark_Out");
                die = true;
                if (currentMagicCircle)
                {
                    Destroy(currentMagicCircle);
                }
                collider.enabled = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class DarkBossState1 : MonoBehaviour
{
    [Header("机制相关")]
    [Header("机制相关")]
    public float CDTime;
    float cdTimer;
    bool isCD = true;
    [Header("技能前摇")]
    public float TimeBefore;
    float beforeTimer;
    [Header("法阵存在时间")]
    public float TimeAfter;
    float afterTimer;
    [Header("伤害")]
    public float CircleDamage;
    public float BulletDamage;
    [Header("伤害转化比例")]
    public float HealRate;
    [Header("减速比例")]
    public float SlowDownRate;

    Vector3 playerPos = Vector3.zero;



    [Space]
    [Header("基础参数")]
    public float currentLife;
    public float maxlife;


    Animator animator;
    GameObject player;
    Collider collider;
    bool die = false;

    [Header("素材")]
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

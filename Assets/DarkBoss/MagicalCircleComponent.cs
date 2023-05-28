using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DarkBossState1;

public class MagicalCircleComponent : MonoBehaviour
{
    public float DamagePerSec;
    public float DestroyTime;
    float destroyTimer = 0f;
    [Header("伤害转化比例")]
    public float HealRate;
    [Header("减速比例")]
    public float SlowDownRate;
    [Header("扣血时间")]
    public float DamageTime;
    float damageTimer = 0f;
    Animator animator;
    GameObject player;
    DarkBossState1 darkGod;
    public bool isHitting = false;
    float playerSpeed;
    float newPlayerSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        darkGod = GameObject.FindObjectOfType<DarkBossState1>();
        animator = GetComponent<Animator>();
        playerSpeed = player.GetComponent<PlayerMovementNew>().MaxSpeedMultiplier;
        newPlayerSpeed =playerSpeed*SlowDownRate;
    }

    private void FixedUpdate()
    {
        //Magic Circle Destroy
        if (destroyTimer>=DestroyTime)
        {
            isHitting = false;
            darkGod.IsCD = true;
            darkGod.currentSkill = Skill.DoppeLgangerSkill;
            Destroy(this.gameObject);
        }
        else
        {
            destroyTimer += Time.fixedDeltaTime;
        }
        //When The Player is in the Magic Circle.
        if (isHitting)
        {
            if(!player.GetComponentInChildren<PlayControl>().ShiftPressed)
            {
                player.GetComponent<PlayerMovementNew>().MaxSpeedMultiplier = newPlayerSpeed;
            }
            damageTimer += Time.fixedDeltaTime;
            if(damageTimer>=DamageTime)
            {
                damageTimer = 0f;
                //Player Get Damaged
                player.GetComponentInChildren<PlayControl>().currentlife -= DamagePerSec;
                //Boss Get Health
                if (darkGod.currentLife+HealRate * DamagePerSec<darkGod.maxlife)
                {
                    darkGod.currentLife += HealRate * DamagePerSec;
                }
                else
                {
                    darkGod.currentLife = darkGod.maxlife;
                }
            }
        }
        else
        {
            player.GetComponent<PlayerMovementNew>().MaxSpeedMultiplier = playerSpeed;
        }
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" )
        {
            isHitting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isHitting = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovementNew : MonoBehaviour
{
    [Header("基础属性")]
    public float speed;
    public Vector3 movedir;
    public bool IsCloseCombat;
    public bool die;

    [Header("远程攻击怪物")]
    public float DistanceToAttack;
    public float currentspeed ;
    public float AttackCD;
    private float attackpasstime;
    public GameObject BatBullet;

    private CharacterController EnemyController;
    public float distanceToPlayer;
    GameObject player;
    private float passtime;
    public bool attacted=false;


    // Start is called before the first frame update
    void Start()
    {
        EnemyController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        movedir = (player.transform.position - transform.position).normalized;

        if (IsCloseCombat)
        {
       currentspeed = speed;
        }
        else {
            if (distanceToPlayer < DistanceToAttack)
            {
                currentspeed = 0;
                WaitToAttack();

            }
            else {
                currentspeed = speed;
            }
        }
        if (attacted) {
            Attacked();
        }
        if (die) {
            Die();
        }
        attackpasstime += Time.deltaTime;
        EnemyController.Move(movedir * currentspeed * Time.deltaTime);
    }

    public void Attacked() {
        if (passtime < 0.2)
        {
            passtime += Time.deltaTime;
            currentspeed = 0;
        }
        else {
            currentspeed = speed;
            passtime = 0;
            attacted = false;
        }
    }
    public void Die() {
        currentspeed = 0;
    }
    void WaitToAttack() {
        if (attackpasstime > AttackCD)
        {
            attackpasstime = 0;
            GetComponentInChildren<BatAnimationControl>().Attack();
            Instantiate(BatBullet,transform.position,Quaternion.identity);
            StartCoroutine("InstanceBullet");
        }
    }
    IEnumerator InstanceBullet() {
        Instantiate(BatBullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(BatBullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(BatBullet, transform.position, Quaternion.identity);
    }
}

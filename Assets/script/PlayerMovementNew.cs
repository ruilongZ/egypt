using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    [Header("»ù´¡ÒÆ¶¯")]
    public float TimeToReachMaxSpeed;
    public float MaxSpeedMultiplier;
    public float AccelerationMultiplier;
    public Vector3 dir;
    [Space]
    [Header("Ä¦²ÁÁ¦¿ØÖÆ")]
    public float Friction=1;
    [Space]
    [Header("³å´ÌÉÁ±Ü")]
    public float SprintSpeed;
    public float SprintTime;

    public bool havesprintdamageequip;
    public bool ShiftPressed = false;
    [Space]
    [Header("¹¥»÷¿ØÖÆ")]
    public  GameObject rightbullet;
    public GameObject upbullet;
    public GameObject downbullet;
    public GameObject leftbullet;
    public Transform FirePosition;
    public float FiringRate;
    private  Vector3 FireInputDir;
    public float damage;
    public float BulletRange;
    public float BulletSpeed;
    public float BulletOffset;
    [Space]
    [Header("ÊÜ»÷¿ØÖÆ")]
    private float moveuptime;
    private float movedowntime;
    private float movelefttime;
    private float moverighttime;

    private float passtime = 0;
    private bool CanFire ;
    Animator animator;

    private CharacterController control;
    void Start()
    {
        control = GetComponent<CharacterController>();
        CanFire = true;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicMove();
        Sprint();
        if (!ShiftPressed) { Attack(); }
        
    }
    public void BasicMove() {
        if (Input.GetKey("w")) {
            moveuptime += Time.deltaTime*Friction; 
            if (moveuptime > TimeToReachMaxSpeed)
            {
                moveuptime = TimeToReachMaxSpeed;
            }
        }
        else { 
            moveuptime -= Time.deltaTime * Friction;
            if (moveuptime < 0) {
                moveuptime = 0;
            }
        }

        if (Input.GetKey("s"))
        {
            movedowntime += Time.deltaTime * Friction;
            if (movedowntime > TimeToReachMaxSpeed)
            {
                movedowntime = TimeToReachMaxSpeed;
            }
        }
        else
        {
            movedowntime -= Time.deltaTime * Friction;
            if (movedowntime < 0)
            {
                movedowntime = 0;
            }
        }

        if (Input.GetKey("a"))
        {
            movelefttime += Time.deltaTime * Friction;
            if (movelefttime > TimeToReachMaxSpeed)
            {
                movelefttime = TimeToReachMaxSpeed;
            }
        }
        else
        {
            movelefttime -= Time.deltaTime * Friction;
            if (movelefttime < 0)
            {
                movelefttime = 0;
            }
        }

        if (Input.GetKey("d"))
        {
            moverighttime += Time.deltaTime * Friction;
            if (moverighttime > TimeToReachMaxSpeed)
            {
                moverighttime = TimeToReachMaxSpeed;
            }
        }
        else
        {
            moverighttime -= Time.deltaTime * Friction;
            if (moverighttime < 0)
            {
                moverighttime = 0;
            }
        }
        dir = new Vector3((moverighttime-movelefttime) * AccelerationMultiplier, (moveuptime - movedowntime) * AccelerationMultiplier, 0);
        control.Move(dir * MaxSpeedMultiplier * Time.deltaTime);
    }
    public void Sprint() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            ShiftPressed = true;
            if (passtime < SprintTime && dir != Vector3.zero) {
                    animator.SetTrigger("sprint");
            }
        }
        if (ShiftPressed) {
            if (passtime < SprintTime)
            {
                control.Move(dir.normalized * SprintSpeed * Time.deltaTime);
                passtime+=Time.deltaTime;
            }
            else {
                passtime = 0;
                ShiftPressed = false;
            }
        }
    }
    public void Attack() {
        if (Input.GetKeyDown(KeyCode.UpArrow)&&CanFire) {
            Instantiate(upbullet , FirePosition.position, Quaternion.Euler(0,0,90-dir.x* BulletOffset));
            FireInputDir = new Vector3(0,1,0);
            CanFire = false;
            StartCoroutine("CanAttack");

            AttackAnimationControl(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && CanFire)
        {
            Instantiate(downbullet, FirePosition.position, Quaternion.Euler(0, 0, -90+dir.x* BulletOffset));
            FireInputDir = new Vector3(0, -1, 0);
            CanFire = false;
            StartCoroutine("CanAttack");

            AttackAnimationControl(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && CanFire)
        {
            Instantiate(rightbullet, FirePosition.position, Quaternion.Euler(0, 0, 0+dir.y* BulletOffset));
            FireInputDir = new Vector3(1, 0, 0);
            CanFire = false;
            StartCoroutine("CanAttack");

            AttackAnimationControl(0,1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanFire)
        {
            Instantiate(leftbullet, FirePosition.position, Quaternion.Euler(0, 0, 180-dir.y* BulletOffset));
            FireInputDir = new Vector3(-1, 0, 0);
            CanFire = false;
            StartCoroutine("CanAttack");

            AttackAnimationControl(0, -1);
        }
    }
    IEnumerator CanAttack() {
        yield return new WaitForSeconds(FiringRate);
        CanFire = true;
    }
    void AttackAnimationControl(int j,int k) {
        animator.SetTrigger("attack");
        animator.SetFloat("attacky", j);
        animator.SetFloat("attackx", k);
    }
}

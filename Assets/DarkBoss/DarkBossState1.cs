using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DarkBossState1 : MonoBehaviour
{
    public Animator dialogue;
    bool dialogueed;
    [Header("机制相关")]
    public float CDTime;
    float cdTimer;
    public bool IsCD = true;
    [Header("技能前摇")]
    public float TimeBefore;
    float beforeTimer;
    [Header("法阵存在时间")]
    public float TimeAfter;
    Vector3 playerPos = Vector3.zero;
    [Space]
    [Header("基础参数")]
    public float currentLife;
    public float maxlife;
    Animator animator;
    GameObject player;
    Collider collider;
    bool die = false;
    [Space]
    [Header("ui相关")]
    public Text bloodcounttext;
    public Slider bloodslider;

    [Header("素材")]
    public GameObject MagicCircle;
    GameObject currentMagicCircle;
    public GameObject Warning;
    GameObject warning;
    public GameObject Doppelgang;
    public int DoppeCount;
    int doppeCount;
    public float DoppeIntervalTime;
    float doppeTimer = 0f;
    public List<DoppelgangerComponent> doppes = new List<DoppelgangerComponent>();

    public GameObject shield;
    public GameObject bossbull;
    public enum Skill
    {
        MagicCircleSkill,
        DoppeLgangerSkill,
    };
    public Skill currentSkill = Skill.MagicCircleSkill;
    Skill lastSkill;

    void Start()
    {
        currentLife = maxlife;
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        doppeCount = DoppeCount;
        setui();
    }

    void FixedUpdate()
    {
        if (dialogueed)
        {
            if (cdTimer >= CDTime && !die)
            {
                IsCD = false;
                cdTimer = 0f;
                switch (currentSkill)
                {
                    case Skill.MagicCircleSkill:

                        StartCoroutine(SpawnMagicCircle());
                        lastSkill = Skill.MagicCircleSkill;
                        return;
                    case Skill.DoppeLgangerSkill:
                        shield.SetActive(true);
                        lastSkill = Skill.DoppeLgangerSkill;
                        doppeCount = 0;
                        return;
                }
            }

            //CD Time Counter
            if (IsCD)
            {
                cdTimer += Time.fixedDeltaTime;
            }
            //When The Dead Animation is Finished, Destroy The Boss
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                if (currentLife <= 0)
                {
                    Destroy(gameObject, 4f);
                    transform.GetChild(1).gameObject.SetActive(false);
                    // Destroy(transform.parent.gameObject, 4.1f);
                }
            }

            //Skill 2
            SpawnDoope();
        }
    }

    private IEnumerator SpawnMagicCircle()
    {
        shield.SetActive(false);
        playerPos = player.transform.position;
        warning = Instantiate(Warning, playerPos, player.transform.rotation);
        warning.GetComponent<destoryself>().destorytime = TimeBefore - 0.5f;
        yield return new WaitForSeconds(TimeBefore);
        Destroy(warning);
        currentMagicCircle=Instantiate(MagicCircle, playerPos+ GetRandomPointAroundPlayer(), player.transform.rotation);
        MagicCircle.GetComponent<MagicalCircleComponent>().DestroyTime = TimeAfter;
    }

    void SpawnDoope()
    {
        //Spawn Doppelgangs at regular intervals.
        if (doppeCount < DoppeCount)
        {
            if (doppeTimer >= DoppeIntervalTime)
            {
                var doppe = Instantiate(Doppelgang, GetRandomPointInRoom()+new Vector3(0,0.8f,0), player.transform.rotation).GetComponent<DoppelgangerComponent>();
                doppes.Add(doppe);
                doppeCount++;
                doppeTimer = 0f;
            }
            else
            {
                doppeTimer += Time.fixedDeltaTime;
            }
        }
        //When all the doppelgangs died, Destroy them and Change Skill.
        if (doppeCount == DoppeCount && doppes.Count != 0 && doppes.All(dope => dope.die == true))
        {
            foreach (var dope in doppes)
            {
                Destroy(dope.gameObject,2.5f);
            }
            doppes.Clear();
            shield.SetActive(false);
            IsCD = true;
            currentSkill = Skill.MagicCircleSkill;
        }
    }

    Vector3 GetRandomPointAroundPlayer()
    {
        Vector3 point = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        return point;
    }
    Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(transform.position.x - 8, transform.position.x + 8), Random.Range(transform.position.y - 3, transform.position.y + 3), 0);
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
                    if (other.GetComponent<PlayControl>().sprintdamageequip && other.GetComponent<PlayControl>().ShiftPressed)
                    {
                        currentLife -= other.GetComponent<PlayControl>().defence;
                    }
                    break;
            }
            setui();
            if (currentLife <= 0)
            {

                foreach (var dope in doppes)
                {
                    Destroy(dope.gameObject,4f);
                }
                currentLife = 0;
                animator.SetTrigger("die");
                die = true;
                if (currentMagicCircle)
                {
                    Destroy(currentMagicCircle);
                }
                collider.enabled = false;
                StartCoroutine(instance());
            }
        }
    }
    IEnumerator instance() {
        yield return new WaitForSeconds(4f);
        Instantiate(bossbull, transform.position, Quaternion.identity);
    }

    void setui()
    {
        bloodslider.value = currentLife / maxlife;
        bloodcounttext.text = currentLife.ToString();
    }

    public void setbossdialogue()
    {
        dialogue.SetBool("enterroom", true);
        dialogueed = true;
    }
}

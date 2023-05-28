using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossbullcontrol : MonoBehaviour
{float passtime;
    float sprintpasstime;
    float stonepasstime;
    GameObject player;
    Animator animator;
    [Header("基础属")]
    public float maxlife;
    public float currentlife;
    public int hitcount;
    float damageCount;
    public float standdamagetolife;
    [Space]
    [Header("技能属")]
    public float resttime;
    public float skilltime;
    public float sprintCD;
    public float stoneCD;
    public GameObject stone;
    float currentspeed;
    public float maxspeed;
    public float multi;
    bool skilling;
    bool resting;
    bool isdead;

    [Space]
    [Header("ui相关")]
    public Text hitcounttext;
    public Text bloodcounttext;
    public Slider bloodslider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(settrue());
        currentlife = maxlife;
        setui();
    }
    IEnumerator settrue() {
        yield return new WaitForSeconds(1.5f);
        skilling = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (skilling&&!resting) {
            switchtorest();
            animator.SetTrigger("skill");
            if (!isdead)
            {
                sprintandstone();
            }
        }
        if (!skilling && resting)
        {
            switchtoskill();
            animator.SetTrigger("skillend");
        }

    }
    void switchtorest() {
        if (passtime < skilltime)
        {
            passtime += Time.deltaTime;
        }
        else {
            passtime = 0;
             skilling=false;
            resting=true;
        }
    }
    void switchtoskill() {
        if (passtime < resttime)
        {
            passtime += Time.deltaTime;
        }
        else
        {
            passtime = 0;
            skilling = true;
            resting =false;
        }
    }
    void sprintandstone() {
        if (stonepasstime < stoneCD)
        {
            stonepasstime += Time.deltaTime;
        }
        else {
            stonepasstime = 0;
            Instantiate(stone,GetRandomPointInRoom(), Quaternion.identity);
        }
        if (sprintpasstime<sprintCD) {
            sprintpasstime += Time.deltaTime;
            currentspeed = Mathf.Lerp(currentspeed, 0, Time.deltaTime * multi);
        }
        else
        {
            sprintpasstime = 0;
            currentspeed = maxspeed;
        }
        transform.Translate((player.transform.position - transform.position) * currentspeed * Time.deltaTime);
    }
    Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(76.8f - 7,76.8f + 7), Random.Range(0 - 2.5f, 0 + 2.5f), 0);
        return point;
    }
    void die()
    {
        animator.SetTrigger("die");
        transform.parent.GetChild(1).gameObject.SetActive(false);
        Destroy(gameObject, 3.4f);
        Destroy(transform.parent.gameObject, 3.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet" || (other.tag == "Player" && other.name == "character"))
        {
            if (hitcount > 0)
            {
                switch (other.tag)
                {
                    case "playerbullet":
                        hitcount--;
                        damageCount += other.GetComponent<BulletMovementNew>().damage;
                        break;
                    case "Player":
                        if (other.GetComponent<PlayControl>().sprintdamageequip && other.GetComponent<PlayControl>().ShiftPressed)
                        {
                            hitcount--;
                            damageCount += other.GetComponent<PlayControl>().defence;
                        }
                        break;
                }
                if (hitcount == 0)
                {
                    if (damageCount >= standdamagetolife)
                    {
                        currentlife -= damageCount;
                    }
                    damageCount = 0;
                    hitcount = 5;
                }
                hitcounttext.text = hitcount.ToString();
            }
            setui();
            if (currentlife <= 0)
            {
                die();
                isdead = true;
            }
        }
    }
    void setui() {
        bloodslider.value = currentlife / maxlife;
        bloodcounttext.text = currentlife.ToString();
    }
}

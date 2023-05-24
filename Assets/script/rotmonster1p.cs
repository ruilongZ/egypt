using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class rotmonster1p : MonoBehaviour
{
    public Slider bloodslider;
    public Text bloodtext;
    Animator animator;
    Collider collider;

    [Header("基础参数")]
    public float maxlife;
   public   float currentlife;
    GameObject player;
    public GameObject rotmonsterleft;
    public GameObject rotmonsterright;


    [Space]
    [Header("机制参数")]
    public int hitcount;
    public GameObject rain;
    public GameObject spray;
    public float RainTime;
    public float RestTime;
    public float SprayTime;
    public float SwitchSkillCD;

    [Space]
    [Header("下雨技能")]
    public float raincd;
    [Space]
    [Header("喷射技能")]
    public float spraycd;

    float passtime;
    int currenthit;
    float skillpasstime;
    bool switchskilltorain;
    bool switchskilltospray;
    bool switchskilltorest;
    bool raintospray;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        currenthit = hitcount;
        currentlife = maxlife;
        setui();
        animator = GetComponent<Animator>();

        player = GameObject.Find("player");
        switchskilltorain = true;
        raintospray = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchskilltorain && !switchskilltospray && !switchskilltorest)
        {
            animator.SetTrigger("startskill1");
            skillrain();
            if (skillpasstime < RainTime)
            {
                skillpasstime += Time.deltaTime;
            }
            else
            {
                skillpasstime = 0;
                switchskilltorain = false;
                switchskilltorest = true;
                switchskilltospray = false;
            }
        }

        if (!switchskilltorain && !switchskilltospray && switchskilltorest)
        {
            rest();
            if (skillpasstime < RestTime)
            {
                skillpasstime += Time.deltaTime;
            }
            else
            {
                skillpasstime = 0;
                raintospray = !raintospray;
                if (raintospray)
                {
                    switchskilltorain = false;
                    switchskilltorest = false;
                    switchskilltospray = true;
                }
                else
                {
                    switchskilltorain = true;
                    switchskilltorest = false;
                    switchskilltospray = false;
                }
            }
        }

        if (!switchskilltorain && switchskilltospray && !switchskilltorest)
        {
            animator.SetTrigger("startskill2");
            skillspray();
            if (skillpasstime < SprayTime)
            {
                skillpasstime += Time.deltaTime;
            }
            else
            {
                skillpasstime = 0;
                switchskilltorain = false;
                switchskilltorest = true;
                switchskilltospray = false;
            }
        }

    }
    void rest()
    {
        animator.SetTrigger("endskill2");
        animator.SetTrigger("endskill1");
    }

    void skillrain()
    {
        if (passtime < raincd)
        {
            passtime += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(rain, player.transform.position + GetRandomPointAroundPlayer(), Quaternion.identity);
            }
            passtime = 0;
        }
    }
    void skillspray()
    {
        if (passtime < spraycd)
        {
            passtime += Time.deltaTime;
        }
        else
        {
            Instantiate(spray, GetRandomPointInRoom(), Quaternion.identity);
            passtime = 0;
        }

    }
    Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(transform.position.x - 8, transform.position.x + 8), Random.Range(transform.position.y - 4, transform.position.y + 4), 0);
        return point;
    }
    Vector3 GetRandomPointAroundPlayer()
    {
        Vector3 point = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        return point;
    }
    void dieandspawn()
    {
        StartCoroutine("leftspawnmonster");
        StartCoroutine("spawnmonster");
        Destroy(gameObject, 3.1f);
    }
        IEnumerator spawnmonster() {
        yield return new WaitForSeconds(2);
        Instantiate(rotmonsterright, transform.position + Vector3.right * Random.Range(1.5f, 3f) + Vector3.up * Random.Range(-2, 2), Quaternion.identity);
    }
    IEnumerator leftspawnmonster()
    {
        yield return new WaitForSeconds(3);
        Instantiate(rotmonsterleft, transform.position - Vector3.right * Random.Range(1.5f, 3f) + Vector3.up * Random.Range(-2, 2), Quaternion.identity);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet"|| (other.tag == "Player"&&other.name=="character"))
        {
            switch (other.tag) {
                case "playerbullet":
                    currentlife -= other.GetComponent<BulletMovementNew>().damage;
                    break;
                case "Player":
                    if (other.GetComponent<PlayControl>().sprintdamageequip && other.GetComponent<PlayControl>().ShiftPressed)
                    {
                        currentlife -= other.GetComponent<PlayControl>().defence;
                    }
                    break;
            }

            currenthit--;
            if (currentlife<=0) {
                currentlife = 0;
                animator.SetTrigger("die");
                collider.enabled = false;
                Destroy(gameObject, 2f);
            }
            setui();
            if (currenthit == 0)
            {
                animator.SetTrigger("die");
                collider.enabled = false;
                dieandspawn();
            }
        }
    }
    void setui() {
        bloodtext.text = currentlife.ToString();
        bloodslider.value = currentlife / maxlife;
    }
}

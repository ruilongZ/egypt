using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class sunbosscontrol : MonoBehaviour
{
    float skillpasstime;
    bool switchskilltofire;
    bool switchskilltoshield;
    bool switchskilltorest;
    bool firetoshield;

    float birdskillpasstime;
    bool switchskilltobullet;
    bool switchskilltoflash;
    bool birdswitchskilltorest;
    bool bullettoflash;
    [SerializeField]
    [Header("ui相关")]
    public Slider bloodslider;
    public Text bloodtext;
    public Text hitcounttext;
    [SerializeField]
    [Header("机制相关")]
    float damageCount;
    public float standdamagetolife;
    int hitcount;
    public int maxhitcount;
    bool isbird;
    public float turntobirdlife;
    public float firetime;
    public float RestTime;
    public float shieldtime;
    [SerializeField]
    [Space]
    [Header("基础参数")]
    public float currentlife;
    public float maxlife;
    Animator animator;
    [SerializeField]
    [Space]
    [Header("护盾技能")]
    public GameObject shieldvfx;
    public GameObject bat;
    float spawnpasstime;
    public float spawnCD;
    [SerializeField]
    [Space]
    [Header("爆炸技能")]
    float firepasstime;
    public float bulletCD;
    public GameObject firebullet;
    [SerializeField]
    [Space]
    [Header("鸟子弹技能")]
    public GameObject birdbigbullet;
    float bulletpasstime;
    public float birdbulletCD;
    [SerializeField]
    [Space]
    [Header("鸟闪电技能")]
    public GameObject birdflash;
    public float flashCD;
    float flashpasstime;
    // Start is called before the first frame update
    void Start()
    {
        currentlife = maxlife;
        animator = GetComponent<Animator>();
        switchskilltofire = true;
        switchskilltobullet = true;
        setui();
        hitcount = maxhitcount;
        hitcounttext.text = hitcount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isbird)
        {
            if (switchskilltofire && !switchskilltoshield && !switchskilltorest)
            {
                fireball();
                if (skillpasstime < firetime)
                {
                    skillpasstime += Time.deltaTime;
                }
                else
                {
                    skillpasstime = 0;
                    switchskilltofire = false;
                    switchskilltorest = true;
                    switchskilltoshield = false;
                }
            }

            if (!switchskilltofire && !switchskilltoshield && switchskilltorest)
            {
                rest();
                if (skillpasstime < RestTime)
                {
                    skillpasstime += Time.deltaTime;
                }
                else
                {
                    skillpasstime = 0;
                    firetoshield = !firetoshield;
                    if (firetoshield)
                    {
                        switchskilltofire = false;
                        switchskilltorest = false;
                        switchskilltoshield = true;
                    }
                    else
                    {
                        switchskilltofire = true;
                        switchskilltorest = false;
                        switchskilltoshield = false;
                    }
                }
            }

            if (!switchskilltofire && switchskilltoshield && !switchskilltorest)
            {
                shield();
                if (skillpasstime < shieldtime)
                {
                    skillpasstime += Time.deltaTime;
                }
                else
                {
                    skillpasstime = 0;
                    switchskilltofire = false;
                    switchskilltorest = true;
                    switchskilltoshield = false;
                }
            }
        }
        else {
            if (switchskilltobullet && !switchskilltoflash && !birdswitchskilltorest)
            {
                birdbullet();
                if (birdskillpasstime < firetime)
                {
                    birdskillpasstime += Time.deltaTime;
                }
                else
                {
                    birdskillpasstime = 0;
                    switchskilltobullet = false;
                    birdswitchskilltorest = true;
                    switchskilltoflash = false;
                }
            }

            if (!switchskilltobullet && !switchskilltoflash && birdswitchskilltorest)
            {
                rest();
                if (birdskillpasstime < RestTime)
                {
                    birdskillpasstime += Time.deltaTime;
                }
                else
                {
                    birdskillpasstime = 0;
                    bullettoflash = !bullettoflash;
                    if (bullettoflash)
                    {
                        switchskilltobullet = false;
                        birdswitchskilltorest = false;
                        switchskilltoflash = true;
                    }
                    else
                    {
                        switchskilltobullet = true;
                        birdswitchskilltorest = false;
                        switchskilltoflash = false;
                    }
                }
            }

            if (!switchskilltobullet && switchskilltoflash && !birdswitchskilltorest)
            {
                flash();
                if (birdskillpasstime < shieldtime)
                {
                    birdskillpasstime += Time.deltaTime;
                }
                else
                {
                    birdskillpasstime = 0;
                    switchskilltobullet = false;
                    birdswitchskilltorest = true;
                    switchskilltoflash = false;
                }
            }
        }
    }
    void rest()
    {
        animator.SetBool("attack", false);
        shieldvfx.SetActive(false);
    }
    void fireball() {
        if (firepasstime < bulletCD)
        {
            firepasstime += Time.deltaTime;
        }
        else
        {
            Instantiate(firebullet, GameObject.Find("player").transform.position, Quaternion.identity);
            firepasstime = 0;
        }
    }

    Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(transform.position.x - 8, transform.position.x + 8), Random.Range(transform.position.y - 3.5f, transform.position.y + 3.5f), 0);
        return point;
    }
    void shield() {
        shieldvfx.SetActive(true);
        if (spawnpasstime < spawnCD)
        {
            spawnpasstime += Time.deltaTime;
        }
        else
        {
            Instantiate(bat, GetRandomPointInRoom(), Quaternion.identity);
            spawnpasstime = 0;
        }
    }
    void turntobird() {
        isbird = true;
        animator.SetTrigger("turn");
    }

    void birdbullet() {
        animator.SetBool("attack", true);
        if (bulletpasstime < birdbulletCD)
        {
            bulletpasstime += Time.deltaTime;
        }
        else
        {
            Instantiate(birdbigbullet, GetRandomPointInRoom() + new Vector3(0, 2, 0), Quaternion.identity);
            bulletpasstime = 0;
        }
    }

    void flash()
    {
        animator.SetBool("attack",true);
        if (flashpasstime < flashCD)
        {
            flashpasstime += Time.deltaTime;
        }
        else
        {
            Instantiate(birdflash, GetRandomPointInRoom(), Quaternion.identity);
            flashpasstime = 0;
        }
    }
    void die() {
        animator.SetTrigger("die");
        transform.parent.GetChild(1).gameObject.SetActive(false);
        Destroy(transform.parent.gameObject,1.5f);
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="playerbullet"||(other.tag=="Player"&&other.name=="character")) {
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
                if (hitcount==0) {
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
            if (currentlife<=turntobirdlife) {
                turntobird();
            }
            if (currentlife<=0) {
                die();
            }
        }
    }
    public void setui() {
        if (currentlife>maxlife) {
            currentlife = maxlife;
        }
        bloodslider.value = currentlife / maxlife;
        bloodtext.text = currentlife.ToString();
        
    }
}

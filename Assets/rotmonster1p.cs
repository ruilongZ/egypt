using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rotmonster1p : MonoBehaviour
{
    [Header("基础参数")]
    public float life;
    GameObject player;
    public GameObject rotmonsterp2;

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
        player = GameObject.Find("player");
        switchskilltorain = true;
        raintospray =false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchskilltorain && !switchskilltospray && !switchskilltorest)
        {
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
                else {
                    switchskilltorain = true;
                    switchskilltorest = false;
                    switchskilltospray = false;
                }
            }
        }

        if (!switchskilltorain && switchskilltospray && !switchskilltorest)
        {
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

    }

    void skillrain()
    {
        if (passtime < raincd)
        {
            passtime+=Time.deltaTime;
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
                passtime+=Time.deltaTime;
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
        Vector3 point = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range( -1.5f,1.5f), 0);
        return point;
    }
    void die()
        {
        Instantiate(rotmonsterp2,transform.position-Vector3.right*Random.Range(1.5f,3f)+Vector3.up*Random.Range(-2,2),Quaternion.identity);
        Instantiate(rotmonsterp2, transform.position + Vector3.right * Random.Range(1.5f, 3f) + Vector3.up * Random.Range(-2, 2), Quaternion.identity);
        Destroy(gameObject,2f);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "playerbullet")
            {
            life -= other.GetComponent<BulletMovementNew>().damage;
                currenthit++;
                if (currenthit == hitcount)
                {
                    die();
                }
            }
        }
}

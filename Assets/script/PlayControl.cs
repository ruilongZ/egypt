using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{
    [Header("玩家基础")]
    public float currentlife;
    public float maxlife;
    public float defence;

    public bool ShiftPressed;
  public  bool issprintanddamage;
    float passtime;
    float SprintTime = 0.4f;
    public bool sprintdamageequip;

    public bool bloodtodamageequip;
    public float damageadd = 10;
    public float damageaddtime = 5;
    [Space]
    [Header("UI")]
    [SerializeField]
    public Slider bloodbar;
    public Slider defencebar;
    public Text defenceNum;
    public GameObject gameoverui;

    Animator playerAnimator;
    bool die;

    [Space]
    [Header("经济系统")]
    public int coin;
    void Start()
    {
        currentlife = maxlife;
        playerAnimator = GetComponent<Animator>();
        setbloodbar();
        setdefencebar();
        setdefenceNum();
    }
    private void Update()
    {
        if (die) {
            dietodestory();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShiftPressed = true;
        }
        if (ShiftPressed)
        {
            if (passtime < SprintTime)
            {
                passtime += Time.deltaTime;
            }
            else
            {
                passtime = 0;
                ShiftPressed = false;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collection") {
            coin++;
        }
        if (sprintdamageequip && ShiftPressed)
        {
            return;
        }
        else {
            if (other.tag == "enemybullet" || other.tag == "Enemy" || other.tag == "boss" || other.tag == "bossbullet"|| other.tag == "track"|| other.tag == "damageallbullet")
            {
                playerAnimator.SetTrigger("attacked");
                switch (other.tag)
                {
                    case "enemybullet":
                        TakeDamage(other.GetComponent<Batbulletcontrol>().damage);
                        break;
                    case "Enemy":
                        TakeDamage(5);
                        break;
                    case "boss":
                        TakeDamage(8);
                        break;
                    case "bossbullet":
                        TakeDamage(10);
                        break;
                    case "track":
                        TakeDamage(10);
                        break;
                    case "damageallbullet":
                        TakeDamage(10);
                        break;
                }
                if (currentlife <= 0)
                {
                    gameoverui.SetActive(true);
                    die = true;
                    playerAnimator.SetTrigger("die");
                    gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    GetComponentInParent<PlayerMovementNew>().MaxSpeedMultiplier = 0;
                    GetComponentInParent<PlayerMovementNew>().SprintSpeed = 0;
                    Invoke("dieanddestory", 2f);
                }
            }
        }

    }
    IEnumerator backdamage() {
        yield return new WaitForSeconds(damageaddtime);
        GetComponentInParent<PlayerMovementNew>().damage -= damageadd;
    }
    void TakeDamage(float damage) {
        if (defence <= 0)
        {
            currentlife -= damage;
            if (bloodtodamageequip)
            {
                GetComponentInParent<PlayerMovementNew>().damage += damageadd;
                StartCoroutine("backdamage");
            }
            setbloodbar();
        }
        else {
            defence -= damage;
            if (defence < 0) {
                defence = 0;
            }
            setdefenceNum();
            setdefencebar();
        }
    }
    void dieanddestory() {
        Destroy(GetComponentInParent<PlayerMovementNew>().gameObject);
    }
    void dietodestory() {
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        Destroy(GameObject.FindGameObjectWithTag("enemybullet"));
        Destroy(GameObject.FindGameObjectWithTag("god"));
        Destroy(GameObject.FindGameObjectWithTag("boss"));
    }
    public void setbloodbar()
    {
            bloodbar.value =currentlife / maxlife;
     }
    public void setdefencebar()
    {
        defencebar.value = defence / 30;
    }

    public void setdefenceNum()
    {
        defenceNum.text=defence.ToString();
    }
}

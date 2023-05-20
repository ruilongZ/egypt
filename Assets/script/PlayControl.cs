using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{
    [Header("玩家基础")]
    public float currentlife;
    public float maxlife;
    public float defence;
    [Space]
    [Header("UI")]
    [SerializeField]
    public Scrollbar bloodbar;
    public Scrollbar defencebar;
    public Text defenceNum;
    public Text coinNum;

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
        setcionNub();
        setdefenceNum();
    }
    private void Update()
    {
        if (die) {
            dieanddestory();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collection") {
            coin++;
            setcionNub();
        }
        if (other.tag == "enemybullet"|| other.tag == "Enemy"||other.tag=="boss"|| other.tag == "bossbullet")
        {
            playerAnimator.SetTrigger("attacked");
            switch (other.tag){
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
            }
            if (currentlife <= 0)
            {
                die = true;
                playerAnimator.SetTrigger("die");
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                GetComponentInParent<PlayerMovementNew>().MaxSpeedMultiplier = 0;
                GetComponentInParent<PlayerMovementNew>().SprintSpeed = 0;
                Invoke("Setactivefalse",2f);
            }
        }
    }
    void TakeDamage(float damage) {
        if (defence <= 0)
        {
            currentlife -= damage;
            setbloodbar();
        }
        else {
            defence -= damage;
            setdefenceNum();
            setdefencebar();
            if (defence < 0) {
                defence = 0;
            }
        }
    }
    void Setactivefalse() {
        Destroy(GetComponentInParent<PlayerMovementNew>().gameObject);
    }
    void dieanddestory() {
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        Destroy(GameObject.FindGameObjectWithTag("enemybullet"));
        Destroy(GameObject.FindGameObjectWithTag("god"));
        Destroy(GameObject.FindGameObjectWithTag("boss"));
    }
    public void setbloodbar()
    {
            bloodbar.size =currentlife / maxlife;
     }
    public void setdefencebar()
    {
        defencebar.size = defence / 30;
    }
    public void setcionNub()
    {
        coinNum.text = coin.ToString();
    }
    public void setdefenceNum()
    {
        defenceNum.text=defence.ToString();
    }
}

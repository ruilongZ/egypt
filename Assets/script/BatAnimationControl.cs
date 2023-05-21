using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimationControl : MonoBehaviour
{
    [System.Serializable]
    public struct RewardAndRate
    {
        public int rate;
        public GameObject reward;

        public RewardAndRate(int rate, GameObject reward)
        {
            this.rate = rate;
            this.reward = reward;
        }
    }

    [Header("生成奖励概率控制")]
    public RewardAndRate[] RewardingAndRate;


    [Header("蝙蝠基础")]
    public float life;

    Animator batAnimator;
    // Start is called before the first frame update
    void Start()
    {
        batAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        batAnimator.SetFloat("x", GetComponentInParent<EnemyMovementNew>().movedir.x * 1000);
        if (GetComponentInParent<EnemyMovementNew>().currentspeed == 0)
        {
            batAnimator.SetBool("move", false);
        }
        else {
            batAnimator.SetBool("move", true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet")
        {
            life -= other.GetComponentInParent<BulletMovementNew>().damage;
            GetComponentInParent<EnemyMovementNew>().attacted = true;
            batAnimator.SetTrigger("attacked");
        }
        if (other.name=="character")
        {
            if (other.GetComponent<PlayControl>().sprintdamageequip && other.GetComponent<PlayControl>().ShiftPressed) {
                life -= other.GetComponent<PlayControl>().defence;
                GetComponentInParent<EnemyMovementNew>().attacted = true;
                batAnimator.SetTrigger("attacked");
            }
        }
        if (life <= 0)
        {
            batAnimator.SetTrigger("die");
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GetComponentInParent<EnemyMovementNew>().die = true;
            Destroy(GetComponentInParent<EnemyMovementNew>().gameObject, 0.7f);

            RandomingRate();
        }
    }

    int sum = 0;
    void RandomingRate()
    {
        for (int i = 0; i < RewardingAndRate.Length; i++)
        {
            sum += RewardingAndRate[i].rate;
        }
        int k = Random.Range(0, sum);
        int j = RewardingAndRate[0].rate;
        for (int i = 0; i < RewardingAndRate.Length; i++)
        {
            if (k <= j)
            {
                Instantiate(RewardingAndRate[i].reward, transform.position, Quaternion.identity);
                break;
            }
            else
            {
                j += RewardingAndRate[i + 1].rate;
            }
        }
    }

    public void Attack() {
        batAnimator.SetTrigger("attack");
    }
}

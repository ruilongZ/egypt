using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneenemytriggrt : MonoBehaviour
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

    [SerializeField] float life;
    Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block")
        {
            gameObject.GetComponentInParent<stoneenemymove>().block();
        }
        if (other.tag == "playerbullet")
        {
            life -= other.GetComponentInParent<BulletMovementNew>().damage;
        }
        if (other.name == "character")
        {
            if (other.GetComponent<PlayControl>().sprintdamageequip && other.GetComponent<PlayControl>().ShiftPressed)
            {
                life -= other.GetComponent<PlayControl>().defence;
            }
        }
        if (life <= 0)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            GetComponentInParent<stoneenemymove>().die();
            Destroy(gameObject.GetComponentInParent<stoneenemymove>().gameObject, 2f);
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
        int j = 0;
        for (int i = 0; i < RewardingAndRate.Length; i++)
        {
            if (k <= j + RewardingAndRate[i].rate)
            {
                Instantiate(RewardingAndRate[i].reward, transform.position, Quaternion.identity);
                break;
            }
            else
            {
                j += RewardingAndRate[i].rate;
            }
        }
    }
}

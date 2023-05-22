using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class randomitem : MonoBehaviour
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

    void Start()
    {
        RandomingRate();
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
}

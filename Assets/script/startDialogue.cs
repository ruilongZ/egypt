
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDialogue : MonoBehaviour
{
    [System.Serializable]
    public struct RewardAndRate {
        public int rate;
        public GameObject reward;

        public RewardAndRate(int rate, GameObject reward) {
            this.rate = rate;
            this.reward = reward;
        }
    }

    [Header("生成奖励概率控制")]
    public RewardAndRate[] RewardingAndRate;

    Animator UIanimator;
    private bool firsttalk=false;
    GameObject playersave;

 void Start()
    {
        UIanimator = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Animator>();
        playersave = GameObject.FindGameObjectWithTag("playersave");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name=="player") {
            UIanimator.SetBool("talk",true);
            if (!firsttalk)
            {
                playersave.GetComponent<playersave>().AddNpcTime();
                UIanimator.gameObject.GetComponentInChildren<changeconversation>().ChangeConversation();
                UIanimator.gameObject.GetComponentInChildren<changeselection>().ChangeConversation();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!firsttalk) {
                RandomingRate();
            }
            firsttalk = true;
            UIanimator.SetBool("talk", false);
        }
    }

    int  sum=0;
    void RandomingRate() {
        for (int i = 0; i < RewardingAndRate.Length;i++)
        {
            sum += RewardingAndRate[i].rate;
        }
        int k = Random.Range(0,sum);
        int j = RewardingAndRate[0].rate;
        for (int i = 0; i <RewardingAndRate.Length; i++)
        {
            if (k <= j)
            {
                Instantiate(RewardingAndRate[i].reward, transform.position, Quaternion.identity);
                break;
            }
            else {
                j += RewardingAndRate[i+1].rate;
            }
        }
    }
}

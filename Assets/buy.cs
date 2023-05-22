using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buy : MonoBehaviour
{
    Text showtext;
    public int price;
    public GameObject reward;
    public int rewardNum;
    // Start is called before the first frame update
    void Start()
    {
        showtext = GameObject.Find("mianui").transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"&&other.name=="player") {
            if (other.transform.GetChild(0).GetComponent<PlayControl>().coin >= price)
            {
                other.transform.GetChild(0).GetComponent<PlayControl>().coin -= price;
                for (int i = 0; i < rewardNum; i++)
                {
                    Instantiate(reward, GameObject.Find("shop").transform.GetChild(1).position, Quaternion.identity);
                }
                Destroy(gameObject, 0.2f);
                showtext.text = "--";
            }
            else {
                showtext.text = "½ð±Ò²»¹»";
            }
        }
    }
}

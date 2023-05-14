using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeselection : MonoBehaviour
{
    public string[] dialogue = new string[8];

    public string[] greetingReply = new string[5];

    private Text text;
    GameObject playersave;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        playersave = GameObject.Find("playersave");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeConversation()
    {
        if (PlayerPrefs.GetInt("TimeMeet") <= 8)
        {
            switch (PlayerPrefs.GetInt("TimeMeet"))
            {
                case 1:
                    text.text = dialogue[0];
                    break;
                case 2:
                    text.text = dialogue[1];
                    break;
                case 3:
                    text.text = dialogue[2];
                    break;
                case 4:
                    text.text = dialogue[3];
                    break;
                case 5:
                    text.text = dialogue[4];
                    break;
                case 6:
                    text.text = dialogue[5];
                    break;
                case 7:
                    text.text = dialogue[6];
                    break;
                case 8:
                    text.text = dialogue[7];
                    break;
            }
        }
        else
        {
            text.text = greetingReply[Random.Range(0, greetingReply.Length)];
        }
    }
}

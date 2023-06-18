using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sunbosschangetalk : MonoBehaviour
{
    public Text talk;
    public Text select1t;
    public Text select2t;
    int clicktime;
    public void changetext() {
        clicktime++;
        switch (clicktime) {
            case 1:
                select1t.text = "居然是一场历经几个世纪误解，，，，，";
                select2t.text = "原来神明一直在保护人类！";
                talk.text = "黑暗神，在众神诞生之初便存在，我们众神选择向善，保护人类，而黑暗神则是所有神的阴暗面，我作为众神之神，理应为此负责，但它无法消灭，只要存在善必然存在恶。而我在长期的对抗中无暇顾及全部，导致战火波及人间。";
                break;
            case 2:
                gameObject.GetComponent<Canvas>().enabled=false;
                break;
        }
    }
}

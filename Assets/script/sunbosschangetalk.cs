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
                select1t.text = "��Ȼ��һ����������������⣬��������";
                select2t.text = "ԭ������һֱ�ڱ������࣡";
                talk.text = "�ڰ�����������֮������ڣ���������ѡ�����ƣ��������࣬���ڰ�������������������棬����Ϊ����֮����ӦΪ�˸��𣬵����޷�����ֻҪ�����Ʊ�Ȼ���ڶ񡣶����ڳ��ڵĶԿ�����Ͼ�˼�ȫ��������ս�𲨼��˼䡣";
                break;
            case 2:
                gameObject.GetComponent<Canvas>().enabled=false;
                break;
        }
    }
}

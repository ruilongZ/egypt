using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class discribetext : MonoBehaviour
{
    public string discribe;
    public float distancetoshow;
    Text showtext;
    bool showed=true;
    // Start is called before the first frame update
    void Start()
    {
        showtext = GameObject.Find("mianui").transform.GetChild(2).GetComponent<Text>();
    }

    void Update()
    {
        if (Vector3.Distance(GameObject.Find("player").transform.position, gameObject.transform.position) <= distancetoshow && showed)
        {
            showtext.text = discribe;
            StartCoroutine("settext");
            showed = false;
        }
        if (Vector3.Distance(GameObject.Find("player").transform.position, gameObject.transform.position) >= distancetoshow + 1)
        {
            showed = true;
        }
    }
    IEnumerator settext() {
        yield return new WaitForSeconds(5);            showtext.text = "--";
    }
}

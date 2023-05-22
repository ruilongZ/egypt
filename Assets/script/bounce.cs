using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bounce : MonoBehaviour
{
     Vector3 dir;
    public float speed;
    bool blocked;
    Text showtext;
    private void Start()
    {
        showtext = GameObject.Find("mianui").transform.GetChild(2).GetComponent<Text>();
        dir = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0).normalized;
    }
    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
        if (blocked)
        {
            speed = 0;
        }
        else {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * 1.5f);
        }

        if (speed < 0.1f) {
            speed = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="block") {
            blocked = true;
        }
        if (other.tag == "Player" )
        {
            blocked = true;
            Destroy(gameObject,0.2f);
            showtext.text = "--";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunbossfirecontrol : MonoBehaviour
{
    Collider collider;
    public float damage;
    bool followplayer=true;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        StartCoroutine("setable");
        Destroy(gameObject,1.5f);
    }
    IEnumerator setable() {
        yield return new WaitForSeconds(0.7f);
        followplayer = false;
        yield return new WaitForSeconds(0.2f);
        collider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (followplayer) {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}

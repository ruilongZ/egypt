using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashmove : MonoBehaviour
{
    GameObject player;
    Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        Destroy(gameObject, 1f);
        collider = GetComponent<Collider>();
        StartCoroutine("setable");
    }
    IEnumerator setable() {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }
}

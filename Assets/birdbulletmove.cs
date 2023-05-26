using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdbulletmove : MonoBehaviour
{
    GameObject player;
    public float speed;
    Collider collider;
    public float addlife;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        Destroy(gameObject, 10f);
        collider = GetComponent<Collider>();
        StartCoroutine("setable");
    }
    IEnumerator setable()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate((player.transform.position +new Vector3(0,1.5f,0)- transform.position).normalized * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"&&other.name=="player") {
            GameObject.Find("sunbosscharacter").GetComponent<sunbosscontrol>().currentlife += addlife;
            GameObject.Find("sunbosscharacter").GetComponent<sunbosscontrol>().setui();
        }
    }
}

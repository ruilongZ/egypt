using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBullet : MonoBehaviour
{
    GameObject player;
    Vector3 dir;
    public GameObject EnemySpawned;
    
    public float speed;
    bool die = false;
    public float multi = 0.8f;
    bool blocked;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dir = player.transform.position + player.GetComponent<PlayerMovementNew>().dir - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       speed = Mathf.Lerp(speed, 0, Time.deltaTime*multi);
        if (blocked|| speed < 0.1f) {
            speed = 0;
            Instantiate(EnemySpawned, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        transform.Translate(dir * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block") {
            blocked = true;
        }
    }
}

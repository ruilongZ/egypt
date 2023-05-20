using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterp2bulletmovement : MonoBehaviour
{
    GameObject player;
    Vector3 dir;
    public float speed;
    public float damping;
    bool die;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dir = player.transform.position + player.GetComponent<PlayerMovementNew>().dir - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            speed = 0;
        }
        else
        {
            speed = Mathf.Lerp(speed,0,Time.deltaTime*damping);
        }
        transform.Translate(dir.normalized * Time.deltaTime * speed);
        if (speed<0.3f) {
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "block")
        {

            die = true;
        }
    }
}

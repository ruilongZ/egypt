using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public float speed;
    GameObject player;
    GameObject enemybat;
    public bool playerbullet;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        if (playerbullet)
        {
            player = GameObject.Find("player");
            dir = player.GetComponent<PlayerMovementNew>().dir;
        }
        else {
            enemybat = GameObject.FindGameObjectWithTag("Enemy");
            dir = enemybat.GetComponent<EnemyMovementNew>().movedir;
        }
        Destroy(gameObject, 3);
        DiretionChange();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * speed);
    }
    void DiretionChange()
    {
        float Angle=0;
        if (playerbullet)
        {
            Angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x- transform.position.x) * Mathf.Rad2Deg;
        }
        else
        {
            Angle = Mathf.Atan2(enemybat.transform.position.y- transform.position.y, enemybat.transform.position.x- transform.position.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle + 180));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="block"|| other.tag=="Enemy"||other.tag=="Player") {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public float speed;
    GameObject player;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        dir = player.GetComponent<PlayerMovementNew>().dir;
        DiretionChange();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * speed);
    }
    void DiretionChange()
    {
        float Angle = Mathf.Atan2(player.transform.position.y + dir .y- transform.position.y, player.transform.position.x +dir.x- transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle + 180));
    }
}

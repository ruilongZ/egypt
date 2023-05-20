using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontrol : MonoBehaviour
{
    public GameObject warning;
    public GameObject attackcollider;
    public float warningtime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("warningtoattack");
    }
    IEnumerator warningtoattack() {
        yield return new WaitForSeconds(0.1f);
        Instantiate(warning, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(warningtime);
        Instantiate(attackcollider, transform.position, Quaternion.Euler(0,0,-90));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "boss" || other.tag == "block")
        {
            Destroy(gameObject);
        }
    }
}

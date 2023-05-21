using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackreward : MonoBehaviour
{
    public GameObject follow;
    public GameObject reward;
    public Vector3 offset;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"&&other.name=="player") {
            StartCoroutine("instance");
            Destroy(gameObject,0.6f);
        }
    }
    IEnumerator instance() {
        yield return new WaitForSeconds(0.5f);
        Instantiate(follow, transform.position + offset, Quaternion.identity);
        Instantiate(reward, transform.position + offset, Quaternion.identity);
    }
}

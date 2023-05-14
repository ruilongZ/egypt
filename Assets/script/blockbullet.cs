using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockbullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if (other.tag=="playbullet") {
            other.GetComponent<BulletMovementNew>().StartCoroutine("DestoryBullet");
        }
    }
}

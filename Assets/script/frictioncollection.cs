using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frictioncollection : MonoBehaviour
{
    public float frictionadd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&other.name=="player") {
            other.GetComponent<PlayerMovementNew>().Friction += frictionadd;
        }
    }
}

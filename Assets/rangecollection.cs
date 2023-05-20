using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangecollection : MonoBehaviour
{
    public float rangeadd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.name == "player")
        {
            other.GetComponent<PlayerMovementNew>().BulletRange += rangeadd;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratecollection : MonoBehaviour
{
    public float rateadd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.name == "player")
        {
            other.GetComponent<PlayerMovementNew>().FiringRate -= rateadd;
        }
    }
}

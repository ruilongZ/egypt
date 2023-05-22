using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprintdamagecollection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name=="character")
        {
            other.GetComponent<PlayControl>().sprintdamageequip= true;
        }
    }
}

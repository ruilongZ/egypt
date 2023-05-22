using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blooddamagecollection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "character")
        {
            other.GetComponent<PlayControl>().bloodtodamageequip = true;
        }
    }
}

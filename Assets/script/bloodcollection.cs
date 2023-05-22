using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodcollection : MonoBehaviour
{
    public float bloodadd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.name == "character")
        {
            other.GetComponent<PlayControl>().currentlife += bloodadd;
            if (other.GetComponent<PlayControl>().currentlife >= 100)
            {
                other.GetComponent<PlayControl>().currentlife = 100;
            }
            other.GetComponent<PlayControl>().setbloodbar();
        }
    }
}

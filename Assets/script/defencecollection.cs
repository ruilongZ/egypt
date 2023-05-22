using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defencecollection : MonoBehaviour
{
    public float defenceadd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.name == "character")
        {
            other.GetComponent<PlayControl>().defence += defenceadd;
            other.GetComponent<PlayControl>().setdefenceNum();
            other.GetComponent<PlayControl>().setdefencebar();
        }
    }
}

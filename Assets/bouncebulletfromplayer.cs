using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncebulletfromplayer : MonoBehaviour
{
    public GameObject bouncebullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 hitpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet") {
            hitpoint = other.bounds.ClosestPoint(transform.position);
            Instantiate(bouncebullet, hitpoint, Quaternion.identity);
        }
    }
}

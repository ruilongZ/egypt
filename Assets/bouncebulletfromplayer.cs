using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncebulletfromplayer : MonoBehaviour
{
    public GameObject bouncebullet;
    public GameObject enemybouncebullet;
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
        if (other.tag == "playerbullet"|| other.tag == "enemybullet") {
            hitpoint = other.bounds.ClosestPoint(transform.position);
            switch (other.tag) {
                case "playerbullet":
                    Instantiate(bouncebullet, hitpoint, Quaternion.identity);
                    break;
                case "enemybullet":
                    Instantiate(enemybouncebullet, hitpoint, Quaternion.identity);
                    break;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstercontrol : MonoBehaviour
{
    public GameObject monsterp1;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(monsterp1, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

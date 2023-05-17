using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryself : MonoBehaviour
{
    public float destorytime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destorytime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

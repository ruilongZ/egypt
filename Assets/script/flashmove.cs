using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class flashmove : MonoBehaviour
{
    Collider collider;
    public GameObject waining;
    GameObject wainingsign;
    public float vfxlife;
    public float waittime;
    public float damageactive;
    public float standardsize=1;
    public Vector3 offset=new Vector3(0,0.5f,0);
    // Start is called before the first frame update
    void Start()
    {
        wainingsign = Instantiate(waining,( transform.position+offset), Quaternion.identity);
        transform.localScale = Vector3.one * Random.Range(standardsize-0.4f, standardsize+0.2f);
        Destroy(wainingsign, waittime);
        collider = GetComponent<Collider>();
        StartCoroutine("setable");
        Destroy(gameObject, vfxlife+waittime);
    }
    IEnumerator setable() {
        yield return new WaitForSeconds(waittime);
        collider.enabled = true;
    }
}

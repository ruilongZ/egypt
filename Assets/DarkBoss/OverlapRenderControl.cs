using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverlapRenderControl : MonoBehaviour
{
    private Renderer myRenderer;
    private int originOrder;
    bool addLayer = false;
    int otherOrder;
    private float timer;
    private float maxTime=2;
    private void Awake()
    {
        myRenderer = gameObject.GetComponentInParent<Renderer>();
        originOrder = myRenderer.sortingOrder;
    }


    private void LateUpdate()   //在角色和场景内所有物体(包括摄像机)完成移动后进行
    {
        if(addLayer)
        {
            myRenderer.sortingOrder ++;
            addLayer = false;
        }
            //if(this.transform.position.y<)
            //myRenderer.sortingOrder = 
        }
    private void OnTriggerStay(Collider other)
    {
        otherOrder = other.GetComponentInParent<Renderer>().sortingOrder;
        if(this.transform.position.y<other.transform.position.y&& myRenderer.sortingOrder <= otherOrder)
        {
            addLayer = true; 
        }
       // else if(this.transform.position.x<other.transform.position.x)
    }
    private void OnTriggerExit(Collider other)
    {

        myRenderer.sortingOrder = originOrder;
    }
}

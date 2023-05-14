using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomcontrol : MonoBehaviour
{
    public GameObject[] room=new GameObject[12];
    // Start is called before the first frame update
    void Start()
    {
        SetAllRoomCamPointDisable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAllRoomCamPointDisable() {
        foreach (GameObject gameObject in room)
        {
            // Get the component you want to disable from the game object
            Component componentToDisable = gameObject.GetComponent<CinemachineVirtualCamera>();

            
            // Disable the component
            if (componentToDisable != null)
            {
                gameObject.GetComponent<singleroom>().tofalse();
                (componentToDisable as Behaviour).enabled = false;
            }
        }
    }
}

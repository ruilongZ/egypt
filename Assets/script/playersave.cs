using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddNpcTime() {
        int TimeSeeNpc = PlayerPrefs.GetInt("TimeMeet", 0);
        PlayerPrefs.SetInt("TimeMeet", TimeSeeNpc+1);
        PlayerPrefs.Save();
    }
}

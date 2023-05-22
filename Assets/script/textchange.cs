using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textchange : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void changetext(string discribe) {
        text.text = discribe;
    }
}

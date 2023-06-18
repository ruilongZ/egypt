using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplay : MonoBehaviour
{
    public AudioClip[] music;
    AudioSource audio;
    AudioClip choosen;
    float passtime;
    bool changed;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        choosen = music[Random.Range(0, music.Length)];
        audio.PlayOneShot(choosen);
    }

    // Update is called once per frame
    void Update()
    {
        if (passtime >= choosen.length)
        {
            changed = true;
            passtime = 0;
        }
        else {
            passtime += Time.deltaTime;
        }
        if (changed) {
            choosen = music[Random.Range(0, music.Length)];
            audio.PlayOneShot(choosen);
            changed = false;
        }
    }
}

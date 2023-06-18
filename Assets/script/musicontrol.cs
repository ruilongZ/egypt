using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicontrol : MonoBehaviour
{
    [Header("基础参数")]
    public float sounddamping;
    public float maxvolum;
    [SerializeField]
    [Header("播放器")]
    public AudioSource commonroom;
    public AudioSource shop;
    public AudioSource god;
    public AudioSource secret;
    public AudioSource boss;
    private void Start()
    {
        commonroom.volume = 0;
        shop.volume = 0;
        god.volume = 0;
        secret.volume = 0;
        boss.volume = 0;
    }

    public void entercommon() {
        commonroom.volume = Mathf.Lerp(commonroom.volume, 1* maxvolum, Time.deltaTime * sounddamping);
        shop.volume = Mathf.Lerp(shop.volume, 0, Time.deltaTime * sounddamping);
        god.volume = Mathf.Lerp(god.volume, 0, Time.deltaTime * sounddamping);
        secret.volume = Mathf.Lerp(secret.volume, 0, Time.deltaTime * sounddamping);
        boss.volume = Mathf.Lerp(boss.volume, 0, Time.deltaTime * sounddamping);
    }
    public void entershop()
    {
        commonroom.volume = Mathf.Lerp(commonroom.volume, 0, Time.deltaTime * sounddamping);
        shop.volume = Mathf.Lerp(shop.volume, 0.7f * maxvolum, Time.deltaTime * sounddamping);
        god.volume = Mathf.Lerp(god.volume, 0, Time.deltaTime * sounddamping);
        secret.volume = Mathf.Lerp(secret.volume, 0, Time.deltaTime * sounddamping);
        boss.volume = Mathf.Lerp(boss.volume, 0, Time.deltaTime * sounddamping);
    }
    public void entergod()
    {
        commonroom.volume = Mathf.Lerp(commonroom.volume, 0, Time.deltaTime * sounddamping);
        shop.volume = Mathf.Lerp(shop.volume, 0, Time.deltaTime * sounddamping);
        god.volume = Mathf.Lerp(god.volume, 0.8f* maxvolum, Time.deltaTime * sounddamping);
        secret.volume = Mathf.Lerp(secret.volume, 0, Time.deltaTime * sounddamping);
        boss.volume = Mathf.Lerp(boss.volume, 0, Time.deltaTime * sounddamping);
    }
    public void entersecret()
    {
        commonroom.volume = Mathf.Lerp(commonroom.volume, 0, Time.deltaTime * sounddamping);
        shop.volume = Mathf.Lerp(shop.volume, 0, Time.deltaTime * sounddamping);
        god.volume = Mathf.Lerp(god.volume, 0, Time.deltaTime * sounddamping);
        secret.volume = Mathf.Lerp(secret.volume, 1 * maxvolum, Time.deltaTime * sounddamping);
        boss.volume = Mathf.Lerp(boss.volume, 0, Time.deltaTime * sounddamping);
    }
    public void enterboss()
    {
        commonroom.volume = Mathf.Lerp(commonroom.volume, 0, Time.deltaTime * sounddamping);
        shop.volume = Mathf.Lerp(shop.volume, 0, Time.deltaTime * sounddamping);
        god.volume = Mathf.Lerp(god.volume, 0, Time.deltaTime * sounddamping);
        secret.volume = Mathf.Lerp(secret.volume, 0, Time.deltaTime * sounddamping);
        boss.volume = Mathf.Lerp(boss.volume, 1 * maxvolum, Time.deltaTime * sounddamping);
    }
}

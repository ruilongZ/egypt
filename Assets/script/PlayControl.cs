using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    [Header("Íæ¼Ò»ù´¡")]
    public float currentlife;
    public float maxlife;
    Animator playerAnimator;
    bool die;
     void Start()
    {
        currentlife = maxlife;
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (die) {
            dieanddestory();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemybullet"|| other.tag == "Enemy")
        {
            currentlife -= 1;
            playerAnimator.SetTrigger("attacked");
            if (currentlife <= 0)
            {
                die = true;
                playerAnimator.SetTrigger("die");
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                GetComponentInParent<PlayerMovementNew>().MaxSpeedMultiplier = 0;
                GetComponentInParent<PlayerMovementNew>().SprintSpeed = 0;
                Invoke("Setactivefalse",2f);
            }
        }
    }
    void Setactivefalse() {
        Destroy(GetComponentInParent<PlayerMovementNew>().gameObject);
    }
    void dieanddestory() {
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        Destroy(GameObject.FindGameObjectWithTag("enemybullet"));
        Destroy(GameObject.FindGameObjectWithTag("god"));
    }
}

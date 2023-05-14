using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    [Header("Íæ¼Ò»ù´¡")]
    public float life;
    Animator playerAnimator;
     void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemybullet"|| other.tag == "Enemy")
        {
            life -= 1;
            playerAnimator.SetTrigger("attacked");
            if (life <= 0)
            {
                dieanddestory();
                playerAnimator.SetTrigger("die");
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

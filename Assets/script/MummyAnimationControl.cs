using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyAnimationControl : MonoBehaviour
{
    [Header("Ä¾ÄËÒÁ»ù´¡")]
    public float life;


    Animator mummyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mummyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mummyAnimator.SetFloat("x",GetComponentInParent<EnemyMovementNew>().movedir.x*1000);
        if (GetComponentInParent<EnemyMovementNew>().distanceToPlayer<1.2 && GetComponentInParent<EnemyMovementNew>().IsCloseCombat) {
            mummyAnimator.SetTrigger("attack");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="playerbullet") {
            life -= other.GetComponentInParent<BulletMovementNew>().damage;
            GetComponentInParent<EnemyMovementNew>().attacted = true;
            mummyAnimator.SetTrigger("attacked");
        }
        if (other.name == "character")
        {

            if (other.GetComponent<PlayControl>().sprintdamageequip&& other.GetComponent<PlayControl>().ShiftPressed)
            {

                life -= other.GetComponent<PlayControl>().defence;
                GetComponentInParent<EnemyMovementNew>().attacted = true;
                mummyAnimator.SetTrigger("attacked");
            }
        }
        if (life <= 0)
        {
            mummyAnimator.SetTrigger("die");
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GetComponentInParent<EnemyMovementNew>().die = true;
            Destroy(GetComponentInParent<EnemyMovementNew>().gameObject, 0.7f);
        }
    }

}

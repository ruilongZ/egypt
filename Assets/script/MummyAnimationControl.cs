using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyAnimationControl : MonoBehaviour
{
    [System.Serializable]
    public struct RewardAndRate
    {
        public int rate;
        public GameObject reward;

        public RewardAndRate(int rate, GameObject reward)
        {
            this.rate = rate;
            this.reward = reward;
        }
    }

    [Header("生成奖励概率控制")]
    public RewardAndRate[] RewardingAndRate;

    [Header("木乃伊基础")]
    public float life;
    Collider collider;

    Animator mummyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mummyAnimator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("setanimator");
        if (GetComponentInParent<EnemyMovementNew>().distanceToPlayer<1.2 && GetComponentInParent<EnemyMovementNew>().IsCloseCombat) {
            mummyAnimator.SetTrigger("attack");
        }
    }
    IEnumerator setanimator() {
        yield return new WaitForSeconds(0.8f);
        mummyAnimator.SetTrigger("move");
        mummyAnimator.SetFloat("x", GetComponentInParent<EnemyMovementNew>().movedir.x * 1000);
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
            RandomingRate();
            collider.enabled = false;
        }
    }

    int sum = 0;
    void RandomingRate()
    {
        for (int i = 0; i < RewardingAndRate.Length; i++)
        {
            sum += RewardingAndRate[i].rate;
        }
        int k = Random.Range(0, sum);
        int j = 0;
        for (int i = 0; i < RewardingAndRate.Length; i++)
        {
            if (k <= j + RewardingAndRate[i].rate)
            {
                Instantiate(RewardingAndRate[i].reward, transform.position, Quaternion.identity);
                break;
            }
            else
            {
                    j += RewardingAndRate[i].rate;
            }
        }
    }
}

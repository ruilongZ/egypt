using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class singleroom : MonoBehaviour
{
    public enum RoomKind { 
     EnemyRoom,
     BossRoom,
     StartRoom,
     RewardRoom,
     ShopRoom,
     HiddenRoom,
    }

    [Header("basic")]
    public bool PlayerIsInRoom = false;
    public RoomKind RoomType=RoomKind.EnemyRoom;
    [Space]
    public GameObject[] door=new GameObject[4];
    [Space]
    [Header("generate enemy")]
    public GameObject[] enemy = new GameObject[2];
    public int[] enemyNum = new int[2];
    [Space]
    [Header("generate boss")]
    public GameObject boss ;
    [Space]
    [Header("generate rewardgod")]
    public GameObject[] rewardgod=new GameObject[7];
    [Space]
    [Header("generate shop")]
    public GameObject shop;
    [Space]
    [Header("generate fairy")]
    public GameObject[] fairy=new GameObject[2];

    private CinemachineVirtualCamera CamVC;
    private GameObject roomcontrol;
    private bool PlayerFirstEnter=false;
    // Start is called before the first frame update
    void Start()
    {
        CamVC = GetComponent<CinemachineVirtualCamera>();
        roomcontrol = GameObject.Find("roommanager");
        OpenAllDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy")==null) {
            OpenAllDoor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
         PlayerIsInRoom = true;
            roomcontrol.GetComponent<roomcontrol>().SetAllRoomCamPointDisable();
            CamVC.enabled = true;

            if (!PlayerFirstEnter)
            {
                switch (RoomType) {
                    case RoomKind.StartRoom:
                        break;
                    case RoomKind.ShopRoom:
                        GenerateShop();
                        break;
                    case RoomKind.BossRoom:
                        GenerateBoss();
                        break;
                    case RoomKind.RewardRoom:
                        GenerateRewardGod();
                        break;
                    case RoomKind.HiddenRoom:
                        GenerateFairy();
                        break;
                    case RoomKind.EnemyRoom:
                        StartCoroutine("CloseAllDoor");
                        GenerateEnemy();
                        break;
                }

                PlayerFirstEnter = true;
            }
        }
    }

    public void tofalse() {
        PlayerIsInRoom = false;
    }
    public void OpenAllDoor() {
        foreach (GameObject gameObject in door) {
            gameObject.SetActive(false);
        }
    }
    IEnumerator  CloseAllDoor()
    {
        yield return new WaitForSeconds(0.4f);
        foreach (GameObject gameObject in door)
        {
            gameObject.SetActive(true);
        }
    }

    public  Vector3 GetRandomPointInRoom() {
        Vector3 point = new Vector3(Random.Range(transform.position.x-8, transform.position.x +8), Random.Range(transform.position.y - 4, transform.position.y + 4), 0);
        return point;
    }
    public void GenerateEnemy() {

        if (enemyNum[0]>0) {
            Instantiate(enemy[0], GetRandomPointInRoom(), Quaternion.identity);
            enemyNum[0]--;
        }

        if (enemyNum[1] > 0)
        {
            Instantiate(enemy[1], GetRandomPointInRoom(), Quaternion.identity);
            enemyNum[1]--;
        }
    }
    public void GenerateBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
    public void GenerateShop()
    {
        Instantiate(shop, transform.position, Quaternion.identity);
    }
    public void GenerateRewardGod()
    {
        Instantiate(rewardgod[Random.Range(0,rewardgod.Length+1)], Vector3.zero, Quaternion.identity);
    }
    public void GenerateFairy()
    {
        Instantiate(fairy[0], transform.position, Quaternion.identity);
    }
}

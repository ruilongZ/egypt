using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    #region  贴图相关
    [Header("RoomTexture")]
    public Sprite basicroomtex;
    public Sprite secretroomtex;
    public Sprite rewardroomtex;
    public Sprite bossroomtex;
    public Sprite shoproomtex;
    [Space]
    [Header("DoorOpenTexture")]
    public Sprite basicopendoortex;
    public Sprite secretopendoortex;
    public Sprite rewardopendoortex;
    public Sprite bossopendoortex;
    public Sprite shopopendoortex;
    [Space]
    [Header("DoorOpenTexture")]
    public Sprite basicclosedoortex;
    public Sprite secretclosedoortex;
    public Sprite rewardclosedoortex;
    public Sprite bossclosedoortex;
    public Sprite shopclosedoortex;
    #endregion

    #region 控制贴图对象
    [Space]
    [Header("doors and walls")]
    public SpriteRenderer[] opendoor;
    public SpriteRenderer[] closedoor;
    #endregion

    [Space]
    [Header("basic")]
    public bool PlayerIsInRoom = false;
    public RoomKind RoomType=RoomKind.EnemyRoom;
    public Image smallmap;
    [Space]
    public GameObject[] door=new GameObject[4];


    [System.Serializable]
    public struct SpawnEnenyAndNum
    {
        public int enemyNum;
        public GameObject enemyType;

        public SpawnEnenyAndNum(int enemyNum, GameObject enemyType)
        {
            this.enemyNum = enemyNum;
            this.enemyType = enemyType;
        }
    }

    #region 自动生成房间内相关
    [Space]
    [Header("generate enemy")]
    public SpawnEnenyAndNum[] SpawnEnemy;
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
    #endregion

    #region 摄像机控制与私有变量
    private CinemachineVirtualCamera CamVC;
    private GameObject roomcontrol;
    private bool PlayerFirstEnter=false;
    #endregion

    private void Awake()
    {
        switch (RoomType) {
            case RoomKind.StartRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = basicroomtex;
                foreach (SpriteRenderer j in opendoor) {
                    j.sprite = basicopendoortex;
                }
                foreach (SpriteRenderer k in closedoor)
                {
                    k.sprite = basicclosedoortex;
                }
                break;
            case RoomKind.ShopRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shoproomtex;
                foreach (SpriteRenderer j in opendoor)
                {
                    j.sprite =shopopendoortex;
                }
                foreach (SpriteRenderer k in closedoor)
                {
                    k.sprite = shopclosedoortex;
                }
                break;
            case RoomKind.BossRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =bossroomtex;
                foreach (SpriteRenderer j in opendoor)
                {
                    j.sprite = bossopendoortex;
                }
                foreach (SpriteRenderer k in closedoor)
                {
                    k.sprite = bossclosedoortex;
                }
                break;
            case RoomKind.RewardRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =rewardroomtex;
                foreach (SpriteRenderer j in opendoor)
                {
                    j.sprite = rewardopendoortex;
                }
                foreach (SpriteRenderer k in closedoor)
                {
                    k.sprite = rewardclosedoortex;
                }
                break;
            case RoomKind.HiddenRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = secretroomtex;
                foreach (SpriteRenderer j in opendoor)
                {
                    j.sprite = secretopendoortex;
                }
                foreach (SpriteRenderer k in closedoor)
                {
                    k.sprite = secretclosedoortex;
                }
                break;
        }
    }

    void Start()
    {
        CamVC = GetComponent<CinemachineVirtualCamera>();
        roomcontrol = GameObject.Find("roommanager");
        smallmap.color = Color.gray;
        OpenAllDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy")==null&& GameObject.FindGameObjectWithTag("boss")==null) {
            OpenAllDoor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player") {
            smallmap.color = Color.white;
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
                        StartCoroutine("CloseAllDoor");
                        StartCoroutine("GenerateBoss");
                        break;
                    case RoomKind.RewardRoom:
                        GenerateRewardGod();
                        break;
                    case RoomKind.HiddenRoom:
                        GenerateFairy();
                        break;
                    case RoomKind.EnemyRoom:
                        StartCoroutine("CloseAllDoor");
                        StartCoroutine("GenerateEnemy");
                        break;
                }

                PlayerFirstEnter = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "player")
        {
            smallmap.color = Color.gray;
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
    IEnumerator GenerateEnemy() {
        yield return new WaitForSeconds(0.2f);
        foreach (SpawnEnenyAndNum i in SpawnEnemy) {
            for (int j=0;j<i.enemyNum;j++) {
                Instantiate(i.enemyType, GetRandomPointInRoom(), Quaternion.identity);
            }
        }
    }
    IEnumerator GenerateBoss()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(boss, transform.position, Quaternion.identity);
    }
    public void GenerateShop()
    {
        Instantiate(shop, transform.position, Quaternion.identity);
    }
    public void GenerateRewardGod()
    {
        Instantiate(rewardgod[Random.Range(0,rewardgod.Length)], transform.position, Quaternion.identity);
    }
    public void GenerateFairy()
    {
        Instantiate(fairy[Random.Range(0,2)], transform.position, Quaternion.identity);
    }
}

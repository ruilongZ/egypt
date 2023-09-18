using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class singleroom : MonoBehaviour
{
    public enum RoomKind
    {
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
    List<SpriteRenderer> opendoor = new List<SpriteRenderer>();
    List<SpriteRenderer> closedoor = new List<SpriteRenderer>();
    #endregion

    [Space]
    [Header("basic")]
    public bool PlayerIsInRoom = false;
    public RoomKind RoomType = RoomKind.EnemyRoom;
    public Image smallmap;
    [Space]
    public GameObject[] doorpos = new GameObject[4];
    public List<GameObject> door = new List<GameObject>();
    public GameObject withdoor;
    public GameObject withoutdoor;
    #region 刷怪结构体
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
    #endregion

    #region 自动生成房间内相关
    [Space]
    [Header("generate enemy")]
    public SpawnEnenyAndNum[] SpawnEnemy;
    [Space]
    [Header("generate boss")]
    public GameObject boss;
    [Space]
    [Header("generate rewardgod")]
    public GameObject[] rewardgod = new GameObject[7];
    [Space]
    [Header("generate shop")]
    public GameObject shop;
    [Space]
    [Header("generate fairy")]
    public GameObject[] fairy = new GameObject[2];
    #endregion

    #region 摄像机控制与私有变量
    private CinemachineVirtualCamera CamVC;
    private GameObject roomcontrol;
    private bool PlayerFirstEnter = false;

    float rightroomPosOffset = 19.2f;
    float uproomPosOffset = 10.8f;
    #endregion

    private void Awake()
    {
        roomcontrol = GameObject.Find("roommanager");
        roomcontrol.GetComponent<newRoomControl>().takenpos.Add(transform.position);
        roomcontrol.GetComponent<newRoomControl>().allroom.Add(gameObject);


        StartCoroutine(spawndoorAndGivetexture(0.3f));

    }
    IEnumerator spawndoorAndGivetexture(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        int withoutDoorNum = 0;

        //生成门
        if (RoomType != RoomKind.ShopRoom && RoomType != RoomKind.BossRoom)
        {
            //遍历所有位置物体的子集里是否有门，如果没有子集，则随机生成门并存入数组门种。如果子集有东西，判断是不是门，是门则装入数组门
            foreach (GameObject i in doorpos)
            {
                if (i.transform.childCount == 0)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        if (roomOverlay(i.name) && (roomcontrol.GetComponent<newRoomControl>().commonroomNum > 0 || roomcontrol.GetComponent<newRoomControl>().hiddenroomNum > 0 || roomcontrol.GetComponent<newRoomControl>().rewardroomNum > 0))
                        {
                            GameObject instancedoor = Instantiate(withdoor, i.transform.position, i.transform.rotation);
                            instancedoor.transform.parent = i.transform;
                            roomcontrol.GetComponent<newRoomControl>().SpawnRoomByDoorNameAndSpawnDoor(transform, i.name);
                            door.Add(instancedoor);
                        }
                        else
                        {
                            withoutDoorNum++;
                            GameObject instancedoor = Instantiate(withoutdoor, i.transform.position, i.transform.rotation);
                            instancedoor.transform.parent = i.transform;
                        }
                    }
                    else
                    {
                        if (roomOverlay(i.name) && withoutDoorNum >= 3 && (roomcontrol.GetComponent<newRoomControl>().commonroomNum > 0 || roomcontrol.GetComponent<newRoomControl>().hiddenroomNum > 0 || roomcontrol.GetComponent<newRoomControl>().rewardroomNum > 0))
                        {
                            GameObject instancedoor = Instantiate(withdoor, i.transform.position, i.transform.rotation);
                            instancedoor.transform.parent = i.transform;
                            roomcontrol.GetComponent<newRoomControl>().SpawnRoomByDoorNameAndSpawnDoor(transform, i.name);
                            door.Add(instancedoor);
                        }
                        else
                        {
                            withoutDoorNum++;
                            GameObject instancedoor = Instantiate(withoutdoor, i.transform.position, i.transform.rotation);
                            instancedoor.transform.parent = i.transform;
                        }
                    }

                }
                else
                {
                    if (i.transform.Find("withdoor(Clone)")!=null)
                    {
                        door.Add(i.transform.Find("withdoor(Clone)").gameObject);
                    }
                }
            }

            //从门数组中检出开门关门的render
            foreach (GameObject j in door) {
                opendoor.Add(j.GetComponent<SpriteRenderer>());
                closedoor.Add(j.transform.Find("doorclose").gameObject.GetComponent<SpriteRenderer>());
            }

            givetexture();
        }
        else
        {
            //关闭门
            foreach (GameObject i in doorpos)
            {
                if (i.transform.childCount == 0)
                {
                    GameObject instancedoor = Instantiate(withoutdoor, i.transform.position, i.transform.rotation);
                    instancedoor.transform.parent = i.transform;
                }
            }
        }
    }   
    
    //给种屋子不同的背景与门款式
    void givetexture() {

        switch (RoomType)
        {
            case RoomKind.StartRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = basicroomtex;
                foreach (SpriteRenderer j in opendoor)
                {
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
                    j.sprite = shopopendoortex;
                }
                foreach (SpriteRenderer k in closedoor)
                {
                    k.sprite = shopclosedoortex;
                }
                break;
            case RoomKind.BossRoom:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bossroomtex;
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
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = rewardroomtex;
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


    public IEnumerator updatedoors(float time)
    {
        yield return new WaitForSeconds(time);
        door.Clear();
        foreach (GameObject i in doorpos)
        {
            if (i.transform.Find("withdoor(Clone)")!=null)
            {
                door.Add(i.transform.Find("withdoor(Clone)").gameObject);
            }
        }

        //从门数组中检出开门关门的render
        foreach (GameObject j in door)
        {
            opendoor.Add(j.GetComponent<SpriteRenderer>());
            closedoor.Add(j.transform.Find("doorclose").gameObject.GetComponent<SpriteRenderer>());
        }

        givetexture();
    }

    //根据门来判断即将生成的房间位置是否有重叠
    bool roomOverlay(string doorpos)
    {
        bool canspawn = true;
        Vector3 prespawnpos;
        switch (doorpos)
        {
            case "dooruppos":
                prespawnpos = new Vector3(transform.position.x, transform.position.y + uproomPosOffset, 0);
                if(roomcontrol.GetComponent<newRoomControl>().takenpos.Contains(prespawnpos))
                {
                     canspawn = false;
                }
                break;
            case "doorleftpos":
                prespawnpos = new Vector3(transform.position.x - rightroomPosOffset, transform.position.y, 0);
                if (roomcontrol.GetComponent<newRoomControl>().takenpos.Contains(prespawnpos))
                {
                    canspawn = false;
                }
                break;
            case "doorrightpos":
                prespawnpos = new Vector3(transform.position.x + rightroomPosOffset, transform.position.y, 0);
                if (roomcontrol.GetComponent<newRoomControl>().takenpos.Contains(prespawnpos))
                {
                    canspawn = false;
                }
                break;
            case "doordownpos":
                prespawnpos = new Vector3(transform.position.x, transform.position.y - uproomPosOffset, 0);
                if (roomcontrol.GetComponent<newRoomControl>().takenpos.Contains(prespawnpos))
                {
                    canspawn = false;
                }
                break;
        }
        return canspawn;
    }

    void Start()
    {
        CamVC = GetComponent<CinemachineVirtualCamera>();
        //smallmap.color = Color.gray;
    }


    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("boss") == null)
        {
            OpenAllDoor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player")
        {
            //smallmap.color = Color.white;
            PlayerIsInRoom = true;
            //roomcontrol.GetComponent<roomcontrol>().SetAllRoomCamPointDisable();
            CamVC.enabled = true;

            if (!PlayerFirstEnter)
            {
                switch (RoomType)
                {
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
            //smallmap.color = Color.gray;
        }
    }
    public void tofalse()
    {
        PlayerIsInRoom = false;
    }
    public void OpenAllDoor()
    {
        foreach (GameObject i in door)
        {
            i.transform.Find("doorclose").gameObject.SetActive(false);
        }
    }
    IEnumerator CloseAllDoor()
    {
        yield return new WaitForSeconds(0.4f);
        foreach (GameObject i in door)
        {
            i.transform.Find("doorclose").gameObject.SetActive(true);
        }
    }

    public Vector3 GetRandomPointInRoom()
    {
        Vector3 point = new Vector3(Random.Range(transform.position.x - 8, transform.position.x + 8), Random.Range(transform.position.y - 4, transform.position.y + 4), 0);
        return point;
    }
    IEnumerator GenerateEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (SpawnEnenyAndNum i in SpawnEnemy)
        {
            for (int j = 0; j < i.enemyNum; j++)
            {
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
        Instantiate(rewardgod[Random.Range(0, rewardgod.Length)], transform.position, Quaternion.identity);
    }
    public void GenerateFairy()
    {
        Instantiate(fairy[Random.Range(0, 2)], transform.position, Quaternion.identity);
    }
}

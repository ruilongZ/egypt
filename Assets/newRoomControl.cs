using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class newRoomControl : MonoBehaviour
{
    [Header("贴图资源")]
    public Sprite bossclosetex;
    public Sprite bossopentex;
    public Sprite commonclosetex;
    public Sprite commonpentex;
    public Sprite rewardclosetex;
    public Sprite rewardopentex;
    public Sprite hiddenclosetex;
    public Sprite hiddenopentex;
    public Sprite shopclosetex;
    public Sprite shopopentex;

    [Space ]
    [Header("全局索引与生成记录")]
    public List<Vector3> takenpos = new List<Vector3>();
    public List<GameObject> allroom = new List<GameObject>();

    [Space]
    [Header("房间预制体")]
    public GameObject withdoor;
    public GameObject roomPrefab;

    [Space]
    [Header("生成各种房间数量控制")]
    public int commonroomNum = 10;
    [Tooltip("目前只能填1，多商店生成时可能会重叠")]
    public int shopNum = 1;
    public int rewardroomNum = 2;
    public int hiddenroomNum = 1;

    //位置补偿
    Vector3 rightroomPosOffset = new Vector3(19.2f, 0, 0);
    Vector3 uproomPosOffset = new Vector3(0, 10.8f, 0);

    void Awake()
    {
        GameObject startroom = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
        startroom.GetComponent<singleroom>().RoomType = singleroom.RoomKind.StartRoom;
        takenpos.Add(Vector3.zero);
    }
    private void Start()
    {
        StartCoroutine(detectroomAndspawnspecialroom(5));
    }
    //房间中生成门会自动调用
    public void SpawnRoomByDoorNameAndSpawnDoor(Transform RoomPos, string doorName)
    {
        switch (doorName)
        {
            case "dooruppos":
                spawnroom(RoomPos, uproomPosOffset,3);
                break;
            case "doorleftpos":
                spawnroom(RoomPos, -rightroomPosOffset,2);
                break;
            case "doorrightpos":
                spawnroom(RoomPos, rightroomPosOffset, 1);
                break;
            case "doordownpos":
                spawnroom(RoomPos, -uproomPosOffset, 0);
                break;
        }
    }

    //生成房间并给房间类型，把房间加入到房间数组中
    void spawnroom(Transform RoomPos, Vector3 offset, int spawnDoorPos)
    {
        int k = Random.Range(0, 3);
        if (commonroomNum > 0 || rewardroomNum > 0 || hiddenroomNum > 0)
        {
            GameObject spawnroom = Instantiate(roomPrefab, RoomPos.position + offset, Quaternion.identity);

            TranslateNumIntoRoomType(k, spawnroom);
            Transform doorgameobject = spawnroom.GetComponent<singleroom>().doorpos[spawnDoorPos].transform;

            GameObject instancedoor = Instantiate(withdoor, doorgameobject.position, doorgameobject.rotation);
            instancedoor.transform.parent = doorgameobject;
            changedoortex(spawnroom, instancedoor);
        }
    }
    //根据生成的房间类型，修改门的样式
    void changedoortex(GameObject room,GameObject instancedoor)
    {
        switch (room.GetComponent<singleroom>().RoomType) {
            case singleroom.RoomKind.EnemyRoom:
                break;
            case singleroom.RoomKind.RewardRoom:
                instancedoor.GetComponent<SpriteRenderer>().sprite = rewardopentex;
                instancedoor.transform.Find("doorclose").GetComponent<SpriteRenderer>().sprite = rewardclosetex;
                break;
            case singleroom.RoomKind.HiddenRoom:
                instancedoor.GetComponent<SpriteRenderer>().sprite =hiddenopentex;
                instancedoor.transform.Find("doorclose").GetComponent<SpriteRenderer>().sprite = hiddenclosetex;
                break;
        }
    }
    //随机给房间类型，并且减少可生成数量，如果数量没有自动转换
    singleroom.RoomKind TranslateNumIntoRoomType(int i, GameObject room)
    {
        switch (i)
        {
            case 0:
                if (commonroomNum > 0)
                {
                    room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.EnemyRoom;
                    commonroomNum--;
                }
                else
                {
                    if (rewardroomNum > 0)
                    {
                        room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.RewardRoom;
                        rewardroomNum--;
                    }
                    else
                    {
                        if (hiddenroomNum > 0)
                        {
                            room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.HiddenRoom;
                            hiddenroomNum--;
                        }
                    }
                }
                break;
            case 1:
                if (rewardroomNum > 0)
                {
                    room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.RewardRoom;
                    rewardroomNum--;
                }
                else
                {
                    if (commonroomNum > 0)
                    {
                        room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.EnemyRoom;
                        commonroomNum--;
                    }
                    else
                    {
                        if (hiddenroomNum > 0)
                        {
                            room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.HiddenRoom;
                            hiddenroomNum--;
                        }
                    }
                }
                break;
            case 2:
                if (hiddenroomNum > 0)
                {
                    room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.HiddenRoom;
                    hiddenroomNum--;
                }
                else
                {
                    if (commonroomNum > 0)
                    {
                        room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.EnemyRoom;
                        commonroomNum--;
                    }
                    else
                    {
                        if (rewardroomNum > 0)
                        {
                            room.GetComponent<singleroom>().RoomType = singleroom.RoomKind.RewardRoom;
                            rewardroomNum--;
                        }
                    }
                }
                break;
        }
        return room.GetComponent<singleroom>().RoomType;
    }

    #region 特殊房间list与数组声明
    [Header("符合特殊要求的房间")]
    List<GameObject> upCanSpawnroom = new List<GameObject>();
    List<GameObject> leftCanSpawnroom = new List<GameObject>();
    List<GameObject> rightCanSpawnroom = new List<GameObject>();
    List<GameObject> downCanSpawnroom = new List<GameObject>();

    GameObject[] upavailabelroom;
    GameObject[] leftavailabelroom;
    GameObject[] rightavailabelroom;
    GameObject[] downavailabelroom;

    GameObject choosenroom;
    #endregion

    //检测初始类型的房间是否都生成完毕
    IEnumerator detectroomAndspawnspecialroom(float waittime)
    {
        yield return new WaitForSeconds(waittime);

        findaroundroom();

        ToArry();

        StartCoroutine(Generatespecialroom(0.2f));
    }


    //生成特殊房间
    IEnumerator Generatespecialroom(float time)
    {
        yield return new WaitForSeconds(time);

        //生成商店
        for (int j = 0; j < shopNum; j++)
        {
            if (leftCanSpawnroom.Count == 0)
            {
                if (upCanSpawnroom.Count != 0)
                {
                    changedoorAndupdatespecialroom(upavailabelroom, singleroom.RoomKind.ShopRoom, uproomPosOffset, 3, 0, upCanSpawnroom);
                }
            }
            else
            {
                changedoorAndupdatespecialroom(leftavailabelroom, singleroom.RoomKind.ShopRoom, -rightroomPosOffset, 2, 1, leftCanSpawnroom);
            }
        }

        //生成boss房
        if (rightCanSpawnroom.Count == 0)
        {
            if (upCanSpawnroom.Count != 0)
            {
                changedoorAndupdatespecialroom(upavailabelroom, singleroom.RoomKind.BossRoom, uproomPosOffset, 3, 0, upCanSpawnroom);
            }
        }
        else
        {
            changedoorAndupdatespecialroom(rightavailabelroom, singleroom.RoomKind.BossRoom, rightroomPosOffset, 1, 2, rightCanSpawnroom);
        }

        foreach (GameObject j in allroom) {
            j.GetComponent<singleroom>().OpenAllDoor();
        }
    }


    //生成房间，改变特殊房间生成后的门与重新计算可用的特殊位置房间
    void changedoorAndupdatespecialroom(GameObject[] specialroomarry, singleroom.RoomKind specialroomkind, Vector3 offset, int thisdoorpos, int referroomdoorpos, List<GameObject> specialrooms)
    {
        //生成房间并赋予房间类型
        choosenroom = specialroomarry[Random.Range(0, specialroomarry.Length)];

        //更新对应可用特殊位置的房间
        takenpos.Add(choosenroom.transform.position + offset);
        specialrooms.Remove(choosenroom);
        specialroomarry = specialrooms.ToArray();

        GameObject specialroom = Instantiate(roomPrefab, choosenroom.transform.position + offset, Quaternion.identity);
        specialroom.GetComponent<singleroom>().RoomType = specialroomkind;



        //更换房门
        GameObject instancethisroomdoor = Instantiate(withdoor, specialroom.GetComponent<singleroom>().doorpos[thisdoorpos].transform.position, specialroom.GetComponent<singleroom>().doorpos[thisdoorpos].transform.rotation);
        instancethisroomdoor.transform.parent = specialroom.GetComponent<singleroom>().doorpos[thisdoorpos].transform;
        Destroy(choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform.GetChild(0).gameObject, 1f);
        GameObject instancereferroomdoor = Instantiate(withdoor, choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform.position, choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform.rotation);
        instancereferroomdoor.transform.parent = choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform;

        //更新单个房间里的门数组
        StartCoroutine(specialroom.GetComponent<singleroom>().updatedoors(1.2f));
        StartCoroutine(choosenroom.GetComponent<singleroom>().updatedoors(1.2f));


        if (specialroomkind==singleroom.RoomKind.BossRoom) {
            instancereferroomdoor.GetComponent<SpriteRenderer>().sprite = bossopentex;
            instancereferroomdoor.transform.Find("doorclose").GetComponent<SpriteRenderer>().sprite = bossclosetex;
        }
    }

    //list转array
    void ToArry()
    {
        upavailabelroom = upCanSpawnroom.ToArray();
        leftavailabelroom = leftCanSpawnroom.ToArray();
        rightavailabelroom = rightCanSpawnroom.ToArray();
        downavailabelroom = downCanSpawnroom.ToArray();
    }

    //自动寻找没门且可生成房间的房间
    void findaroundroom()
    {
        takenpos.Add(new Vector3(0, 10.8f, 0));
        takenpos.Add(new Vector3(0, -10.8f, 0));
        takenpos.Add(new Vector3(19.2f, 0, 0));
        takenpos.Add(new Vector3(-19.2f, 0, 0));
        //自动寻找上门没有的房间
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[0].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position + uproomPosOffset))
            {
                upCanSpawnroom.Add(i);
            }
        }

        //自动寻找左门没有的房间
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[1].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position - rightroomPosOffset))
            {
                leftCanSpawnroom.Add(i);
            }
        }

        //自动寻找右门没有的房间
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[2].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position + rightroomPosOffset))
            {
                rightCanSpawnroom.Add(i);
            }
        }

        //自动寻找下门没有的房间
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[3].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position - uproomPosOffset))
            {
                downCanSpawnroom.Add(i);
            }
        }
    }
}

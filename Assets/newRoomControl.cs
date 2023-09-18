using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class newRoomControl : MonoBehaviour
{
    [Header("��ͼ��Դ")]
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
    [Header("ȫ�����������ɼ�¼")]
    public List<Vector3> takenpos = new List<Vector3>();
    public List<GameObject> allroom = new List<GameObject>();

    [Space]
    [Header("����Ԥ����")]
    public GameObject withdoor;
    public GameObject roomPrefab;

    [Space]
    [Header("���ɸ��ַ�����������")]
    public int commonroomNum = 10;
    [Tooltip("Ŀǰֻ����1�����̵�����ʱ���ܻ��ص�")]
    public int shopNum = 1;
    public int rewardroomNum = 2;
    public int hiddenroomNum = 1;

    //λ�ò���
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
    //�����������Ż��Զ�����
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

    //���ɷ��䲢���������ͣ��ѷ�����뵽����������
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
    //�������ɵķ������ͣ��޸��ŵ���ʽ
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
    //������������ͣ����Ҽ��ٿ������������������û���Զ�ת��
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

    #region ���ⷿ��list����������
    [Header("��������Ҫ��ķ���")]
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

    //����ʼ���͵ķ����Ƿ��������
    IEnumerator detectroomAndspawnspecialroom(float waittime)
    {
        yield return new WaitForSeconds(waittime);

        findaroundroom();

        ToArry();

        StartCoroutine(Generatespecialroom(0.2f));
    }


    //�������ⷿ��
    IEnumerator Generatespecialroom(float time)
    {
        yield return new WaitForSeconds(time);

        //�����̵�
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

        //����boss��
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


    //���ɷ��䣬�ı����ⷿ�����ɺ���������¼�����õ�����λ�÷���
    void changedoorAndupdatespecialroom(GameObject[] specialroomarry, singleroom.RoomKind specialroomkind, Vector3 offset, int thisdoorpos, int referroomdoorpos, List<GameObject> specialrooms)
    {
        //���ɷ��䲢���跿������
        choosenroom = specialroomarry[Random.Range(0, specialroomarry.Length)];

        //���¶�Ӧ��������λ�õķ���
        takenpos.Add(choosenroom.transform.position + offset);
        specialrooms.Remove(choosenroom);
        specialroomarry = specialrooms.ToArray();

        GameObject specialroom = Instantiate(roomPrefab, choosenroom.transform.position + offset, Quaternion.identity);
        specialroom.GetComponent<singleroom>().RoomType = specialroomkind;



        //��������
        GameObject instancethisroomdoor = Instantiate(withdoor, specialroom.GetComponent<singleroom>().doorpos[thisdoorpos].transform.position, specialroom.GetComponent<singleroom>().doorpos[thisdoorpos].transform.rotation);
        instancethisroomdoor.transform.parent = specialroom.GetComponent<singleroom>().doorpos[thisdoorpos].transform;
        Destroy(choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform.GetChild(0).gameObject, 1f);
        GameObject instancereferroomdoor = Instantiate(withdoor, choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform.position, choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform.rotation);
        instancereferroomdoor.transform.parent = choosenroom.GetComponent<singleroom>().doorpos[referroomdoorpos].transform;

        //���µ����������������
        StartCoroutine(specialroom.GetComponent<singleroom>().updatedoors(1.2f));
        StartCoroutine(choosenroom.GetComponent<singleroom>().updatedoors(1.2f));


        if (specialroomkind==singleroom.RoomKind.BossRoom) {
            instancereferroomdoor.GetComponent<SpriteRenderer>().sprite = bossopentex;
            instancereferroomdoor.transform.Find("doorclose").GetComponent<SpriteRenderer>().sprite = bossclosetex;
        }
    }

    //listתarray
    void ToArry()
    {
        upavailabelroom = upCanSpawnroom.ToArray();
        leftavailabelroom = leftCanSpawnroom.ToArray();
        rightavailabelroom = rightCanSpawnroom.ToArray();
        downavailabelroom = downCanSpawnroom.ToArray();
    }

    //�Զ�Ѱ��û���ҿ����ɷ���ķ���
    void findaroundroom()
    {
        takenpos.Add(new Vector3(0, 10.8f, 0));
        takenpos.Add(new Vector3(0, -10.8f, 0));
        takenpos.Add(new Vector3(19.2f, 0, 0));
        takenpos.Add(new Vector3(-19.2f, 0, 0));
        //�Զ�Ѱ������û�еķ���
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[0].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position + uproomPosOffset))
            {
                upCanSpawnroom.Add(i);
            }
        }

        //�Զ�Ѱ������û�еķ���
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[1].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position - rightroomPosOffset))
            {
                leftCanSpawnroom.Add(i);
            }
        }

        //�Զ�Ѱ������û�еķ���
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[2].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position + rightroomPosOffset))
            {
                rightCanSpawnroom.Add(i);
            }
        }

        //�Զ�Ѱ������û�еķ���
        foreach (GameObject i in allroom)
        {
            if (i.GetComponent<singleroom>().doorpos[3].transform.Find("withoutdoor(Clone)") && !takenpos.Contains(i.transform.position - uproomPosOffset))
            {
                downCanSpawnroom.Add(i);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicdetect : MonoBehaviour
{
    public GameObject musiccontrol;
    int roomkind;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (roomkind) {
            case 1:
                musiccontrol.GetComponent<musicontrol>().entercommon();
                break;
            case 2:
                musiccontrol.GetComponent<musicontrol>().enterboss();
                break;
            case 3:
                musiccontrol.GetComponent<musicontrol>().entercommon();
                break;
            case 4:
                musiccontrol.GetComponent<musicontrol>().entergod();
                break;
            case 5:
                musiccontrol.GetComponent<musicontrol>().entershop();
                break;
            case 6:
                musiccontrol.GetComponent<musicontrol>().entersecret();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="room") {
            switch (other.GetComponent<singleroom>().RoomType) {
                case singleroom.RoomKind.EnemyRoom:
                    roomkind = 1;
                    break;
                case  singleroom.RoomKind.BossRoom:
                    roomkind = 2;
                    break;
                case  singleroom.RoomKind.StartRoom:
                    roomkind = 3;
                    break;
                case  singleroom.RoomKind.RewardRoom:
                    roomkind = 4;
                    break;
                case  singleroom.RoomKind.ShopRoom:
                    roomkind = 5;
                    break;
                case  singleroom.RoomKind.HiddenRoom:
                    roomkind = 6;
                    break;
            }
        }
    }
}

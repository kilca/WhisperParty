using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingMenu : Photon.PunBehaviour
{

    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomListing _roomListing;

    [SerializeField]
    private List<RoomListing> roomList=  new List<RoomListing>();

    void Start() {
        OnReceivedRoomListUpdate();
    }


    public void Refresh() {
        foreach (RoomListing l in roomList) {
            Destroy(l.gameObject);
        }

        roomList = new List<RoomListing>();
        OnReceivedRoomListUpdate();
    }

    public override void OnReceivedRoomListUpdate()
    {

        foreach (RoomInfo info in PhotonNetwork.GetRoomList()) {


            int index = roomList.FindIndex(x => x.roomInfo.Name == info.Name);
            if (info.removedFromList) {
                if (index != -1) {
                    Destroy(roomList[index].gameObject);
                    roomList.RemoveAt(index);
                    continue;
                }
            }

            if (!info.IsVisible)
                continue;

            if (index == -1)
            {
                RoomListing listing = Instantiate(_roomListing, _content);
                if (listing != null)
                {
                    roomList.Add(listing);
                    listing.SetRoomInfo(info);
                }
            }
            else {
                roomList[index].UpdateRoom();
            }
        }
        base.OnReceivedRoomListUpdate();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public RoomInfo roomInfo;

    void Start() {
        GetComponent<Button>().onClick.AddListener( ()=>tryJoinRoom());
    }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        this.roomInfo = roomInfo;

        _text.text = roomInfo.PlayerCount+"/"+roomInfo.MaxPlayers + " : " + roomInfo.Name;
    }

    public void UpdateRoom() {
        _text.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + " : " + roomInfo.Name;
    }

    private void tryJoinRoom() {
        photonHandler._instance.joinOrCreateRoom(roomInfo.Name);
    }
}

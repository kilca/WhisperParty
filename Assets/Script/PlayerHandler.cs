using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Photon.PunBehaviour
{

    public MultCreate multCreate;

    public byte ownerId;

    public byte myTextId;
    public byte currentTextId;

    void Start()
    {
        multCreate = MultCreate._instance;
        if (!photonView.isMine)
            return;

        PhotonNetwork.player.NickName = PlayerPrefs.GetString("Nickname");

        ownerId = (byte) photonView.ownerId;

        multCreate.playerInstance = this;

        if (photonView == null)
        {
            Debug.LogError("some value are null");
        }

        PhotonNetwork.player.SetScore(0);

        myTextId = (byte)photonView.ownerId;
        currentTextId = (byte)photonView.ownerId;

        multCreate.UpdateId();

    }

    [PunRPC]
    public void SendEdited()
    {
        //Debug.Log("we send the edited");

        if (!photonView.isMine)
            return;

        myTextId++;
        if (myTextId > PhotonNetwork.room.PlayerCount)
        {
            myTextId = 0;
        }

        byte newIdNumber = (byte)(photonView.ownerId + 1);
        if (newIdNumber > PhotonNetwork.room.PlayerCount)
        {
            newIdNumber = 1;
        }

        currentTextId -= 1;
        if (currentTextId <= 0)
        {
            currentTextId = (byte)PhotonNetwork.room.PlayerCount;
        }

        multCreate.UpdateMode(newIdNumber);

    }

    //------------------------------------------------------


    [PunRPC]
    public void SaveIMG(byte[] receivedByte, PhotonMessageInfo info)
    {
        //Debug.Log("get img");
        /*
        if (!photonView.isMine)
            return;
        */
        multCreate.SaveIMG(receivedByte, info.photonView.ownerId);
    }

    [PunRPC]
    public void GetIMG(byte[] receivedByte, PhotonMessageInfo info)
    {
        //Debug.Log("get img");
        if (!photonView.isMine)
            return;

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(receivedByte);

        multCreate.showHand.SetSprite(tex);

    }

    ///---------------------------TEXT ----------------
    [PunRPC]
    public void GetText(string receivedText, PhotonMessageInfo info)
    {
        
        if (!photonView.isMine)
            return;
        

        if (receivedText == null || receivedText.Equals(""))
            return;

        //Debug.Log("get text :" + receivedText);

        multCreate.showHand.SetText(receivedText);

    }



    [PunRPC]
    public void SaveText(string receivedText, PhotonMessageInfo info)
    {

        //Debug.Log(info.photonView.ownerId);
        /*
        if (!photonView.isMine)
            return;
        */    
        multCreate.SaveText(receivedText, info.photonView.ownerId);


    }

}

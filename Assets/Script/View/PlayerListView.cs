using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListView : MonoBehaviour
{
    public InputField nameField;
    public Text idText;
    public GameObject playerList;

    public GameObject playerNamePrefab;

    

    void Start() {
        InvokeRepeating("UpdatePlayerList", 5.0f, 0.25f);
        nameField.text = PlayerPrefs.GetString("Nickname");
    }

    public void ChangeName(string s) {
        PhotonNetwork.player.NickName = nameField.text;
        PlayerPrefs.SetString("Nickname",nameField.text);
    }

    public void ClickList() {
        playerList.SetActive(!playerList.activeSelf);
    }

    //WARNING DO NOT HANDLE DISCONNECT

    private void UpdatePlayerList() {


        PhotonPlayer[] plist = PhotonNetwork.playerList;
        for (int i = 0; i < plist.Length; i++) {
            if (plist.Length < i)
                Destroy(playerList.transform.GetChild(i).gameObject);

            string isReady = " ";

            if (plist[i].GetScore() == 1)
                isReady += "X";

            Transform t;

            if (playerList.transform.childCount <= i)
            {
                GameObject g = Instantiate(playerNamePrefab, playerList.transform);
                t = g.transform;
            }
            else {
                t = playerList.transform.GetChild(i);
            }

            t.GetComponent<Text>().text =
                "[" + plist[i].ID + "] " + plist[i].NickName + isReady;

        }



        

    }

}

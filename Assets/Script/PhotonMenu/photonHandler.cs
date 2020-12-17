using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class photonHandler : MonoBehaviour {

    public static bool hasBegan = false;
    public static photonHandler _instance;


    public PhotonButtons photonB;
    public GameObject mainPlayer;

    private const int maxPlayer = 12;

    public Text existRoomText;

    private void Awake() {

        if (_instance == null) {
            _instance = this;
        }

        //DontDestroyOnLoad(this.transform);
        if (!hasBegan)
        {
            SceneManager.sceneLoaded += OnSceneFinishedLoading;//ATSTER
            hasBegan = true;
        }
    }

    public void createNewRoom(bool visible) {

        string roomName = photonB.createRoomInput.text;

        if (roomName == null || roomName == "")
            return;

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].Name.Equals(roomName))
            {
                existRoomText.gameObject.SetActive(true);
                return;
            }
        }

        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = maxPlayer,IsVisible = visible }, null);
    }


    public void joinOrCreateRoom() {
        string name = photonB.joinRoomInput.text;

        if (name == null || name == "")
            return;

        PhotonNetwork.JoinRoom(photonB.joinRoomInput.text);
    }

    public void joinOrCreateRoom(string name)
    {
        if (name == null || name == "")
            return;
        PhotonNetwork.JoinRoom(name);
    }

    public void moveScene() {
        photonB = null;//FAIRE GAFFE MODIFIE APRES

        PhotonNetwork.LoadLevel("Jeu");
    }

    private void OnJoinedRoom()
    {
        Debug.Log("We are connected to the room");
        moveScene();
        //PhotonNetwork.LoadLevel("Jeu");


    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Jeu") {
            spawnPlayer();
        }

    }

    private void spawnPlayer() {
        //print(mainPlayer.name);
        PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation , 0);

    }

}

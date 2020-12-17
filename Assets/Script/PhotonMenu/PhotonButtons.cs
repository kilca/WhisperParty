using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonButtons : MonoBehaviour {

    public photonHandler pHandler;

    public InputField createRoomInput, joinRoomInput;

    public Toggle visibleToggle;

    public void onClickCreateRoom() {
        pHandler.createNewRoom(visibleToggle.isOn);
    }

    public void onClickJoinRoom() {
        pHandler.joinOrCreateRoom();
    }

    public void onClickBackMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

  


}

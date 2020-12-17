using FreeDraw;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//public class MultCreate : Photon.MonoBehaviour
public class MultCreate : MonoBehaviour
{

    public static MultCreate _instance;
    public PlayerHandler playerInstance;

    public enum ShowMode { Text, Sprite, Watch };
    public ShowMode mode = ShowMode.Text;

    public bool isValidated = false;

    public bool isStarted = false;

    public byte myId;
    public byte myTextId;//id of the player who got your text
    public byte currentTextId;//id of the player where your text come from

    public byte round = 1;

    public Drawable drawable;
    public ShowHandler showHand;
    public PlayerListView playerView;
    public SliderSave sliderSave;

    [Header("Transform")]
    public GameObject _transformSprite;
    public GameObject _transformText;
    public Transform _transformSaved;

    [Header("From Components")]
    public SpriteRenderer _fromSprite;
    public InputField _fromText;
    public InputField _fromTextMobile;


    [Header("Idea Prefab")]
    public GameObject _textIdea;
    public GameObject _imageIdea;


    public Transform showParent;

    public GameObject gridSavePrefab;

    [Space(2)]
    public Toggle infoCheck;

    void Awake()
    {
        if (keyboardClass.isMobile()) {
            _fromText.gameObject.SetActive(false);
            _fromTextMobile.gameObject.SetActive(true);
            _fromText = _fromTextMobile;
        }

        Debug.Log("call awake");
        _instance = this;
    }

    public void ClickShowEdit() {
        showParent.gameObject.SetActive(!showParent.gameObject.activeSelf);
    }

    public void ClickReturnMenu() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void EndGame() {

        sliderSave.SetMax(PhotonNetwork.room.PlayerCount);
        sliderSave.ChangeValue();

        mode = ShowMode.Watch;

        _transformSaved.gameObject.SetActive(true);

        _transformText.SetActive(false);
        _transformSprite.SetActive(false);

        showHand._image.gameObject.SetActive(false);
        showHand._text.gameObject.SetActive(false);

    }

    public void UpdateId() {
        myId = playerInstance.ownerId;

        myTextId = playerInstance.myTextId;
        currentTextId = playerInstance.currentTextId;
    }



    public void UpdateMode(byte newIdNumber) {

        if (mode == ShowMode.Watch)
            return;

        if (mode == ShowMode.Sprite)
        {
            mode = ShowMode.Text;
            SendIMG(newIdNumber);
            _transformText.SetActive(true);
            _transformSprite.SetActive(false);

            showHand._image.gameObject.SetActive(true);
            showHand._text.gameObject.SetActive(false);

            drawable.ResetCanvas();
        }

        else if (mode == ShowMode.Text)
        {
            mode = ShowMode.Sprite;

            SendText(newIdNumber);

            _transformText.SetActive(false);
            _transformSprite.SetActive(true);

            showHand._image.gameObject.SetActive(false);
            showHand._text.gameObject.SetActive(true);

            _fromText.text = "";

        }

        UpdateId();
        round++;

        if (round == (PhotonNetwork.playerList.Length+1))
        {
            EndGame();
            return;
        }

        ClickValidate();

    }

    private void CheckAllReady()
    {

        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            if (p.GetScore() == 0)
                return;
        }

        Debug.Log("we assume all ready");

        if (!isStarted)
        {
            isStarted = true;
            //Fermer acces jeu
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
            g.GetComponent<PlayerHandler>().photonView.RPC("SendEdited", PhotonTargets.All);
        }

    }

    public void ClickValidate()
    {

        isValidated = !isValidated;

        infoCheck.isOn = isValidated;

        if (isValidated)
        {
            PhotonNetwork.player.SetScore(1);
            CheckAllReady();
        }
        else
        {
            PhotonNetwork.player.SetScore(0);
        }


    }

    ///------------------IMG---------------------------

    public void SendIMG(byte idTo)
    {
        byte[] bs = _fromSprite.sprite.texture.EncodeToJPG();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = g.GetComponent<PlayerHandler>().photonView;
            if (pv.ownerId == currentTextId)
                pv.RPC("SaveIMG", PhotonTargets.All, bs);

            if (pv.ownerId == idTo)
            {
                pv.RPC("GetIMG", PhotonTargets.All, bs);
            }

        }

    }

    ///---------------------------TEXT ----------------

    public void SendText(byte idTo)
    {

        string s = _fromText.text;

        //Debug.Log("we send the text :" + s+", as :"+ myId+ ", to"+ idTo);


        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = g.GetComponent<PlayerHandler>().photonView;

            if (pv.ownerId == currentTextId)
                pv.RPC("SaveText", PhotonTargets.All, s);

            if (pv.ownerId == idTo) {
                pv.RPC("GetText", PhotonTargets.All, s);
            }

        }
    }

    public void SaveIMG(byte[] receivedByte, int id) {

        Transform par;
        if (_transformSaved.childCount <= id)
        {
            for (int i = 1; i <= id; i++)
            {
                Instantiate(gridSavePrefab, _transformSaved);
            }
            par = _transformSaved.GetChild(id);
        }
        else
        {
            par = _transformSaved.GetChild(id);
        }

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(receivedByte);

        GameObject g = Instantiate(_imageIdea, par, false);
        g.GetComponent<Image>().sprite =
            Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }


    public void SaveText(string receivedText, int id)
    {
        Transform par;
        if (_transformSaved.childCount <= id)
        {
            for (int i = 1; i <= id; i++)
            {
               Instantiate(gridSavePrefab, _transformSaved);
            }
            par = _transformSaved.GetChild(id);
        }else
        {
            par = _transformSaved.GetChild(id);
        }

        GameObject g = Instantiate(_textIdea, par, false);
        g.transform.GetChild(0).GetComponent<Text>().text = receivedText;
    }

}
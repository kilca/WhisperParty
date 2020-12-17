using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text;

public class SpriteView : MonoBehaviour
{

    public enum ShowMode { Text,Sprite,Watch};
    public ShowMode mode = ShowMode.Text;

    private PhotonView m_view;

    public SpriteRenderer _from;
    public SpriteRenderer _to;

    // Start is called before the first frame update
    void Start()
    {
        m_view = GetComponent<PhotonView>();

        if (_to == null || m_view == null) {
            Debug.LogError("some value are null");
        }
    }

    public void clickButton() {

        byte[] bs = _from.sprite.texture.EncodeToJPG();

        m_view.RPC("AfficheIMG", PhotonTargets.Others,bs);

    }

    [PunRPC]
    public void AfficheIMG(byte[] receivedByte)
    {

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(receivedByte);

        _to.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);

    }

    /*

    private void AddObservable()
    {
        if (!m_view.ObservedComponents.Contains(this))
        {
            m_view.ObservedComponents.Add(this);
        }
    }

    private void Update()
    {
        if (!m_view.isMine) return;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("we view");
        if (stream.isWriting)
        {
            stream.SendNext(m_spriteRenderer.sprite.texture);
        }
        else
        {
            Texture2D text = (Texture2D)stream.ReceiveNext();
            m_spriteRenderer.sprite = Sprite.Create(text, m_spriteRenderer.sprite.rect, m_spriteRenderer.sprite.pivot);
        }
    }
    */
}

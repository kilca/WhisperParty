using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDraw : MonoBehaviour
{
    public PhotonView m_view;
    public List<Sprite> sprites;

    public SpriteRenderer sprite;

    public int id;

    void Start() {

        if (m_view.isMine) {
            id = PhotonNetwork.countOfPlayers-1;
            if (id < 0)
                id = 0;

            sprite.sprite = sprites[id];

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class ShowHandler : MonoBehaviour
{
    public Text _text;
    public Image _image;

    public void CleanSprite() {
        Texture2D tex = new Texture2D(400, 400);
        _image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    public void CleanText() {
        _text.text = "";
    }

    public void SetSprite(Texture2D tex) {

        _image.sprite =
            Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    public void SetText(string s) {
        _text.text = s;
    }

}

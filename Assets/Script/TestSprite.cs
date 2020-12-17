using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSprite : MonoBehaviour
{

    public SpriteRenderer from;
    public SpriteRenderer to;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture = from.sprite.texture;
        Sprite sprite = Sprite.Create(texture, to.sprite.rect, Vector2.zero);
        to.sprite = sprite;
    }

}

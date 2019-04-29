using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Sprite sp;

    Image im;

    public void SpriteChange()
    {
        im = this.GetComponent<Image>();

        var tmp = sp;
        sp = im.sprite;
        im.sprite = tmp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Sprite spOn;
    public Sprite spOff;

    Image im;
    public GameScriptableObject obj;

    void Start()
    {
        im = this.GetComponent<Image>();
        SpriteChange();
    }

    void OnEnable()
    {
        Start();
    }

    public void SpriteChange()
    {
        if (obj.muted) //change to mute.
        {
            im.sprite = spOn;
        }
        else im.sprite = spOff;
    }
}

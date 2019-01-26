using System;
using UnityEngine;

[Serializable]
public class ActionData
{
    public Color color;
    public Sprite sprite;
    public GameObject skillbit;

    public ActionData(Color col, Sprite sp, GameObject obj)
    {
        this.color = col;
        this.sprite = sp;
        this.skillbit = obj;
    }
}

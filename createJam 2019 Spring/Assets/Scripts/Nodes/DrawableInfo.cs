using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawableInfo
{
    public string title;
    protected float height = 120f;
    public GUIStyle style;

    public abstract void Draw(Rect rect, GUIStyle style);
    public abstract float GetHeight();
}


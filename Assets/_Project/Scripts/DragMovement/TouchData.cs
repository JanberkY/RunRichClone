using UnityEngine;

public class TouchData
{
    public float Verticle, Horizontal;

    public Vector2 FirstPos;
    public Vector2 CurrentPos;
}

public enum ClickControl
{
    Empty, Down, Set, Up
}

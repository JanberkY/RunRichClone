using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DragController : IHyperTouch
{
    public event Action<TouchData> DownClick;
    public event Action<TouchData> UpClick;
    public event Action<TouchData> SetClick;


    private TouchData _touchData;
    private ClickControl _clickControl = ClickControl.Empty;


    [HorizontalLine, Range(0.01f, 0.99f)]
    public float DragLerpRange = 0.1f;

    public DragController()
    {
        _touchData = new TouchData();
    }
    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchData.FirstPos = Input.mousePosition;
            Assign(ref DownClick);

            _clickControl = ClickControl.Down;
        }

        else if (Input.GetMouseButton(0))
        {
            _clickControl = ClickControl.Set;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            Assign(ref UpClick);

            _clickControl = ClickControl.Up;
        }
    }
    public void Handle()
    {
        switch (_clickControl)
        {
            case ClickControl.Set:
                _touchData.CurrentPos = Input.mousePosition;

                var data = Mathf.Clamp(DragLerpRange, 0.01f, 0.99f);

                _touchData.FirstPos = Vector2.Lerp(_touchData.FirstPos, _touchData.CurrentPos, data);

                var t = (_touchData.CurrentPos - _touchData.FirstPos) * Time.deltaTime;

                _touchData.Verticle = t.y;
                _touchData.Horizontal = t.x;
                Assign(ref SetClick);
                break;
        }
    }
    private void Assign(ref Action<TouchData> action)
    {
        action?.Invoke(_touchData);
    }
}

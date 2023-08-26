using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : JSingleton<TouchManager>
{
    private GameManager _gameManager;
    private IHyperTouch _touch;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _touch = new DragController();
    }
    private void Update()
    {
        if (_touch == null)
            return;

        if (!_gameManager.GetPlayable())
            return;

        _touch.Click();
    }
    private void FixedUpdate()
    {
        if (_touch == null)
            return;

        if (!_gameManager.GetPlayable())
            return;

        _touch.Handle();
    }

    public IHyperTouch ClickData()
    {
        if (_touch == null)
            Debug.LogError("there is no referance in \"touch\" ");

        return _touch;
    }
}

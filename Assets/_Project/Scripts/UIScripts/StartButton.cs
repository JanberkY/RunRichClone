using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
    }
    public void TapToStart()
    {
        _gameManager.SetPlayable(true);
        _uiManager.OpenPanel(0);
        _uiManager.ClosePanel(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    private UIManager _uiManager;
    private LevelManager _levelManager;

    private void Start()
    {
        _uiManager = UIManager.Instance;
        _levelManager = LevelManager.Instance;
    }
    public void ContinueButtonOperation()
    {
        _uiManager.ClosePanel(2);
        _levelManager.NextLevel();
    }
}

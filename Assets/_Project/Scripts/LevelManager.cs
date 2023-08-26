using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : JSingleton<LevelManager>
{
    private DataManager _dataManager;
    private UIManager _uiManager;

    [SerializeField]
    private List<GameObject> _levelList;

    private GameObject _tempObj;

    private void Start()
    {
        _dataManager = DataManager.Instance;
        _uiManager = UIManager.Instance;
        LevelGenerator();
    }

    public void LevelGenerator()
    {
        _tempObj = Instantiate(_levelList[_dataManager.GetLevel()]);
    }
    public void NextLevel()
    {
        _uiManager.OpenPanel(1);
        _tempObj.SetActive(false);

        if (_dataManager.GetLevel() == _levelList.Count - 1)
        {
            _dataManager.ClearLevel();
            _tempObj = Instantiate(_levelList[_dataManager.GetLevel()]);
        }
        else
        {
            _dataManager.SetLevel();
            _tempObj = Instantiate(_levelList[_dataManager.GetLevel()]);
        }
    }
}

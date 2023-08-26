using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : JSingleton<DataManager>
{
    private const string _moneyKey = "MoneyKey";
    private const string _levelKey = "LevelKey";
    private const string _positiveBarValueKey = "PositiveBar";
    private const string _negativeBarValueKey = "NegativeBar";
    private const string _positiveDoorValueKey = "PositiveDoor";
    private const string _negativeDoorValueKey = "NegativeDoor";
    private const string _moneyIncreaseValueKey = "MoneyIncrease";

    [SerializeField]
    private int _moneyIncreaseValue;

    [SerializeField]
    private float _positiveBarValue;

    [SerializeField]
    private float _negativeBarValue;

    [SerializeField]
    private float _positiveDoorValue;

    [SerializeField]
    private float _negativeDoorValue;

    public void SetMoney(int x)
    {
        PlayerPrefs.SetInt(_moneyKey, GetMoney() + x);
    }
    public int GetMoney()
    {
        return PlayerPrefs.GetInt(_moneyKey, 100);
    }
    public int GetPositiveValue()
    {
        return PlayerPrefs.GetInt(_moneyIncreaseValueKey, 5);
    }
    public float GetPositiveBarValue()
    {
        return PlayerPrefs.GetInt(_positiveBarValueKey, 5);
    }
    public float GetNegativeBarValue()
    {
        return PlayerPrefs.GetInt(_negativeBarValueKey, 5);
    }
    public float GetPositiveDoorValue()
    {
        return PlayerPrefs.GetInt(_positiveDoorValueKey, 10);
    }
    public float GetNegativeDoorValue()
    {
        return PlayerPrefs.GetInt(_negativeDoorValueKey, 10);
    }
    public void SetLevel()
    {
        PlayerPrefs.SetInt(_levelKey, GetLevel() + 1);
    }
    public int GetLevel()
    {
        return PlayerPrefs.GetInt(_levelKey, 0);
    }
    public void ClearLevel()
    {
        PlayerPrefs.SetInt(_levelKey, 0);
    }
}

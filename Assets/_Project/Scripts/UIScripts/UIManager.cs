using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : JSingleton<UIManager>
{
    private DataManager _dataManager;
    private const string _poorText = "POOR";
    private const string _averageText = "AVERAGE";
    private const string _richText = "RICH";

    [SerializeField]
    private List<GameObject> _panelList;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Image _fillArea;

    [SerializeField]
    private TextMeshProUGUI _typeText;

    [SerializeField]
    private Color _poorColor;

    [SerializeField]
    private Color _averageColor;

    [SerializeField]
    private Color _richColor;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    private void Start()
    {
        _dataManager = DataManager.Instance;
        StartOperation();
    }
    private void StartOperation()
    {
        _slider.value = 0;
        _fillArea.color = _poorColor;
        _typeText.text = _poorText;
        _typeText.color = _poorColor;
        SetMoneyText(_dataManager.GetMoney());
    }
    public void SetTypeText()
    {
        if (GetSliderValue() >= 0 && GetSliderValue() <= 35)
        {
            _typeText.text = _poorText;
            _typeText.color = _poorColor;
            _fillArea.color = _poorColor;
        }
        if (GetSliderValue() > 35 && GetSliderValue() <= 70)
        {
            _typeText.text = _averageText;
            _typeText.color = _averageColor;
            _fillArea.color = _averageColor;
        }
        if (GetSliderValue() > 70 && GetSliderValue() <= 100)
        {
            _typeText.text = _richText;
            _typeText.color = _richColor;
            _fillArea.color = _richColor;
        }
    }
    public void SetSliderValue(float z, bool tf)
    {
        if (GetSliderValue() - z >= 0 && !tf)
            _slider.value -= z;

        if (GetSliderValue() + z <= 100 && tf)
            _slider.value += z;

        SetTypeText();
    }
    public void SetMoneyText(int x)
    {
        _moneyText.text = x.ToString();
    }
    public float GetSliderValue()
    {
        return _slider.value;
    }

    public void OpenPanel(int panelIndex)
    {
        _panelList[panelIndex].SetActive(true);
    }
    public void ClosePanel(int panelIndex)
    {
        _panelList[panelIndex].SetActive(false);
    }
    public void CloseAllPanel()
    {
        for (int i = 0; i < _panelList.Count; i++)
        {
            _panelList[i].SetActive(false);
        }
    }
}

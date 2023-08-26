using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Dreamteck.Splines;
using NaughtyAttributes;

public partial class CharacterController : MonoBehaviour
{
    private TouchManager _touchManager;
    private DataManager _dataManager;
    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private float _leftRightSpeed;

    [SerializeField]
    private Transform _myChild;

    [SerializeField]
    private Vector3 _clampOffSet;

    [SerializeField]
    private List<GameObject> _characterTypeList;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SplineFollower _follower;

    [SerializeField]
    private CharacterType _characterType;

    [SerializeField]
    private List<ParticleSystem> _particleList;

    [SerializeField]
    private Transform _finalPoint;

    [SerializeField]
    private CameraController _camera;

    private Vector3 _tempPos;

    private bool _isStart;

    private void Start()
    {
        _touchManager = TouchManager.Instance;
        _dataManager = DataManager.Instance;
        _uiManager = UIManager.Instance;
        _gameManager = GameManager.Instance;

        StartCoroutine(TouchInitialize());

        _isStart = false;
    }
    private IEnumerator TouchInitialize()
    {
        yield return new WaitForSeconds(0.5f);

        _touchManager.ClickData().DownClick -= Down;
        _touchManager.ClickData().SetClick -= Set;
        _touchManager.ClickData().UpClick -= Up;

        _touchManager.ClickData().DownClick += Down;
        _touchManager.ClickData().SetClick += Set;
        _touchManager.ClickData().UpClick += Up;
    }
    private void Update()
    {
        Clamp();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            MoneyControl();
            CharacterTextControl(true);
            CharacterTypeControl();
            _particleList[2].Play();
        }
        if (other.CompareTag("PositiveDoor"))
        {
            DoorControl(true);
            CharacterTypeControl();
            _particleList[4].Play();
        }
        if (other.CompareTag("NegativeDoor"))
        {
            DoorControl(false);
            CharacterTypeControl();
            _particleList[5].Play();
        }
        if (other.CompareTag("Obstacle"))
        {
            CharacterTextControl(false);
            CharacterTypeControl();
            _particleList[3].Play();
        }
        if (other.CompareTag("Finish"))
            FinalOperation();
    }
    private void FixedUpdate()
    {
        if (!_isStart && _gameManager.GetPlayable())
        {
            SetCharacterSpeed(5);
            PoorWalk();
            _isStart = true;
        }
    }
    private void Down(TouchData obj)
    {
        if (!_gameManager.GetPlayable())
            return;
    }
    private void Set(TouchData obj)
    {
        _myChild.localPosition += new Vector3(obj.Horizontal, 0, 0) * _leftRightSpeed;
    }
    private void Up(TouchData obj)
    {
        
    }
    private void Clamp()
    {
        _tempPos = _myChild.localPosition;
        _tempPos.x = Mathf.Clamp(_tempPos.x, -_clampOffSet.x, _clampOffSet.x);
        _myChild.localPosition = _tempPos;
    }
    private void MoneyControl()
    {
        _dataManager.SetMoney(_dataManager.GetPositiveValue());
        _uiManager.SetMoneyText(_dataManager.GetMoney());
    }
    private void DoorControl(bool tf)
    {
        if (tf)
            _uiManager.SetSliderValue(_dataManager.GetPositiveDoorValue(), tf);
        else
            _uiManager.SetSliderValue(_dataManager.GetNegativeDoorValue(), tf);
    }
    private void CharacterTextControl(bool tf)
    {
        if (tf)
            _uiManager.SetSliderValue(_dataManager.GetPositiveBarValue(), tf);
        else
            _uiManager.SetSliderValue(_dataManager.GetNegativeBarValue(), tf);
    }
    private void CharacterTypeControl()
    {
        if (_uiManager.GetSliderValue() >= 0 && _uiManager.GetSliderValue() <= 35 && _characterType != CharacterType.Poor)
            CharacterListOperation(0);
        if (_uiManager.GetSliderValue() > 35 && _uiManager.GetSliderValue() <= 70 && _characterType != CharacterType.Normal)
            CharacterListOperation(1);
        if (_uiManager.GetSliderValue() > 70 && _uiManager.GetSliderValue() <= 100 && _characterType != CharacterType.Rich)
            CharacterListOperation(2);
    }
    private void CharacterListOperation(int x)
    {
        if (x == 0)
        {
            Turn(1);
            _characterTypeList[x].SetActive(true);
            _characterTypeList[x + 1].SetActive(false);
            PoorWalk();
            _characterType = CharacterType.Poor;
        }
        if (x == 1)
        {
            if (_characterType == CharacterType.Rich)
            {
                Turn(1);
                _characterTypeList[x].SetActive(true);
                _characterTypeList[x + 1].SetActive(false);
                AverageWalk();
                _characterType = CharacterType.Normal;
            }
            else
            {
                Turn(0);
                _characterTypeList[x].SetActive(true);
                _characterTypeList[x - 1].SetActive(false);
                AverageWalk();
                _characterType = CharacterType.Normal;
            }

        }
        if (x == 2)
        {
            Turn(0);
            _characterTypeList[x].SetActive(true);
            _characterTypeList[x - 1].SetActive(false);
            RichWalk();
            _characterType = CharacterType.Rich;
        }
    }
    private void FinalOperation()
    {
        SetCharacterSpeed(0);
        _gameManager.SetPlayable(false);
        _uiManager.ClosePanel(0);
        _camera.FinalPosition();

        var pos = new Vector3(_finalPoint.position.x, 1.18f, _finalPoint.position.z);
        var rot = Quaternion.Euler(new Vector3(0, 180, 0));

        if (_uiManager.GetSliderValue() > 35)
            transform.DOMove(pos, 0.5f).OnComplete(() => 
            {
                transform.DOLocalRotateQuaternion(rot, 0.2f).OnComplete(() =>
                {
                    Dance();
                    _uiManager.OpenPanel(2);
                });

            });
        else
            transform.DOMove(pos, 0.5f).OnComplete(() =>
            {
                transform.DOLocalRotateQuaternion(rot, 0.2f).OnComplete(() =>
                {
                    Cry();
                    _uiManager.OpenPanel(2);
                });

            });
    }
    private void SetCharacterSpeed(int x)
    {
        _follower.followSpeed = x;
    }
}
public partial class CharacterController : MonoBehaviour
{
    private void Turn(int x)
    {
        var p = Quaternion.Euler(new Vector3(0, 180, 0));
        transform.DOLocalRotateQuaternion(p, 0.25f).SetRelative(true).
        SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOLocalRotateQuaternion(p, 0.25f).
            SetRelative(true).SetEase(Ease.Linear);
            _particleList[x].Play();
        });
    }
    private void SetAnim(int x)
    {
        _animator.SetInteger("changeAnim", x);
    }
    private void PoorWalk()
    {
        SetAnim(1);
    }
    private void AverageWalk()
    {
        SetAnim(2);
    }
    private void RichWalk()
    {
        SetAnim(3);
    }
    private void Dance()
    {
        SetAnim(4);
    }
    private void Cry()
    {
        SetAnim(5);
    }
}
public enum CharacterType
{
    Poor, Rich, Normal
}

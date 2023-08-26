using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private TouchManager _touchManager;
    private GameManager _gameManager;

    [SerializeField]
    private float _leftRightSpeed;

    [SerializeField]
    private Transform _myChild;

    [SerializeField]
    private Vector3 _clampOffSet;

    [SerializeField]
    private SplineFollower _follower;

    private Vector3 _tempPos;

    private bool _isStart;

    private void Start()
    {
        _touchManager = TouchManager.Instance;
        _gameManager = GameManager.Instance;

        StartCoroutine(TouchInitialize());

        _isStart = false;

        transform.localPosition = Vector3.zero;
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
    
    private void FixedUpdate()
    {
        if (!_isStart && _gameManager.GetPlayable())
        {
            SetCharacterSpeed(5);
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
    private void SetCharacterSpeed(int x)
    {
        _follower.followSpeed = x;
    }
    public void FinalPosition()
    {
        transform.DOLocalMove(new Vector3(0, 0, 7f), 0.5f);
    }
}

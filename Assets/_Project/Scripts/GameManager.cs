using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : JSingleton<GameManager>
{
    [SerializeField]
    private bool _playable;

    public void SetPlayable(bool tf)
    {
        _playable = tf;
    }
    public bool GetPlayable()
    {
        return _playable;
    }
}

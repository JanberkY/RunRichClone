using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHyperTouch
{
    event Action<TouchData> DownClick;
    event Action<TouchData> UpClick;
    event Action<TouchData> SetClick;
    void Click();
    void Handle();
}


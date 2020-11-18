using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKCancelPanelController : MonoBehaviour
{
    public delegate void Callback(string s);
    private Callback callback = null;

    public void SetCallback(Callback cal)
    {
        callback = cal;
    }
}

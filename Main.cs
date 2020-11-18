using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject prefabmanager;
    [HideInInspector] public static GameObject pfmanager;

    public GameObject fileManager;

    [HideInInspector] public static GameObject fileMgr;
    // Start is called before the first frame update
    void Start()
    {
        if (pfmanager == null)
        {
            pfmanager = Instantiate(prefabmanager);
        }

        if (fileMgr == null)
        {
            fileMgr = Instantiate(fileManager);
        }
    }
}

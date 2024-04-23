using Framework.Network;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogic_Main : MonoBehaviour
{
    void Start()
    {
        Debug.Log("SceneLogic_Main Start");
    }

    public void Connect()
    {
        NetworkManager.Instance.Connect("TestClient");
    }
}

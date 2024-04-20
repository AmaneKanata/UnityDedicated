using Framework.Network;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogic_Main : MonoBehaviour
{
    Button button;

    void Start()
    {
        Debug.Log("SceneLogic_Main Start");

        button = GameObject.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(Test);
    }

    public async void Test()
    {
        Debug.Log("Test");

        Connection connection = new();
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.8"), 7777);
        await Connector.Connect(endPoint, connection);
    }
}

using Framework.Network;
using System.Net;
using UnityEngine;

public class SceneLogic_Main : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.StartAccept();
    }
}

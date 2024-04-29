using Framework.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogic_Lobby : MonoBehaviour
{
    public void Start()
    {
        NetworkManager.Instance.StartAccept();
    }
}
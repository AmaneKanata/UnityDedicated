using Framework.Network;
using TMPro;
using UnityEngine.UI;

public class LobbyPanel : PanelBase
{
    TMP_InputField ID_Input;

    Button Connect_Button;
    Button Ready_Button;

    private void Awake()
    {
        ID_Input = transform.Find("ID_Input").GetComponent<TMP_InputField>();
        ID_Input.placeholder.GetComponent<TMP_Text>().text = GenerateRandomString(5);

        Connect_Button = transform.Find("Connect_Button").GetComponent<Button>();
        Ready_Button = transform.Find("Ready_Button").GetComponent<Button>();
    }

    private void Start()
    {
        NetworkManager.Instance.Client.connectedHandler += OnConnected;
    }

    private void OnDestroy()
    {
        NetworkManager.Instance.Client.connectedHandler -= OnConnected;
    }

    public void SetConnectButtonState( bool isInteractable )
    {
        Connect_Button.interactable = isInteractable;
    }

    public void SetReadyButtonState( bool isInteractable )
    {
        Ready_Button.interactable = isInteractable;
    }

    public void OnConnected()
    {
        SetConnectButtonState(false);
        SetReadyButtonState(true);
    }

    override public void OnOpen()
    {
        SetConnectButtonState(NetworkManager.Instance.Client.State != ConnectionState.Connected);
        SetReadyButtonState(NetworkManager.Instance.Client.State == ConnectionState.Connected);
    }

    private void Connect()
    {
        string clientId = ID_Input.text;

        if (string.IsNullOrEmpty(clientId))
        {
            clientId = ID_Input.placeholder.GetComponent<TMP_Text>().text;
        }

        NetworkManager.Instance.Connect(clientId);
    }

    private void Ready()
    {
        Protocol.C_READY ready = new Protocol.C_READY() { IsReady = true };
        NetworkManager.Instance.Client.Send(PacketManager.MakeSendBuffer(ready));

        SetReadyButtonState(false);
    }

    public string GenerateRandomString( int length )
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Random random = new();
        char[] randomString = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomString[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomString);
    }
}
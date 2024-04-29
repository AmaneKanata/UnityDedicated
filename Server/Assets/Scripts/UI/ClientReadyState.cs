using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientReadyState : MonoBehaviour
{
    public TMP_Text ID;
    public TMP_Text Ready;

    public void SetID(string id)
    {
        ID.text = id;
    }

    public void SetReady(bool ready)
    {
        Ready.text = ready ? "O" : "X";
    }
}

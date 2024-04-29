using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelBase : MonoBehaviour
{
    virtual public void OnOpen() { }

    virtual public void OnClose() { }
}
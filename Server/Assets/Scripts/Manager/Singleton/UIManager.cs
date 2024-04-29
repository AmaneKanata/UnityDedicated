using MEC;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonManager<UIManager>
{
    #region Members

    class Panel
    {
        public GameObject GameObject;
        public bool IsOpen;
    }

    [NonReorderable] Dictionary<string, Panel> panels = new Dictionary<string, Panel>();

    GameObject Panels;

    #endregion

    #region Initialize

    private void Awake()
    {
        Panels = GameObject.Find(nameof(Panels));

        CacheUI(Panels, panels);
    }

    private void OnDestroy()
    {
        panels.Clear();
    }

    private void CacheUI( GameObject _parent, Dictionary<string, Panel> _objects )
    {
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            var child = _parent.transform.GetChild(i);
            var name = child.name;

            if (_objects.ContainsKey(name))
                continue;

            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            child.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            child.gameObject.SetActive(false);

            _objects[name] = new Panel()
            {
                GameObject = child.gameObject,
                IsOpen = false
            };
        }
    }

    #endregion

    #region Core Methods

    public T FetchPanel<T>() where T : Component
    {
        if (!panels.ContainsKey(typeof(T).ToString())) return null;

        return panels[typeof(T).ToString()].GameObject.GetComponent<T>();
    }

    public void OpenPanel<T>( bool _instant = false ) where T : Component
    {
        string panelName = typeof(T).ToString();

        if (panels.TryGetValue(panelName, out Panel panel))
        {
            if (panel.IsOpen)
                return;

            panel.GameObject.GetComponent<PanelBase>().OnOpen();
            panel.IsOpen = true;

            ShowPanel(panels[panelName].GameObject, true, _instant);
        }
    }

    public void ClosePanel<T>( bool _instant = false ) where T : Component
    {
        string panelName = typeof(T).ToString();

        if (panels.TryGetValue(panelName, out Panel panel))
        {
            if (!panel.IsOpen)
                return;

            panel.GameObject.GetComponent<PanelBase>().OnClose();
            panel.IsOpen = false;

            ShowPanel(panels[panelName].GameObject, false, _instant);
        }
    }

    public void TogglePanel<T>( bool _instant = false ) where T : Component
    {
        string panelName = typeof(T).ToString();

        if (panels.TryGetValue(panelName, out Panel panel))
        {
            panel.IsOpen = !panel.IsOpen;

            if (panel.IsOpen)
                panel.GameObject.GetComponent<PanelBase>().OnOpen();
            else
                panel.GameObject.GetComponent<PanelBase>().OnClose();

            ShowPanel(panels[panelName].GameObject, panel.IsOpen, _instant);
        }
    }

    #endregion

    #region Basic Methods

    public void ShowPanel( GameObject _gameObject, bool _isShow, bool _instant )
    {
        if (_instant)
            Show(_gameObject, _isShow);
        else
            Timing.RunCoroutine(Co_Show(_gameObject, _isShow, 1.5f), _gameObject.GetHashCode());
    }

    private IEnumerator<float> Co_Show( GameObject _gameObject, bool _isShow, float _lerpspeed = 10f )
    {
        var canvasGroup = _gameObject.GetComponent<CanvasGroup>();
        var targetAlpha = _isShow ? 1f : 0f;
        var lerpvalue = 0f;
        var lerpspeed = _lerpspeed;

        if (!_isShow) canvasGroup.blocksRaycasts = false;
        else _gameObject.SetActive(true);

        while (Mathf.Abs(canvasGroup.alpha - targetAlpha) >= 0.001f)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, lerpvalue += lerpspeed * Time.deltaTime);

            yield return Timing.WaitForOneFrame;
        }

        canvasGroup.alpha = targetAlpha;

        if (_isShow) canvasGroup.blocksRaycasts = true;
        else _gameObject.SetActive(false);
    }

    private void Show( GameObject _gameObject, bool _isShow )
    {
        var canvasGroup = _gameObject.GetComponent<CanvasGroup>();
        var targetAlpha = _isShow ? 1f : 0f;

        if (!_isShow) canvasGroup.blocksRaycasts = false;
        else _gameObject.SetActive(true);

        canvasGroup.alpha = targetAlpha;

        if (_isShow) canvasGroup.blocksRaycasts = true;
        else _gameObject.SetActive(false);
    }

    #endregion
}
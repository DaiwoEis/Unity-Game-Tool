using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    protected UIWindow _currWindow;

    public UIWindowTree WindowTree { get; set; }

    private Dictionary<string, UIWindow> _windowDic;

    private Transform _canvasTrans;

    protected override void OnCreate()
    {
        base.OnCreate();

        _windowDic = new Dictionary<string, UIWindow>();
        _canvasTrans = GameObject.FindWithTag("UICanvas").transform;
    }

    public void OpenRootWindow(string windowID, UIWindowSwitcher openSwitcher)
    {
        WindowTree.SetCurrWindowNode(windowID);
        StartCoroutine(_OpenWindowInternal(GetWindow(windowID), openSwitcher));
    }

    private IEnumerator _OpenRootWindow(UIWindow window, UIWindowSwitcher switcher)
    {
        yield return _OpenWindowInternal(window, switcher);
    }

    public void OpenWindow(string windowID)
    {
        var nextWindow = GetWindow(windowID);
        var nodeConnection = WindowTree.CurrNode.OutConnections.Find(c => c.ToNode.WindowID == windowID);
        var connection = nodeConnection.Connection;
        var currWindow = GetWindow(WindowTree.CurrNode.WindowID);

        StartCoroutine(_ChangeWindow(currWindow, connection.inSwitcher, nextWindow, connection.outswitcher, nodeConnection.ToNode));
    }

    public void CloseCurrentWindow()
    {
        var nextWindow = GetWindow(WindowTree.CurrNode.InConnection.FromNode.WindowID);
        var nodeConnection = WindowTree.CurrNode.InConnection;
        var connection = nodeConnection.Connection;
        var currWindow = GetWindow(WindowTree.CurrNode.WindowID);

        StartCoroutine(_ChangeWindow(currWindow, connection.outswitcher, nextWindow, connection.inSwitcher, nodeConnection.FromNode));
    }

    private IEnumerator _ChangeWindow(UIWindow currWindow, UIWindowSwitcher closeSwitcher, UIWindow nextWindow, UIWindowSwitcher openSwitcher, UIWindowTreeNode nextNode)
    {       
        yield return _CloseWindowInternal(currWindow, closeSwitcher);

        WindowTree.CurrNode = nextNode;

        yield return _OpenWindowInternal(nextWindow, openSwitcher);
    }

    private IEnumerator _CloseWindowInternal(UIWindow window, UIWindowSwitcher switcher)
    {
        window.TriggerOnClosingStartEvent();
        switcher.OnClose(window);
        yield return new WaitForSeconds(switcher.closingTime);
        window.TriggerOnClosingCompleteEvent();
        window.gameObject.SetActive(false);
    }

    private IEnumerator _OpenWindowInternal(UIWindow window, UIWindowSwitcher switcher)
    {
        window.gameObject.SetActive(true);
        window.TriggerOnOpeningStartEvent();
        switcher.OnOpen(window);
        yield return new WaitForSeconds(switcher.openingTime);
        window.TriggerOnOpeningCompleteEvent();
    }

    public UIWindow GetWindow(string windowID)
    {
        if (_windowDic.ContainsKey(windowID))
            return _windowDic[windowID];

        var windowPrefab = ResourceManager.Load<GameObject>(windowID);
        var window = Instantiate(windowPrefab, _canvasTrans).GetComponent<UIWindow>();     
        window.gameObject.SetActive(false);
        _windowDic.Add(windowID, window);
        return window;
    }

    public T GetWindow<T>(string windowID) where T : UIWindow
    {
        var window = GetWindow(windowID);
        return window != null ? window as T : null;        
    }
}

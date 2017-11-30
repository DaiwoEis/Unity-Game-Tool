using System.Collections.Generic;
using UnityEngine;

public class UIWindowTree
{
    private List<UIWindowTreeNode> _nodes;

    private List<UIWIndowTreeNodeConnection> _connections;

    private UIWindowTreeNode _currNode;

    public UIWindowTreeNode CurrNode
    {
        get { return _currNode; }
        set { _currNode = value; }
    }

    public UIWindowTree()
    {
        _nodes = new List<UIWindowTreeNode>();
        _connections = new List<UIWIndowTreeNodeConnection>();
    }

    public void SetCurrWindowNode(string windowID)
    {
        CurrNode = _nodes.Find(n => n.WindowID == windowID);
    }

    public void AddNode(string windowID)
    {
        _nodes.Add(new UIWindowTreeNode(windowID));
    }

    public UIWindowTreeNode GetNode(string windowID)
    {
        return _nodes.Find(n => n.WindowID == windowID);
    }

    public void AddConnection(string fromWindowID, string toWindowID, UIWindowConnection connection)
    {
        UIWindowTreeNode fromNode = GetNode(fromWindowID);
        if (fromNode == null)
        {
            Debug.LogError(string.Format("The tree: {0} doesnt has window: {1}", ToString(), fromWindowID));
            return;
        }
        UIWindowTreeNode toNode = GetNode(toWindowID);
        if (toNode == null)
        {
            Debug.LogError(string.Format("The tree: {0} doesnt has window: {1}", ToString(), toWindowID));
            return;
        }

        _connections.Add(new UIWIndowTreeNodeConnection(fromNode, toNode, connection));
    }

    public void BuildTree()
    {
        foreach (var connection in _connections)
        {
            connection.FromNode.OutConnections.Add(connection);
            connection.ToNode.InConnection = connection;
        }
    }
}

public class UIWindowTreeNode
{
    private readonly string _windowID;

    public string WindowID { get { return _windowID; } }

    public UIWIndowTreeNodeConnection InConnection { get; set; }
    public List<UIWIndowTreeNodeConnection> OutConnections { get; private set; }

    public UIWindowTreeNode(string windowID)
    {
        _windowID = windowID;
        OutConnections = new List<UIWIndowTreeNodeConnection>();
    }
}

public class UIWIndowTreeNodeConnection
{
    protected UIWindowTreeNode _fromNode;

    public UIWindowTreeNode FromNode { get { return _fromNode; }}

    protected UIWindowTreeNode _toNode;

    public UIWindowTreeNode ToNode { get { return _toNode; }}

    protected UIWindowConnection _connection;

    public UIWindowConnection Connection { get { return _connection; } }

    public UIWIndowTreeNodeConnection(UIWindowTreeNode fromNode, UIWindowTreeNode toNode, UIWindowConnection connection)
    {
        _fromNode = fromNode;
        _toNode = toNode;
        _connection = connection;
    }
}
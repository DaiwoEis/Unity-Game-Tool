using System.Collections.Generic;
//using Sirenix.OdinInspector;

public class BlackBoard
{
    private readonly Dictionary<string, object> _items;

    public BlackBoard()
    {
        _items = new Dictionary<string, object>();
    }

    public void SetItem<T>(string name, T data)
    {
        if (!_items.ContainsKey(name))
            _items[name] = new BlackBoardItem<T>();
        ((BlackBoardItem<T>)_items[name]).Value = data;
    }

    public T GetItem<T>(string name)
    {
        if (!_items.ContainsKey(name))
            return default(T);
        return ((BlackBoardItem<T>) _items[name]).Value;
    }
}

[System.Serializable]
public class BlackBoardItem<T>
{
    //[ShowInInspector]
    public T Value { get; set; }

    public BlackBoardItem()
    {
        Value = default(T);
    }

    public BlackBoardItem(T value)
    {
        Value = value;
    }

    public static explicit operator T(BlackBoardItem<T> item)
    {
        return item.Value;
    }
}
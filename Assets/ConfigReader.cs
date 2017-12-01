using UnityEngine;
using System.Text;
using System.Collections.Generic;

public class ConfigReader
{
    private readonly byte[] _buffer;

    private int _offset;

    public ConfigReader(byte[] bytes)
    {
        _buffer = bytes;
    }

    public ConfigReader(TextAsset asset)
    {
        _buffer = asset.bytes;
    }

    public bool canRead
    {
        get { return (_buffer != null && _offset < _buffer.Length); }
    }

    static string ReadLine(byte[] buffer, int start, int count)
    {
        return Encoding.UTF8.GetString(buffer, start, count);
    }

    public string ReadLine()
    {
        int max = _buffer.Length;

        while (_offset < max && _buffer[_offset] < 32)
            ++_offset;

        int end = _offset;

        if (end < max)
        {
            for (;;)
            {
                if (end < max)
                {
                    int ch = _buffer[end++];
                    if (ch != '\n' && ch != '\r')
                        continue;
                }
                else
                    ++end;

                string line = ReadLine(_buffer, _offset, end - _offset - 1);
                _offset = end;
                return line;
            }
        }
        _offset = max;
        return null;
    }

    public Dictionary<string, Dictionary<string, string>> ReadDictionary()
    {
        Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
        char[] separator = {'='};

        string mainKey = "Config";
        while (canRead)
        {
            string line = ReadLine();
            if (line == null)
                break;

            if (line.StartsWith("*"))
                continue;

            if (line.StartsWith("["))
            {
                int index = line.IndexOf("]");

                mainKey = line.Substring(1, index - 1);

                if (!dict.ContainsKey(mainKey))
                    dict.Add(mainKey, new Dictionary<string, string>());
            }
            else
            {                
                string[] split = line.Split(separator, 2, System.StringSplitOptions.None);
                if (split.Length == 2)
                {
                    string key = split[0].Trim();
                    string val = split[1].Trim();

                    dict[mainKey].Add(key, val);
                }
            }
        }

        return dict;
    }
}
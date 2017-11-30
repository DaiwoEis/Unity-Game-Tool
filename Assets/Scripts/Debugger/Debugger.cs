using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Debugger
{
    private static Dictionary<string, bool> logSystemEnabledDic;

    private static string configPath = "Assets/Resources/LogConfig.txt";

    private static string xmlConfigPath = "Assets/Resources/LogConfig.xml";

    static Debugger()
    {
        logSystemEnabledDic = new Dictionary<string, bool>();
        foreach (var fieldInfo in typeof(LogType).GetFields(BindingFlags.Public | BindingFlags.Static))
            logSystemEnabledDic[fieldInfo.Name] = false;

        Init();
    }

    private static void Init()
    {
        if (!File.Exists(xmlConfigPath))
        {
            SaveXmlConfig();
        }

        if (!File.Exists(configPath))
        {
            SaveConfig();
            return;
        }

        LoadXmlConfig();
        //LoadConfig();
    }

    public static void SetState(string logType, bool value)
    {
        if (value != logSystemEnabledDic[logType])
        {
            logSystemEnabledDic[logType] = value;
            SaveConfig();
            SaveXmlConfig();
        }
    }

    public static bool GetState(string logType)
    {
        return logSystemEnabledDic[logType];
    }

    private static void LoadConfig()
    {
        string[] configDatas = File.ReadAllLines(configPath);
        foreach (var configData in configDatas)
        {
            string[] data = configData.Split(':');
            logSystemEnabledDic[data[0].Trim()] = (bool)Convert.ChangeType(data[1].Trim().ToLower(), typeof(bool));
        }
    }

    private static void LoadXmlConfig()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlConfigPath);
        foreach (XmlElement configData in xmlDoc.DocumentElement.ChildNodes)
        {
            XmlNode logTypeNode = configData.SelectSingleNode("LogType");
            XmlNode enabledNode = configData.SelectSingleNode("Enabled");
            logSystemEnabledDic[logTypeNode.InnerText] = (bool) Convert.ChangeType(enabledNode.InnerText, typeof(bool));
        }        
    }

    private static void SaveXmlConfig()
    {
        XmlDocument xmlDoc = new XmlDocument();
        var root = xmlDoc.CreateElement("LogToggles");
        foreach (var configData in logSystemEnabledDic)
        {
            XmlNode configNode = xmlDoc.CreateElement("ConfigNode");
            XmlNode logTypeNode = xmlDoc.CreateElement("LogType");
            XmlNode enabledNode = xmlDoc.CreateElement("Enabled");
            logTypeNode.InnerText = configData.Key;
            enabledNode.InnerText = configData.Value.ToString();
            root.AppendChild(configNode);
            configNode.AppendChild(logTypeNode);
            configNode.AppendChild(enabledNode);
        }
        xmlDoc.AppendChild(root);
        xmlDoc.Save(xmlConfigPath);
    }

    private static void SaveConfig()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var data in logSystemEnabledDic)
            stringBuilder.Append(string.Format("{0}:{1}\n", data.Key.PadRight(20), data.Value.ToString().PadRight(10)));
        File.WriteAllText(configPath, stringBuilder.ToString());
    }

    public static void Log(string logType, object message)
    {
        Log(logType, message, null);
    }

    public static void Log(string logType, object message, Object context)
    {
        if (!logSystemEnabledDic[logType]) return;
        Debug.Log(message, context);
    }

    public static void LogError(string logType, object message)
    {
        LogError(logType, message, null);
    }

    public static void LogError(string logType, object message, Object context)
    {
        if (!logSystemEnabledDic[logType]) return;
        Debug.LogError(message, context);
    }
    
    public static void LogWarning(string logType, object message)
    {
        LogWarning(logType, message, null);
    }

    public static void LogWarning(string logType, object message, Object context)
    {
        if (!logSystemEnabledDic[logType]) return;
        Debug.LogWarning(message, context);
    }
}

public static partial  class LogType
{    
}
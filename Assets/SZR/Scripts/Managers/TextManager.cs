using System.Collections.Generic;
using System.Text;
using Boomlagoon.JSON;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
#endif

public class TextManager : MonoBehaviour
{
    public JSONObject data;

    public void ParseJSON(string json)
    {
        data = JSONObject.Parse(json);
    }

    public string GetText(string key, Dictionary<string, string> tokens = null)
    {
        if (data == null)
            return "";

        var obj = data.GetObject(key);
        var text = obj.GetString("text");

        if (tokens != null)
            text = ReplaceTokens(text, tokens);

        return text;
    }

    public void SetText(string key, Text target, Dictionary<string, string> tokens = null)
    {
        target.text = GetText(key, tokens);
    }

    public void SetText(Text target)
    {
        SetText(target.name, target);
    }

    public void SetText(Text[] targets)
    {
        foreach (var instance in targets) SetText(instance.gameObject.name, instance);
    }

    public string[] GetStringArray(string key)
    {
        var list = data.GetArray(key);
        var names = new string[list.Length];
        for (var i = 0; i < list.Length; i++) names[i] = list[i].Str;

        return names;
    }

    public string ReplaceTokens(string defaultText, Dictionary<string, string> tokenValuePairs)
    {
        var sb = new StringBuilder(defaultText);

        foreach (var entry in tokenValuePairs) sb.Replace(entry.Key, entry.Value);

        return sb.ToString();
    }
}
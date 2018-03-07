// Copyright 2015 - 2018 Jesse Freeman
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

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
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
//using Boomlagoon.JSON;
using UnityEngine;
using UnityEngine.UI;
//#if UNITY_EDITOR
//#endif

public class TextManager : MonoBehaviour
{
    // Create new game object to store game text
    private GameText gameText = new GameText();
  
    public string GetText(string key, Dictionary<string, string> tokens = null)
    {

        var field = GetField (key);

        if (field == null)
            return "Undefined";

        var text = field.GetValue(gameText) as string;
        
        if (tokens != null)
            text = ReplaceTokens(text, tokens);

        return text;
    }

    public void ParseJSON(string jsonText)
    {
        // TODO Try to deserialize json on gameText object using Unity's built in json parser
    }

    
    FieldInfo[] fields{
        get{

            if (_fields == null) {
                var type = typeof(GameText);
                _fields = type.GetFields (BindingFlags.Public | BindingFlags.Instance);
            }

            return _fields;
        }
    }

    FieldInfo[] _fields;

    private FieldInfo GetField(string name){
        return fields.FirstOrDefault (f => f.Name == name);
    }

    public void SetTextByKey(string name, string value){
        var field = GetField (name);
        field.SetValue (this, value);
    }
    
    public string ReplaceTokens(string defaultText, Dictionary<string, string> tokenValuePairs)
    {
        var sb = new StringBuilder(defaultText);

        foreach (var entry in tokenValuePairs) sb.Replace(entry.Key, entry.Value);

        return sb.ToString();
    }
}
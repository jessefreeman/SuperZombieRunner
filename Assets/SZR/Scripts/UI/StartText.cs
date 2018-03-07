﻿// Copyright 2015 - 2018 Jesse Freeman
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
using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    private Text text;

    private TextManager textManager
    {
        get { return GameObjectUtil.GetSingleton<TextManager>(); }
    }

    public string actionButtonType
    {
        get
        {
            var platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

            var buttonName = "";

            if (platformDetectionUtil.GetPlatformDefinition().deviceType == DeviceType.Console)
                buttonName = "controller";
            else if (platformDetectionUtil.GetPlatformDefinition().hasTouch)
                buttonName = "touch";
            else
                buttonName = "keyboard";

            return buttonName;
        }
    }

    private string deviceKeyString
    {
        get
        {
            var keyStrings = textManager.data.GetObject("Keys");
            var buttonText = keyStrings.GetString(actionButtonType);
            return buttonText;
        }
    }


    // Use this for initialization
    private void Start()
    {
        text = GetComponent<Text>();

        var token = new Dictionary<string, string>();
        token.Add("${key}", deviceKeyString);

        text.text = textManager.GetText("Start", token);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
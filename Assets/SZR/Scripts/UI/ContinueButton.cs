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

using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public string continueText = "PRESS ${INPUT} TO START!";
    public string controllerText = "ANY BUTTON";
    public string inputToken = "${INPUT}";
    public string keyboard = "ANY KEY";
    private PlatformDetectionUtil platformDetectionUtil;
    public Text textUI;
    public string touchText = "ANYWHERE";

    private void Awake()
    {
        textUI = GetComponent<Text>();
    }

    private void Start()
    {
        if (textUI != null)
            textUI.text = GetText();
    }

    public string GetText()
    {
        var replacement = "";

        platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

        if (platformDetectionUtil.GetPlatformDefinition().hasController)
            replacement += controllerText;
        else if (platformDetectionUtil.GetPlatformDefinition().hasTouch ||
                 platformDetectionUtil.GetPlatformDefinition().hasMouse)
            replacement += touchText;
        else
            replacement += keyboard;

        return continueText.Replace(inputToken, replacement);
    }
}
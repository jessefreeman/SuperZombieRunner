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

public class MuteButton : MonoBehaviour
{
    private Text label;

    //private SoundManager soundManager;
    private PlatformDetectionUtil platformDetectionUtil;

    private SoundManager soundManager
    {
        get { return GameObjectUtil.GetSingleton<SoundManager>(); }
    }

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Start()
    {
        //soundManager = GameObjectUtil.GetSingleton<SoundManager>();
        platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

        //TODO need to add in way to save mute

        // If it's on a console don't show this GameObject
        if (platformDetectionUtil.GetPlatformDefinition().deviceType == DeviceType.Console)
            GameObjectUtil.Destroy(gameObject);

        UpdateLabel();
    }

    public void UpdateLabel()
    {
        if (soundManager.mute)
            label.text = "SOUND ON/<color=#f1af2e>OFF</color>";
        else
            label.text = "SOUND <color=#f1af2e>ON</color>/OFF";
    }

    public void ToggleMute()
    {
        soundManager.ToggleMute();
        UpdateLabel();
    }
}
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

public class AchievementMessageBox : MonoBehaviour
{
    private float delay;
    public Image image;

    // Static singleton property
    public Text textField;
    private float time = -1;

    private TweenLayer tweenLayer;

    // Use this for initialization
    private void Awake()
    {
        GameObjectUtil.RegisterSingleton<AchievementMessageBox>(this);
        tweenLayer = GetComponent<TweenLayer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (time != -1)
        {
            time += Time.deltaTime;
            if (time > delay) HideMessage();
        }
    }

    public void ShowMessage(string value, float delay, Sprite sprite)
    {
        if (textField != null)
        {
            textField.text = value;
            image.sprite = sprite;
            time = 0;
            this.delay = delay;
            tweenLayer.TweenIn();
        }

//        if (image != null)
//        {

        //}
    }

    public void HideMessage()
    {
        tweenLayer.TweenOut();
        time = -1;
    }

    public void ToggleDisplay(bool value)
    {
        gameObject.SetActive(value);

        if (!value)
            HideMessage();
    }

    public void Reset()
    {
        tweenLayer.Reset();
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = tweenLayer.start;
    }
}
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

using UnityEngine;

public class TweenLayer : MonoBehaviour
{
    public bool autoRun = true;
    public Vector2 end;
    public float endDelay;
    public float endTime = .5f;

    private RectTransform rectTransform;
    public bool repeat;
    public Vector2 start;
    public float startDelay;
    public float startTime = .5f;
    public LeanTweenType tween = LeanTweenType.linear;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (autoRun)
            TweenIn();
    }

    public void TweenIn()
    {
        if (rectTransform == null)
            return;

        rectTransform.anchoredPosition = start;
        var tmpTween = LeanTween.move(rectTransform, end, startTime).setEase(tween).setDelay(startDelay);
        if (repeat)
            tmpTween.setOnComplete(TweenIn);
    }

    public void TweenOut()
    {
        if (rectTransform == null)
            return;

        LeanTween.move(rectTransform, start, startTime).setEase(tween).setDelay(endDelay);
    }

    public void Reset()
    {
        LeanTween.reset();
    }
}
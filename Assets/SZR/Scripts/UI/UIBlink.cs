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

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    public Color defaultColor;

    public float delay = .5f;
    public bool startVisible;
    public Material targetMaterial;
    public Text textUI;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(OnBlink());
        var textUI = GetComponent<Text>();
        defaultColor = textUI.color;

        if (!startVisible)
            textUI.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0);
    }

    private IEnumerator OnBlink()
    {
        yield return new WaitForSeconds(delay);

        var alpha = textUI.color.a;

        var newAlpha = 0f;

        if (alpha != 1) newAlpha = 1f;

        textUI.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, newAlpha);

        StartCoroutine(OnBlink());
    }
}
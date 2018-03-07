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
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    public float delay;
    public float delayBetweenScores = 1;
    public string format = "D8";
    public Text highScoreTextField;

    public Text scoreTextField;
    public float time = .5f;

    // Use this for initialization
    private void Start()
    {
        var defaultValue = 0.ToString(format);
        scoreTextField.text = highScoreTextField.text = defaultValue;

        //Tween Values
        LeanTween.value(gameObject, UpdateScoreText, 0f, PlayerPrefs.GetInt("Score", 0), time).setDelay(delay)
            .setEase(LeanTweenType.easeOutElastic);
        LeanTween.value(gameObject, UpdateHightScoreText, 0f, PlayerPrefs.GetInt("HighScore", 0), time)
            .setDelay(delay + delayBetweenScores).setEase(LeanTweenType.easeOutElastic);
    }

    private void UpdateScoreText(float val)
    {
        var score = ((int) Mathf.Round(val)).ToString(format);
        if (scoreTextField != null)
            scoreTextField.text = score;
    }

    private void UpdateHightScoreText(float val)
    {
        var score = ((int) Mathf.Round(val)).ToString(format);
        if (highScoreTextField != null)
            highScoreTextField.text = score;
    }
}
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
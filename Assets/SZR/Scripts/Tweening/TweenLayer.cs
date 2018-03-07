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
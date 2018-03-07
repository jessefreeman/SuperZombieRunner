using UnityEngine;

public class PanelTweenManager : MonoBehaviour
{
    private int currentPanel;

    public float delay = 5;
    public float initialDelay = .5f;

    private float lastTime;
    private float startTime;

    public TweenLayer[] tweenLayers;

    // Use this for initialization
    private void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - startTime < initialDelay) return;

        if (Time.time - lastTime > delay) OnChange();
    }

    private void OnChange()
    {
        lastTime = Time.time;

        tweenLayers[currentPanel].TweenOut();

        currentPanel++;
        if (currentPanel >= tweenLayers.Length)
            currentPanel = 0;

        tweenLayers[currentPanel].TweenIn();
        var childrenTweenLayers = tweenLayers[currentPanel].gameObject.GetComponentsInChildren<TweenLayer>();
        foreach (var tweenLayer in childrenTweenLayers) tweenLayer.TweenIn();
    }
}
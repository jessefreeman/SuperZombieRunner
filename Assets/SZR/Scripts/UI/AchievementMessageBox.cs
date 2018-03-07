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
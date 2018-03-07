using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private float delay;

    // Static singleton property
    public Text textField;
    private float time = -1;

    private TweenLayer tweenLayer;

    // Use this for initialization
    private void Awake()
    {
        GameObjectUtil.RegisterSingleton<MessageBox>(this);
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

    public void ShowMessage(string value, float delay)
    {
        if (textField != null)
        {
            textField.text = value;
            time = 0;
            this.delay = delay;
            tweenLayer.TweenIn();
        }
    }

    public void HideMessage()
    {
        tweenLayer.TweenOut();
        time = -1;
    }
}
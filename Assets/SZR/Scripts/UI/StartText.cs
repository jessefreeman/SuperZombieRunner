using System.Collections.Generic;
using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    private Text text;

    private TextManager textManager
    {
        get { return GameObjectUtil.GetSingleton<TextManager>(); }
    }

    public string actionButtonType
    {
        get
        {
            var platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

            var buttonName = "";

            if (platformDetectionUtil.GetPlatformDefinition().deviceType == DeviceType.Console)
                buttonName = "controller";
            else if (platformDetectionUtil.GetPlatformDefinition().hasTouch)
                buttonName = "touch";
            else
                buttonName = "keyboard";

            return buttonName;
        }
    }

    private string deviceKeyString
    {
        get
        {
            var keyStrings = textManager.data.GetObject("Keys");
            var buttonText = keyStrings.GetString(actionButtonType);
            return buttonText;
        }
    }


    // Use this for initialization
    private void Start()
    {
        text = GetComponent<Text>();

        var token = new Dictionary<string, string>();
        token.Add("${key}", deviceKeyString);

        text.text = textManager.GetText("Start", token);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
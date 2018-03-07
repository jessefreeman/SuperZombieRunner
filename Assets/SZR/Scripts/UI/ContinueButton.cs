using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public string continueText = "PRESS ${INPUT} TO START!";
    public string controllerText = "ANY BUTTON";
    public string inputToken = "${INPUT}";
    public string keyboard = "ANY KEY";
    private PlatformDetectionUtil platformDetectionUtil;
    public Text textUI;
    public string touchText = "ANYWHERE";

    private void Awake()
    {
        textUI = GetComponent<Text>();
    }

    private void Start()
    {
        if (textUI != null)
            textUI.text = GetText();
    }

    public string GetText()
    {
        var replacement = "";

        platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

        if (platformDetectionUtil.GetPlatformDefinition().hasController)
            replacement += controllerText;
        else if (platformDetectionUtil.GetPlatformDefinition().hasTouch ||
                 platformDetectionUtil.GetPlatformDefinition().hasMouse)
            replacement += touchText;
        else
            replacement += keyboard;

        return continueText.Replace(inputToken, replacement);
    }
}
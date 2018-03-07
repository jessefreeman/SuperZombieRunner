using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Text label;

    //private SoundManager soundManager;
    private PlatformDetectionUtil platformDetectionUtil;

    private SoundManager soundManager
    {
        get { return GameObjectUtil.GetSingleton<SoundManager>(); }
    }

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Start()
    {
        //soundManager = GameObjectUtil.GetSingleton<SoundManager>();
        platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

        //TODO need to add in way to save mute

        // If it's on a console don't show this GameObject
        if (platformDetectionUtil.GetPlatformDefinition().deviceType == DeviceType.Console)
            GameObjectUtil.Destroy(gameObject);

        UpdateLabel();
    }

    public void UpdateLabel()
    {
        if (soundManager.mute)
            label.text = "SOUND ON/<color=#f1af2e>OFF</color>";
        else
            label.text = "SOUND <color=#f1af2e>ON</color>/OFF";
    }

    public void ToggleMute()
    {
        soundManager.ToggleMute();
        UpdateLabel();
    }
}
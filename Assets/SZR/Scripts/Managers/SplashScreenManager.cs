using jessefreeman.utools;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public TextAsset jsonData;

    // Use this for initialization
    private void Awake()
    {
        var textManager = GameObjectUtil.GetSingleton<TextManager>();
        textManager.ParseJSON(jsonData.ToString());
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
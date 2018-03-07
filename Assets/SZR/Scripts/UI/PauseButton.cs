using jessefreeman.utools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    private Image buttonImage;
    private GameManager gameManager;

    public Sprite pauseSprite;
    private PlatformDetectionUtil platformDetectionUtil;
    public Sprite playSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.TogglePause();
    }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    private void Start()
    {
        gameManager = GameObjectUtil.GetSingleton<GameManager>();
        platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();
        // If it's on a console don't show this GameObject
        if (platformDetectionUtil.GetPlatformDefinition().deviceType == DeviceType.Console)
            GameObjectUtil.Destroy(gameObject);
    }

    public void UpdateButtonSprite()
    {
        if (pauseSprite != null && playSprite != null)
            if (Time.timeScale == 0)
                buttonImage.sprite = playSprite;
            else
                buttonImage.sprite = pauseSprite;
    }
}
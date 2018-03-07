// Copyright 2015 - 2018 Jesse Freeman
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

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
using UnityEngine;

public class TiledBackground : MonoBehaviour
{
    public bool scaleHorizontially = true;
    public bool scaleVertically = true;

    public int textureSize = 32;

    // Use this for initialization
    private void Start()
    {
        var newWidth = !scaleHorizontially ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
        var newHeight = !scaleVertically ? 1 : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));

        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }
}
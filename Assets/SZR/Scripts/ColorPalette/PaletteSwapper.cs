using jessefreeman.utools;
using UnityEngine;

public class PaletteSwapper : MonoBehaviour, IRecyle
{
    private MaterialPropertyBlock block;
    public ColorPalette[] palettes;

    public SpriteRenderer spriteRenderer;

    private Texture2D texture;

    public void Restart()
    {
        if (palettes.Length > 0)
            SwapColors(palettes[Random.Range(0, palettes.Length)]);
    }

    public void Shutdown()
    {
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    private void Start()
    {
        Restart();
    }

    private void SwapColors(ColorPalette palette)
    {
        if (palette.cachedTexture == null)
        {
            //texture = spriteRenderer.sprite.texture;
            texture = palette.source;

            var w = texture.width;
            var h = texture.height;

            var cloneTexture = new Texture2D(w, h);
            cloneTexture.wrapMode = TextureWrapMode.Clamp;
            cloneTexture.filterMode = FilterMode.Point;

            var colors = texture.GetPixels();

            for (var i = 0; i < colors.Length; i++) colors[i] = palette.GetColor(colors[i]);

            cloneTexture.SetPixels(colors);
            cloneTexture.Apply();

            palette.cachedTexture = cloneTexture;
        }

        block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", palette.cachedTexture);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void LateUpdate()
    {
        spriteRenderer.SetPropertyBlock(block);
    }
}
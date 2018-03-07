using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{
    private Material material;

    private Vector2 offset = Vector2.zero;

    public Vector2 speed = Vector2.zero;

    // Use this for initialization
    private void Start()
    {
        material = GetComponent<Renderer>().material;

        offset = material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    private void Update()
    {
        offset += speed * Time.deltaTime;

        material.SetTextureOffset("_MainTex", offset);
    }
}
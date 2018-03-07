using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    public Color defaultColor;

    public float delay = .5f;
    public bool startVisible;
    public Material targetMaterial;
    public Text textUI;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(OnBlink());
        var textUI = GetComponent<Text>();
        defaultColor = textUI.color;

        if (!startVisible)
            textUI.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0);
    }

    private IEnumerator OnBlink()
    {
        yield return new WaitForSeconds(delay);

        var alpha = textUI.color.a;

        var newAlpha = 0f;

        if (alpha != 1) newAlpha = 1f;

        textUI.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, newAlpha);

        StartCoroutine(OnBlink());
    }
}
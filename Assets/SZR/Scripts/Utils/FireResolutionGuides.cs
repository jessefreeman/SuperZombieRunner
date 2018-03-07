using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class FireResolutionGuides : MonoBehaviour
{
    public enum GizmosType
    {
        DrawAlways,
        DrawOnSelection,
        Disable
    }

    private float cameraRatio;

    [Space(16f)] public GizmosType gizmos;

    public float gizmosDepth = 5;

    public bool showSafeZone = true;

    private Vector2 targetResolution = new Vector2(1920, 1080);
    private Vector2 targetResolution2 = new Vector2(1280, 800);

    private float heightFactor
    {
        get { return Screen.height / (float) Screen.height; }
    }

    private float PixelsToScreen
    {
        get { return 1f / Screen.height; }
    }

    private float ScreenToUnits
    {
        get { return GetComponent<Camera>().orthographicSize; }
    }

    private float PixelsToUnits
    {
        get { return PixelsToScreen * ScreenToUnits; }
    }

    private void OnDrawGizmos()
    {
        if (gizmos == GizmosType.DrawAlways) DrawResolutionGizmos();
    }

    private void OnDrawGizmosSelected()
    {
        if (gizmos == GizmosType.DrawOnSelection) DrawResolutionGizmos();
    }

    private void DrawResolutionGizmos()
    {
        cameraRatio = GetComponent<Camera>().aspect;
        float cameraHeight;
        float cameraWidth;
        Vector3 cameraSize;
        if (GetComponent<Camera>().orthographic)
        {
            cameraHeight = Screen.height;
            cameraWidth = cameraHeight * cameraRatio;
            cameraSize = new Vector3(cameraWidth, cameraHeight) * PixelsToUnits;
        }
        else
        {
            cameraHeight = gizmosDepth * Mathf.Tan(GetComponent<Camera>().fieldOfView * 0.5f * Mathf.Deg2Rad);
            cameraWidth = cameraHeight * cameraRatio;
            cameraSize = new Vector3(cameraWidth, cameraHeight);
        }

        var height = cameraHeight;
        float width;

        var ratio = CalculateRatio(targetResolution.x, targetResolution.y);
        width = height * ratio;

        var screenSize = new Vector3(width, height) * (GetComponent<Camera>().orthographic ? PixelsToUnits : 1);

        var lableColor = Color.white * .6f;
        DrawRect(screenSize, lableColor);
        DrawLabel("Approximate Fire TV Resolution", screenSize, targetResolution, 0, lableColor);


        ratio = CalculateRatio(targetResolution2.x, targetResolution2.y);
        width = height * ratio;

        screenSize = new Vector3(width, height) * (GetComponent<Camera>().orthographic ? PixelsToUnits : 1);

        lableColor = Color.white * .6f;
        DrawRect(screenSize, lableColor);
        DrawLabel("Approximate Fire Tablet Resolution", screenSize, targetResolution2, 1, lableColor);

        if (showSafeZone)
        {
            DrawRect(cameraSize * 0.95f, Color.red * 0.8f);
            DrawRect(cameraSize * 0.90f, Color.green * 0.8f);
        }
    }

    private void DrawRect(Vector3 size, Color color)
    {
        var saveColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawLine(
            transform.position + transform.right * -size.x + transform.up * size.y + gizmosDepth * transform.forward,
            transform.position + transform.right * size.x + transform.up * size.y + gizmosDepth * transform.forward);
        Gizmos.DrawLine(
            transform.position + transform.right * size.x + transform.up * size.y + gizmosDepth * transform.forward,
            transform.position + transform.right * size.x + transform.up * -size.y + gizmosDepth * transform.forward);
        Gizmos.DrawLine(
            transform.position + transform.right * size.x + transform.up * -size.y + gizmosDepth * transform.forward,
            transform.position + transform.right * -size.x + transform.up * -size.y + gizmosDepth * transform.forward);
        Gizmos.DrawLine(
            transform.position + transform.right * -size.x + transform.up * -size.y + gizmosDepth * transform.forward,
            transform.position + transform.right * -size.x + transform.up * size.y + gizmosDepth * transform.forward);
        Gizmos.color = saveColor;
    }

    private void DrawLabel(string label, Vector3 size, Vector2 targetSize, float offset, Color color)
    {
#if UNITY_EDITOR
        var saveColor = Gizmos.color;
        var saveColorHandles = Handles.color;
        var handlesStyle = new GUIStyle();
        Gizmos.color = color;
        string text;
        float verticalOffset;
        handlesStyle.normal.textColor = color;
        handlesStyle.fontSize =
            (int) (1f / HandleUtility.GetHandleSize(transform.position + new Vector3(-size.x, size.y) +
                                                    gizmosDepth * transform.forward));
        var scale = 0.2f * HandleUtility.GetHandleSize(transform.position + new Vector3(-size.x, size.y) +
                                                       gizmosDepth * transform.forward);

        text = " " + label + " (" + (int) (Screen.height * heightFactor * CalculateRatio(targetSize.x, targetSize.y)) +
               "x" + Screen.height * heightFactor + ")";
        verticalOffset = (1 - offset + 1) * scale;

        Gizmos.DrawLine(
            transform.position + transform.right * -size.x + transform.up * (size.y + verticalOffset) +
            gizmosDepth * transform.forward,
            transform.position + transform.right * -size.x + transform.up * size.y + gizmosDepth * transform.forward);
        Handles.Label(
            transform.position + transform.right * -size.x + transform.up * (size.y + verticalOffset) +
            gizmosDepth * transform.forward, text, handlesStyle);
        Gizmos.color = saveColor;
        Handles.color = saveColorHandles;
#endif
    }

    private float CalculateRatio(float w, float h)
    {
        return w / h;
    }
}
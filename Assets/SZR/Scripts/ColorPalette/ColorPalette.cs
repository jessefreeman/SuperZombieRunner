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

using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[Serializable]
public class ColorPalette : ScriptableObject
{
    public Texture2D cachedTexture;
    public List<Color> newPalette = new List<Color>();
    public List<Color> palette = new List<Color>();

    public Texture2D source;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Color Palette")]
    public static void CreateColorPalette()
    {
        if (Selection.activeObject is Texture2D)
        {
            var selectedTexture = Selection.activeObject as Texture2D;

            var selectionPath = AssetDatabase.GetAssetPath(selectedTexture);

            selectionPath = selectionPath.Replace(".png", "-color-palette.asset");

            var newPalette = CustomAssetUtil.CreateAsset<ColorPalette>(selectionPath);

            newPalette.source = selectedTexture;
            newPalette.ResetPalette();

            Debug.Log("Creating a Palette " + selectionPath);
        }
        else
        {
            Debug.Log("Can't create a Palette");
        }
    }
#endif

    private List<Color> BuildPalette(Texture2D texture)
    {
        var palette = new List<Color>();

        var colors = texture.GetPixels();

        foreach (var color in colors)
            if (!palette.Contains(color))
                if (color.a == 1)
                    palette.Add(color);

        return palette;
    }

    public void ResetPalette()
    {
        palette = BuildPalette(source);
        newPalette = new List<Color>(palette);
    }

    public Color GetColor(Color color)
    {
        for (var i = 0; i < palette.Count; i++)
        {
            var tmpColor = palette[i];

            if (Mathf.Approximately(color.r, tmpColor.r) &&
                Mathf.Approximately(color.g, tmpColor.g) &&
                Mathf.Approximately(color.b, tmpColor.b) &&
                Mathf.Approximately(color.a, tmpColor.a))
                return newPalette[i];
        }

        return color;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor
{
    public ColorPalette colorPalette;

    private void OnEnable()
    {
        colorPalette = target as ColorPalette;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Source Texture");

        colorPalette.source = EditorGUILayout.ObjectField(colorPalette.source, typeof(Texture2D), false) as Texture2D;


        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Current Color");
        GUILayout.Label("New Color");

        EditorGUILayout.EndHorizontal();

        for (var i = 0; i < colorPalette.palette.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.ColorField(colorPalette.palette[i]);

            colorPalette.newPalette[i] = EditorGUILayout.ColorField(colorPalette.newPalette[i]);

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Revert Palette")) colorPalette.ResetPalette();

        EditorUtility.SetDirty(colorPalette);
    }
}
#endif
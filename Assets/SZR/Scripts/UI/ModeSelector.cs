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

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    public const string CURRENT_MODE = "CurrentMode";
    public GameObject[] buttonInstances;

    public int currentMode;
    public string[] descriptions;
    public Text descriptionTextField;
    public float selectionDelay = 1f;
    public GameObject StartButton;

    // Use this for initialization
    private void Start()
    {
        //TODO save out last selected mode and set to currentMode

        currentMode = PlayerPrefs.GetInt(CURRENT_MODE, 0);

        StartCoroutine(FirstSelection());
    }

    private IEnumerator FirstSelection()
    {
        yield return new WaitForSeconds(selectionDelay);
        MakeSelection(currentMode);
    }

    public void OnSelection(GameObject target)
    {
        for (var i = 0; i < buttonInstances.Length; i++)
            if (buttonInstances[i].name == target.name)
            {
                MakeSelection(i);
                return;
            }
    }

    private void MakeSelection(int id)
    {
        if (id != currentMode) ToggleButton(currentMode, true);

        currentMode = id;
        ToggleButton(currentMode, false);
        descriptionTextField.text = descriptions[currentMode];

        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(StartButton);
    }

    private void ToggleButton(int id, bool interactable)
    {
        var buttonScript = buttonInstances[id].GetComponent<Button>();

        buttonScript.interactable = interactable;


        PlayerPrefs.SetInt(CURRENT_MODE, id);
    }
}
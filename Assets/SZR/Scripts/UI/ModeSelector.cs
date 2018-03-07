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
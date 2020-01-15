using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
struct SetupButton
{
    public GameObject button;
    public bool buttonState;
}

/// <summary>
/// This script shall be added as a component on the ControlPanel.
/// It references the buttons displayed on the Control Panel and their alternative states.
/// Finally it stores the Scene Setup IDs and make them accessible for the SceneOrganizer.
/// </summary>
public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    SetupButton[] _setupButtons;
    [HideInInspector] public int setupId;



    List<SetupButton> _buttonList;
    List<GameObject> _buttonGameObjectsList;
    List<bool> _buttonOldStatesList;


    [SerializeField]
    Material _buttonActivatedMaterial;
    [SerializeField]
    Material _buttonDeactivatedMaterial;

    void Start()
    {
        _buttonGameObjectsList = new List<GameObject>();
        _buttonOldStatesList = new List<bool>();
        foreach (SetupButton s in _setupButtons)
        {
            _buttonGameObjectsList.Add(s.button);
            _buttonOldStatesList.Add(s.buttonState);
        }
    }

    void UpdateButtonState()
    {
        int i = 0;
        List<bool> buttonNewStatesList = new List<bool>(_buttonOldStatesList.Count);

        foreach (SetupButton s in _setupButtons)
        {
            ButtonTrigger bt = s.button.GetComponent<ButtonTrigger>();
            buttonNewStatesList.Insert(i, bt.enabled);
            if (buttonNewStatesList[i] != _buttonOldStatesList[i] && !_buttonOldStatesList[i])
            {
                s.button.GetComponent<Renderer>().material = _buttonActivatedMaterial;
                setupId = bt.id;
            }
            // Eventuellement, empecher desactivation de tous les boutons
            else
            {
                bt.enabled = false;
                s.button.GetComponent<Renderer>().material = _buttonDeactivatedMaterial;
            }
            i += 1;
        }
        _buttonOldStatesList = buttonNewStatesList;
        transform.parent.SendMessage("Setup");
    }
}
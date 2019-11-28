using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script shall be added as a component on the ControlPanel.
/// It references the buttons displayed on the Control Panel and their alternative states.
/// Finally it stores the Scene Setup IDs and make them accessible for the SceneOrganizer.
/// </summary>

struct SetupButton
{
    public GameObject button;
    public bool buttonState;
}

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    SetupButton[] _setupButtons;
    [HideInInspector] public int setupId;
    Texture _basicTxtr;
    Texture _pressedTxtr;


    Dictionary<GameObject, bool> _buttonList;

    [SerializeField]
    Material _buttonActivatedMaterial;
    [SerializeField]
    Material _buttonDeactivatedMaterial;

    void Start()
    {
        foreach (SetupButton s in _setupButtons)
        {
            _buttonList.Add(s.button, s.buttonState);
        }
    }

    void UpdateButtonState()
    {
        foreach (GameObject g in _buttonList.Keys)
        {
            _buttonList[g] = false;
            g.GetComponent<Renderer>().material = _buttonDeactivatedMaterial;
        }
        foreach (GameObject g in _buttonList.Keys)
        {
            ButtonTrigger bt = g.GetComponent<ButtonTrigger>();
            if (bt.enabled  && !_buttonList.ContainsValue(true))
            {
                _buttonList[g] = true;
                g.GetComponent<Renderer>().material = _buttonActivatedMaterial;
                setupId = bt.id;
            }
            transform.parent.SendMessage("Setup");
        }

    }
}

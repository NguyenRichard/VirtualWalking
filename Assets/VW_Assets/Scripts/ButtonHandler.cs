using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script shall be added as a component on each of the player's hands.
/// It references the buttons displayed on the Control Panel and their alternative states.
/// Finally it stores the Scene Setup IDs and make them accessible for the SceneOrganizer.
/// </summary>

struct SetupButton
{
    public GameObject button;
    public int buttonId;
}

[RequireComponent(typeof(SceneOrganizer))]
public class ButtonHandler : MonoBehaviour
{

    [SerializeField] SetupButton[] _setupButtons;
    int _wallSetupStateId;
    int _ceilingSetupId;
    Texture _basicTxtr;
    Texture _pressedTxtr;

    Dictionary<GameObject, int> _buttonList;

    void Start()
    {
        foreach (SetupButton s in _setupButtons)
        {
            _buttonList.Add(s.button, s.buttonId);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Try to get the button Id and stores is in the given field if the colliding object is in the buttonList
        // Returns true if successful, false otherwise
        Vector2 oldIDs = new Vector2(_wallSetupStateId, _ceilingSetupId);
        int newID;
        if (_buttonList.TryGetValue(other.gameObject, out newID))
        {
            if (newID <= 3)
            {
                _wallSetupStateId = _wallSetupStateId == newID ? 0 : newID;
            }
            else
            {
                _ceilingSetupId = _ceilingSetupId == newID ? 0 : newID;
            }
            // Change material accordingly
            Material buttonMat = other.gameObject.GetComponent<Material>();
            buttonMat.mainTexture = buttonMat.mainTexture == _basicTxtr ? _pressedTxtr : _basicTxtr;
        }
    }
}

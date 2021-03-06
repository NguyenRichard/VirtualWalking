﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelChoiceHandler : MonoBehaviour
{
    [SerializeField] GameObject[] _tunnels;
    [SerializeField] GameObject[] _buttons;

    [SerializeField]
    Material _buttonActivatedMaterial;
    [SerializeField]
    Material _buttonDeactivatedMaterial;



    public void ChangeTunnel(int buttonId)
    {
        for (int i = 0; i < _tunnels.Length; i++)
        {
            _tunnels[i].SetActive(false);
            if (_buttons[i].GetComponent<BoxCollider>().enabled)
                _buttons[i].GetComponentInChildren<Renderer>().material = _buttonDeactivatedMaterial;
        }
        _tunnels[buttonId].SetActive(true);
        _buttons[buttonId].GetComponentInChildren<Renderer>().material = _buttonActivatedMaterial;
    }

    public void EnableButton(int id)
    {
        if (id < _buttons.Length)
        {
            _buttons[id].GetComponent<BoxCollider>().enabled = true;
            _buttons[id].GetComponentInChildren<Renderer>().material = _buttonDeactivatedMaterial;
        }
    }
}
